// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Services.SqlServerCE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.Services.EPG.Serialization
{
    public static class EpgDbQuery
    {
        public enum SingleEventOffset
        {
            Before = -1,
            Now = 0,
            After = 1,
        } // enum SingleEventOffset

        #region ServiceDbId

        public static int GetDatabaseId(this EpgService epgService, string dbFile)
        {
            if (epgService.ServiceDatabaseId > 0) return epgService.ServiceDatabaseId;

            using (var cn = DbServices.GetConnection(dbFile))
            {
                var id = GetDatabaseIdForServiceId(epgService.ServiceId, cn);
                cn.Close();
                epgService.ServiceDatabaseId = id;

                return id;
            } // using cn
        } // GetDatabaseId

        public static int GetDatabaseId(this EpgService epgService, SqlCeConnection cn)
        {
            if (epgService.ServiceDatabaseId > 0) return epgService.ServiceDatabaseId;

            var id = GetDatabaseIdForServiceId(epgService.ServiceId, cn);
            epgService.ServiceDatabaseId = id;

            return id;
        } // GetDatabaseId

        public static int GetDatabaseIdForServiceId(string serviceId, SqlCeConnection cn)
        {
            bool added;

            added = false;
            while (true)
            {
                // find
                var id = FindDatabaseIdForServiceId(serviceId, cn);
                if (id > 0) return id;
                if (added) throw new DataException(); // should never happen!

                // add
                using (var cmd = new SqlCeCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [ServiceId] (ServiceId) VALUES (?)";
                    cmd.Parameters.Add("@ServiceId", SqlDbType.NVarChar, 256).Value = serviceId;
                    cmd.ExecuteNonQuery();
                } // using cmd
            } // while
        }  // GetDatabaseIdForServiceId

        #endregion

        public static EpgEvent GetSingleEvent(SqlCeConnection cn, int serviceDbId, SingleEventOffset offset, DateTime? now = null)
        {
            using (var cmd = GetSingleEventCommand(serviceDbId, offset, now ?? DateTime.Now))
            {
                cmd.Connection = cn;
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return DbServices.LoadData<EpgEvent>(reader, 0, 1);
                } // using reader
            } // using cmd
        } // GetSingleEvent

        public static EpgEvent[] GetBeforeNowAndThenEvents(SqlCeConnection cn, int serviceDbId, DateTime? now = null)
        {
            EpgEvent epgNowEvent, epgBeforeEvent, epgAfterEvent;

            var start = now ?? DateTime.Now;
            epgNowEvent = EpgDbQuery.GetSingleEvent(cn, serviceDbId, EpgDbQuery.SingleEventOffset.Now, start);
            epgBeforeEvent = null;
            epgAfterEvent = null;

            if (epgNowEvent != null)
            {
                if (epgNowEvent.EndTime < start)
                {
                    epgBeforeEvent = epgNowEvent;
                    epgNowEvent = null;
                }
                else
                {
                    epgBeforeEvent = EpgDbQuery.GetSingleEvent(cn, serviceDbId, EpgDbQuery.SingleEventOffset.Before, epgNowEvent.StartTime);
                    epgAfterEvent = EpgDbQuery.GetSingleEvent(cn, serviceDbId, EpgDbQuery.SingleEventOffset.After, epgNowEvent.EndTime);
                } // if-else
            } // if

            if (epgAfterEvent == null)
            {
                epgAfterEvent = EpgDbQuery.GetSingleEvent(cn, serviceDbId, EpgDbQuery.SingleEventOffset.After, start);
            } // if

            if ((epgBeforeEvent == null) && (epgNowEvent == null) && (epgAfterEvent == null))
            {
                return null;
            } // if

            return new EpgEvent[]
            {
                epgBeforeEvent, epgNowEvent, epgAfterEvent
            };
        } // GetBeforeNowAndThenEvents

        public static IList<EpgEvent> GetDateRange(SqlCeConnection cn, int serviceDbId, DateTime? start = null, DateTime? end = null)
        {
            List<EpgEvent> epgEvents = new List<EpgEvent>();

            using (var cmd = GetFromDateRangeCommand(serviceDbId, start, end))
            {
                cmd.Connection = cn;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var epgEvent = DbServices.LoadData<EpgEvent>(reader, 0, 1);
                        epgEvents.Add(epgEvent);
                    } // while
                } // using reader
            } // using cmd

            return epgEvents;
        } // GetEpgEvents

        #region

        #endregion

        #region Auxiliary methods: ServiceDbId

        private static int FindDatabaseIdForServiceId(string serviceId, SqlCeConnection cn)
        {
            using (var cmd = new SqlCeCommand())
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [DbId] FROM [ServiceId] WHERE (ServiceId = ?)";
                cmd.Parameters.Add("@ServiceId", SqlDbType.NVarChar, 256).Value = serviceId;

                var result = cmd.ExecuteScalar();
                if (!(result is int)) return 0;
                return (int)result;
            } // using cmd
        } // FindDatabaseIdForServiceId

        #endregion

        #region Auxiliary methods

        private static SqlCeCommand GetSingleEventCommand(int serviceDbId, SingleEventOffset offset, DateTime start)
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            var cmdText = new StringBuilder();
            cmdText.Append("SELECT TOP 1 XmlEpgData, XmlEpgDataAlt " +
                "FROM Events ");

            start = start.ToUniversalTime();
            switch (offset)
            {
                case SingleEventOffset.Before:
                    cmdText.Append("WHERE ((StartTime < ?) AND (ServiceDbId = ?)) " +
                        "ORDER BY StartTime DESC");
                    break;
                case SingleEventOffset.Now:
                    cmdText.Append("WHERE ((StartTime <= ?) AND (ServiceDbId = ?) AND (EndTime > ?))");
                    break;
                case SingleEventOffset.After:
                    cmdText.Append("WHERE ((StartTime >= ?) AND (ServiceDbId = ?)) " +
                        "ORDER BY StartTime ASC");
                    break;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
            cmd.CommandText = cmdText.ToString();

            cmd.Parameters.Add("@StartTime", System.Data.SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@ServiceDbId", System.Data.SqlDbType.Int).Value = serviceDbId;
            cmd.Parameters.Add("@EndTime", System.Data.SqlDbType.DateTime).Value = start;

            return cmd;
        } // GetSingleEventCommand

        private static SqlCeCommand GetFromDateRangeCommand(int serviceDbId, DateTime? start, DateTime? end)
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            var cmdText = new StringBuilder();
            cmdText.Append("SELECT XmlEpgData, XmlEpgDataAlt " +
                "FROM Events ");
            if (start == null)
            {
                if (end == null)
                {
                    cmdText.Append("WHERE (ServiceDbId = ?)");
                }
                else
                {
                    cmdText.Append("WHERE ((StartTime < ?) AND (ServiceDbId = ?)) ");
                } // if-else
            }
            else if (end == null)
            {
                cmdText.Append("WHERE ((StartTime >= ?) AND (ServiceDbId = ?)) ");
            }
            else
            {
                cmdText.Append("WHERE ((StartTime >= ?) AND (StartTime < ?) AND (ServiceDbId = ?)) ");
            } // if-else
            cmdText.Append("ORDER BY StartTime ASC");
            cmd.CommandText = cmdText.ToString();

            if (start != null) cmd.Parameters.Add("@StartTime", System.Data.SqlDbType.DateTime).Value = start.Value.ToUniversalTime();
            if (end != null) cmd.Parameters.Add("@StartTime2", System.Data.SqlDbType.DateTime).Value = end.Value.ToUniversalTime();
            cmd.Parameters.Add("@ServiceDbId", System.Data.SqlDbType.Int).Value = serviceDbId;

            return cmd;
        } // GetFromDateRangeCommand

        #endregion

        public class EpgStatus
        {
            public bool IsUpdating;
            public bool IsUpdated;
            public bool IsNew;
            public bool IsError;
            public DateTime Time;
        } // class EpgStatus

        public static EpgStatus GetStatus(string dbFile)
        {
            using (var cmd = new SqlCeCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 [Status], [Timestamp] FROM [Status] ORDER BY [Timestamp] DESC";
                using (var reader = DbServices.ExecuteReader(dbFile, cmd, CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        var status = new EpgStatus()
                        {
                            Time = reader.GetDateTime(1)
                        };
                        switch (reader.GetInt32(0))
                        {
                            case -1:
                                status.IsError = true; break;
                            case 1:
                                status.IsUpdated = true; break;
                            default:
                                status.IsUpdated = true; break;
                        } // switch

                        return status;
                    }
                    else
                    {
                        return new EpgStatus()
                        {
                            IsNew = true
                        };
                    } // if-else
                } // using reader
            } // using cmd


            throw new NotImplementedException();
        } // GetStatus
    } // static class EpgDbQuery
} // namespace
