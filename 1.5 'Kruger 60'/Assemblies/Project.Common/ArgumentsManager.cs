// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.Common
{
    public static class ArgumentsManager
    {
        public static string[] ExpandArguments(string[] rawArguments, IDictionary<string, string> parameters, string openBrace, string closeBrace, StringComparison braceComparisonType)
        {
            string[] result;

            if (rawArguments == null) return null;

            result = new string[rawArguments.Length];
            if (rawArguments.Length == 0) return result;

            Validate(parameters, openBrace, closeBrace);

            for (int index = 0; index < rawArguments.Length; index++)
            {
                result[index] = InternalExpandArgument(rawArguments[index], parameters, openBrace, closeBrace, braceComparisonType);
            } // for

            return result;
        } // ExpandArguments

        public static string ExpandArgument(string rawArgument, IDictionary<string, string> parameters, string openBrace, string closeBrace, StringComparison braceComparisonType)
        {
            Validate(parameters, openBrace, closeBrace);

            return InternalExpandArgument(rawArgument, parameters, openBrace, closeBrace, braceComparisonType);
        } // ExpandArgument

        public static IDictionary<string, string> CreateParameters(string[] keys, string[] values, bool caseSensitive)
        {
            if ((keys == null) || (keys.Length == 0)) throw new ArgumentException();
            if ((values == null) || (values.Length == 0)) throw new ArgumentException();
            if (keys.Length != values.Length) throw new ArgumentException();

            var result = new Dictionary<string, string>(keys.Length, caseSensitive ? StringComparer.CurrentCulture : StringComparer.CurrentCultureIgnoreCase);

            for (int keyIndex = 0, valueIndex = 0; (keyIndex < keys.Length) && (valueIndex < values.Length); keyIndex++, valueIndex++)
            {
                result.Add(keys[keyIndex], values[valueIndex]);
            } // for

            return result;
        } // CreateParameters

        public static string JoinArguments(string[] arguments)
        {
            if ((arguments == null) || (arguments.Length == 0)) return null;

            var length = 0;
            foreach (var argument in arguments)
            {
                length += argument.Length;
                length += 3; // open quote, close quote, space
            } // foreach

            var result = new StringBuilder(length);
            foreach (var argument in arguments)
            {
                result.Append('"');
                result.Append(argument);
                result.Append('"');
                result.Append(' ');
            } // foreach
            result.Remove(result.Length - 1, 1); // remove last space

            return result.ToString();
        } // JoinArguments

        private static void Validate(IDictionary<string, string> parameters, string openBrace, string closeBrace)
        {
            if ((string.IsNullOrEmpty(openBrace)) || (string.IsNullOrEmpty(closeBrace)))
            {
                throw new ArgumentException();
            } // if
            if ((parameters == null) || (parameters.Count == 0))
            {
                throw new ArgumentException();
            } // if
            if ((string.IsNullOrEmpty(openBrace)) || (string.IsNullOrEmpty(closeBrace)))
            {
                throw new ArgumentException();
            } // if
        } // Validate

        private static string InternalExpandArgument(string rawArgument, IDictionary<string, string> parameters, string openBrace, string closeBrace, StringComparison braceComparisonType)
        {
            int index, param;

            if (string.IsNullOrEmpty(rawArgument)) return rawArgument;

            index = rawArgument.IndexOf(openBrace, braceComparisonType);
            if (index < 0) return rawArgument;

            var current = 0;
            var result = new StringBuilder();
            while (index >= 0)
            {
                if (index > current)
                {
                    result.Append(rawArgument.Substring(current, index - current));
                } // if

                param = index + openBrace.Length;
                index = rawArgument.IndexOf(closeBrace, param, braceComparisonType);

                // close 'brace' not found or no param key is specified
                if ((index == param) || (index < 0))
                {
                    throw new ArgumentOutOfRangeException();
                } // if

                var paramName = rawArgument.Substring(param, index - param);
                result.Append(parameters[paramName]);

                current = index + closeBrace.Length;
                index = rawArgument.IndexOf(openBrace, current, braceComparisonType);
            } // while
            if (current < rawArgument.Length)
            {
                result.Append(rawArgument.Substring(current));
            } // if

            return result.ToString();
        } // InternalExpandArgument
    } // class ArgumentsManager
} // namespace
