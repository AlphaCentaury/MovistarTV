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

using System;
using System.Collections.Generic;
using System.Linq;

namespace IpTviewr.Common
{
    public class CommandLineArguments
    {
        private IEqualityComparer<string> _comparer;

        public CommandLineArguments()
        {
            _comparer = StringComparer.InvariantCultureIgnoreCase;
        } // constructor

        public CommandLineArguments(IEqualityComparer<string> comparer)
        {
            _comparer = comparer ?? StringComparer.InvariantCultureIgnoreCase;
        } // constructor

        public bool SpecialHelpArgument
        {
            get;
            set;
        } // SpecialHelpArgument

        public bool IsHelpRequested => Switches?.ContainsKey("help") ?? false;

        public bool IsOk { get; private set; }

        public IList<string> Arguments { get; private set; }

        public IDictionary<string, string> Switches { get; private set; }

        public IDictionary<string, IList<string>> MultiValueSwitches { get; private set; }

        public void Parse(string[] args, int startIndex = 0)
        {
            Parse((IReadOnlyCollection<string>)args, startIndex);
        } // Parse

        public void Parse(IReadOnlyList<string> args, int startIndex = 0)
        {
            Parse((IReadOnlyCollection<string>)args, startIndex);
        } // Parse

        public void Parse(IReadOnlyCollection<string> args, int startIndex = 0)
        {
            IsOk = false;

            Arguments = new List<string>(args.Count);
            Switches = new Dictionary<string, string>(args.Count, _comparer);
            MultiValueSwitches = new Dictionary<string, IList<string>>(args.Count, _comparer);

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
                            Switches.Add("help", null);
                            break;
                        } // if
                    } // if

                    string argName;
                    string argValue;

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
                    AddToSwitches(argName, argValue);
                }
                else
                {
                    Arguments.Add(arg);
                } // if-else
            } // foreach arg

            IsOk = true;
            if (Switches.Count == 0) Switches = new EmptyDictionary<string, string>();
            if (MultiValueSwitches.Count == 0) MultiValueSwitches = new EmptyDictionary<string, IList<string>>();
        } // Parse

        private void AddToSwitches(string argName, string argValue)
        {
            if (MultiValueSwitches.ContainsKey(argName))
            {
                MultiValueSwitches[argName].Add(argValue);
            }
            else
            {
                MultiValueSwitches.Add(argName, new List<string>
                {
                    argValue
                });

                // keep first value; do not override with later values
                if (!Switches.ContainsKey(argName))
                {
                    Switches[argName] = argValue;
                } // if
            } // if-else
        } // AddToSwitches
    } // class CommandLineArguments
} // namespace
