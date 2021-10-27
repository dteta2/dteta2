using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// .NET C# equivalent to PHP preg_match and preg_replace
/// </summary>
/// 

namespace gedcomtodatabase_c
{
    public static class RegexExt
    {
        /// <summary>
        /// Equivalent to PHP preg_match but only for 3 requied parameters
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <param name="matches"></param>
        /// <returns></returns>
        public static bool PregMatch(this Regex regex, string input, out List<string> matches)
        //public static bool PregMatch(string regex, string input, out List<string> matches)
        {
            var match = regex.Match(input);
            var groups = (from object g in match.Groups select g.ToString()).ToList();

            matches = groups;
            return match.Success;
        }


        /// <summary>
        /// Equivalent to PHP preg_replace
        /// <see cref="http://stackoverflow.com/questions/166855/c-sharp-preg-replace"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacements"></param>
        /// <returns></returns>
        public static string PregReplace(this string input, string pattern, string replacements)
        {
            if (replacements.Length != pattern.Length)
                throw new System.ArgumentException("Replacement and Pattern must be balanced");

            for (var i = 0; i < pattern.Length; i++)
            {
                input = Regex.Replace(input, pattern.Substring(i,1), replacements.Substring(i,1));
            }

            return input;
        }

        /// <summary>
        /// Equivalent to PHP preg_replace
        /// <see cref="http://stackoverflow.com/questions/166855/c-sharp-preg-replace"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacements"></param>
        /// <returns></returns>
        public static string PregReplaceA(this string input, string[] pattern, string[] replacements)
        {
            if (replacements.Length != pattern.Length)
                throw new System.ArgumentException("Replacement and Pattern Arrays must be balanced");

            for (var i = 0; i < pattern.Length; i++)
            {
                input = Regex.Replace(input, pattern[i], replacements[i]);
            }

            return input;
        }
    }
}