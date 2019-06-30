// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;

namespace IpTviewr.Common
{
    public class CommandLineArguments
    {
        public bool SpecialHelpArgument
        {
            get;
            set;
        } // SpecialHelpArgument

        public bool IsHelpRequested
        {
            get { return Switches?.ContainsKey("help") ?? false; }
        } // IsHelpRequested

        public bool IsOk
        {
            get;
            private set;
        } // IsOk

        public IList<string> Arguments
        {
            get;
            private set;
        } // Arguments

        public IDictionary<string, string> Switches
        {
            get;
            private set;
        } // Switches

        public void Parse(string[] args, int startIndex = 0)
        {
            string argName;
            string argValue;

            IsOk = false;

            var arguments = new List<string>(args.Length);
            var switches = new Dictionary<string, string>(args.Length, StringComparer.InvariantCultureIgnoreCase);

            foreach (var arg in args.Skip(startIndex))
            {
                if ((arg[0] == '/') || (arg[0] == '-'))
                {
                    if (arg.Length < 2) // switch name expected
                    {
                        return;
                    } // if

                    if (SpecialHelpArgument)
                    {
                        var partialArg = arg.Substring(1, 1).ToLower();
                        if ((partialArg.StartsWith("h")) || (partialArg.StartsWith("?")))
                        {
                            switches.Add("help", null);
                            break;
                        } // if
                    } // if

                    var pos = arg.IndexOf(':');

                    if (pos < 0)
                    {
                        argName = arg.Substring(1);
                        argValue = null;
                    }
                    else
                    {
                        if (pos == 0) return; // switch name expected

                        argName = arg.Substring(1, pos - 1);
                        argValue = arg.Substring(pos + 1);
                    } // if-else
                    switches[argName] = argValue;
                }
                else
                {
                    arguments.Add(arg);
                } // if-else
            } // foreach arg

            IsOk = true;
            Arguments = arguments;
            Switches = switches;
        } // Parse
    } // class CommandLineArguments
} // namespace
