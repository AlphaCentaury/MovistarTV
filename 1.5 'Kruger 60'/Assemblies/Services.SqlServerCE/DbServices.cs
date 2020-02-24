// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using IpTviewr.Common.Serialization;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;

namespace IpTviewr.Services.SqlServerCE
{
    public class DbServices
    {
        public delegate TData LoadDataAction<TDbData, TData>(TDbData deserializedData, SqlCeDataReader reader);
            
        public static T Load<T>(string dbFile, SqlCeCommand loadCommand, string xmlDataColumnName) where T : class
        {
            using (var cn = GetConnection(dbFile))
            {
                var result = Load<T>(cn, loadCommand, xmlDataColumnName);
                cn.Close();

                return result;
            } // using cn
        } // Load<T>

        public static T Load<T>(SqlCeConnection cn, SqlCeCommand loadCommand, string xmlDataColumnName) where T : class
        {
            byte[] data;

            using (var cmd = loadCommand)
            {
                if (cn != null) loadCommand.Connection = cn;
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                {
                    var index = reader.GetOrdinal(xmlDataColumnName);
                    reader.Read();
                    var value = reader.GetValue(index);
                    data = (byte[])value;
                } // using reader
            } // using cmd

            using (var memory = new MemoryStream(data))
            {
                return XmlSerialization.Deserialize<T>(memory);
            } // using
        } // Load<T>

        public static T LoadData<T>(SqlCeDataReader reader, int dataIndex, int dataAltIdex) where T : class
        {
            if (!reader.IsDBNull(0))
            {
                var data = reader.GetSqlBinary(dataIndex).Value;
                return XmlSerialization.Deserialize<T>(data);
            }
            else if (!reader.IsDBNull(1))
            {
                var data = reader.GetSqlBinary(dataAltIdex).Value;
                return XmlSerialization.Deserialize<T>(data);
            }
            else
            {
                return null;
            } // if-else
        } // LoadData<T>

        public static TData LoadData<TDbData, TData>(SqlCeDataReader reader, int dataIndex, int dataAltIdex, LoadDataAction<TDbData, TData> load)
            where TDbData : class
            where TData : class
        {
            var data = LoadData<TDbData>(reader, dataIndex, dataAltIdex);
            return load(data, reader);
        } // LoadData<T>

        public static int Save<T>(string dbFile, SqlCeCommand saveCommand, string xmlDataParameterName, T obj, bool disposeCommand = true) where T : class
        {
            using (var cn = GetConnection(dbFile))
            {
                var result = Save<T>(cn, saveCommand, xmlDataParameterName, obj, disposeCommand);
                cn.Close();

                return result;
            } // using cn
        } // Save<T>

        /// <summary>
        /// Serializes an object as XML data and stores it in the given database
        /// </summary>
        /// <typeparam name="T">Type of the object to save</typeparam>
        /// <param name="cn">The connection to the database</param>
        /// <param name="saveCommand">The command to excute a save action</param>
        /// <param name="xmlDataParameterName">Name of the parameter to store the data</param>
        /// <param name="obj">The object to save as XML data</param>
        /// <param name="disposeCommand">Indicates if the command is to be disposed after its execution or not</param>
        /// <returns>Number of rows affected</returns>
        public static int Save<T>(SqlCeConnection cn, SqlCeCommand saveCommand, string xmlDataParameterName, T obj, bool disposeCommand = true) where T : class
        {
            byte[] data;

            if (obj == null)
            {
                data = null;
            }
            else
            {
                using (var memory = new MemoryStream())
                {
                    XmlSerialization.Serialize<T>(memory, obj);
                    data = memory.ToArray();
                } // using memory
            } // if-else

            if (cn != null) saveCommand.Connection = cn;
            saveCommand.Parameters[xmlDataParameterName].Value = ((object)data) ?? DBNull.Value;

            if (disposeCommand)
            {
                using (var cmd = saveCommand)
                {
                    return cmd.ExecuteNonQuery();
                } // using cmd
            }
            else
            {
                return saveCommand.ExecuteNonQuery();
            } // if-ele
        } // Save<T>

