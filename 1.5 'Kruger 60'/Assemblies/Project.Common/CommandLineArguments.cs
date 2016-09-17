// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            get { return (Arguments != null) ? Arguments.ContainsKey("help") : false; }
        } // IsHelpRequested

        public bool IsOk
        {
            get;
            private set;
        } // IsOk

        public IDictionary<string, string> Arguments
        {
            get;
            private set;
        } // Arguments

        public void Parse(string[] args, int startIndex = 0)
        {
            string argName;
            string argValue;

            IsOk = false;
            
            var arguments = new Dictionary<string, string>(args.Length, StringComparer.InvariantCultureIgnoreCase);

            foreach (var arg in args.Skip(startIndex))
            {
                if ((arg[0] == '/') || (arg[0] == '-'))
                {
                    if (arg.Length < 2) // argument name expected
                    {
                        return;
                    } // if

                    if (SpecialHelpArgument)
                    {
                        var partialArg = arg.Substring(1, 1).ToLower();
                        if ((partialArg.StartsWith("h")) || (partialArg.StartsWith("?")))
                        {
                            arguments.Add("help", null);
                            break;
                        } // if
                    } // if

                    argValue = null;
                    var pos = arg.IndexOf(':');
                    if (pos == 0) // argument name expected
                    {
                        return;
                    }
                    else if (pos > 0)
                    {
                        argName = arg.Substring(1, pos - 1);
                        argValue = arg.Substring(pos + 1);
                    }
                    else
                    {
                        argName = arg;
                    } // if-else

                    arguments[argName] = argValue;
                }
                else
                {
                    return;
                } // if-else
            } // foreach arg

            IsOk = true;
            Arguments = arguments;
        } // Parse
    } // class CommandLineArguments
} // namespace