        /// <summary>
        /// Serializes an object as XML data and stores it in the given database
        /// </summary>
        /// <typeparam name="T">Type of the object to save</typeparam>
        /// <param name="cn">The connection to the database</param>
        /// <param name="saveCommand">The command to excute a save action</param>
        /// <param name="xmlDataParameterName">Name of the parameter to store the data</param>
        /// <param name="xmlDataAlternativeParameterName">Name of the parameter to alternatively store the data</param>
        /// <param name="obj">The object to save as XML data</param>
        /// <param name="disposeCommand">Indicates if the command is to be disposed after its execution or not</param>
        /// <returns>Number of rows affected</returns>
        /// <remarks>
        /// This method allows to overcome a SQL Server CE 4.0 limitation/problem/issue.
        /// Two columns are required. One must be a varbinary/binary and the second one an image.
        /// If the serialized XML data fits in the first column, data is stored there; otherwise its stored in
        /// the image column, and the other column is set to DBNull.
        /// This allows to minimize database size, as image columns use at least one page (4096 bytes),
        /// even for storing a single byte of information, as CE trims only at the page level.
        /// </remarks>
        public static int Save<T>(SqlCeConnection cn, SqlCeCommand saveCommand, string xmlDataParameterName, string xmlDataAlternativeParameterName, T obj, bool disposeCommand = true) where T : class
        {
            byte[] data;

            if (obj == null)
            {
                data = null;
            }
            else
            {
                using (var memory = new MemoryStream())
                {
                    XmlSerialization.Serialize<T>(memory, obj);
                    data = memory.ToArray();
                } // using memory
            } // if-else

            if (cn != null) saveCommand.Connection = cn;
            if (data == null)
            {
                saveCommand.Parameters[xmlDataParameterName].Value = DBNull.Value;
                saveCommand.Parameters[xmlDataAlternativeParameterName].Value = DBNull.Value;
            }
            else
            {
                var dataParam = saveCommand.Parameters[xmlDataParameterName];
                if (data.Length <= dataParam.Size)
                {
                    dataParam.Value = data;
                    saveCommand.Parameters[xmlDataAlternativeParameterName].Value = DBNull.Value;
                }
                else
                {
                    dataParam.Value = DBNull.Value;
                    saveCommand.Parameters[xmlDataAlternativeParameterName].Value = data;
                } // if-else
            } // if-else

            if (disposeCommand)
            {
                using (var cmd = saveCommand)
                {
                    return cmd.ExecuteNonQuery();
                } // using cmd
            }
            else
            {
                return saveCommand.ExecuteNonQuery();
            } // if-ele
        } // Save<T>

        public static int Save<T1, T2>(string dbFile, SqlCeCommand saveCommand, string xmlData1ParameterName, T1 obj1, string xmlData2ParameterName, T2 obj2, bool disposeCommand = true)
            where T1 : class
            where T2 : class
        {
            using (var cn = GetConnection(dbFile))
            {
                var result = Save<T1, T2>(cn, saveCommand, xmlData1ParameterName, obj1, xmlData2ParameterName, obj2, disposeCommand);
                cn.Close();

                return result;
            } // using cn
        } // Save<T1, T2>

        public static int Save<T1, T2>(SqlCeConnection cn, SqlCeCommand saveCommand, string xmlData1ParameterName, T1 obj1, string xmlData2ParameterName, T2 obj2, bool disposeCommand = true)
            where T1 : class
            where T2 : class
        {
            byte[] data1, data2;

            if (obj1 == null)
            {
                data1 = null;
            }
            else
            {
                using (var memory = new MemoryStream())
                {
                    XmlSerialization.Serialize<T1>(memory, obj1);
                    data1 = memory.ToArray();
                } // using memory
            } // if

            if (obj2 == null)
            {
                data2 = null;
            }
            else
            {
                using (var memory = new MemoryStream())
                {
                    XmlSerialization.Serialize<T2>(memory, obj2);
                    data2 = memory.ToArray();
                } // using memory
            } // if-else

            if (cn != null) saveCommand.Connection = cn;
            saveCommand.Parameters[xmlData1ParameterName].Value = ((object)data1) ?? DBNull.Value;
            saveCommand.Parameters[xmlData2ParameterName].Value = ((object)data2) ?? DBNull.Value;

            if (disposeCommand)
            {
                using (var cmd = saveCommand)
                {

                    return cmd.ExecuteNonQuery();
                } // using cmd
            }
            else
            {
                return saveCommand.ExecuteNonQuery();
            } // if-else
        } // Save<T1,T2>

        public static SqlCeConnection GetConnection(string dbFile)
        {
            SqlCeConnectionStringBuilder builder;

            builder = new SqlCeConnectionStringBuilder
            {
                DataSource = dbFile,
                Password = "movistartv.codeplex.com"
            };

            var cn = new SqlCeConnection(builder.ConnectionString);
            cn.Open();

            return cn;
        } // GetConnection

        public static int Execute(string dbFile, SqlCeCommand cmd)
        {
            using (var cn = GetConnection(dbFile))
            {
                var result = Execute(cn, cmd);
                cn.Close();

                return result;
            } // using cn
        } // Execute

        public static int Execute(SqlCeConnection cn, SqlCeCommand cmd)
        {
            if (cn != null) cmd.Connection = cn;

            return cmd.ExecuteNonQuery();
        } // Execute

        public static SqlCeDataReader ExecuteReader(string dbFile, SqlCeCommand cmd, CommandBehavior options = CommandBehavior.Default)
        {
            var cn = GetConnection(dbFile);
            cmd.Connection = cn;
            return cmd.ExecuteReader(CommandBehavior.CloseConnection | options);
        } // ExecuteReader

        public static SqlCeDataReader ExecuteReader(SqlCeConnection cn, SqlCeCommand cmd, CommandBehavior options = CommandBehavior.Default)
        {
            if (cn != null) cmd.Connection = cn;

            return cmd.ExecuteReader(options);
        } // ExecuteReader
    } // class DbServices
} // namespace
