using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace USVG {

	static class StringParser {

		//Separa  translate(30) rotate(45 50 50)
		public static List<KeyValuePair<string, string>> StringAttrTransform(string attrs)
		{
			List<KeyValuePair<string, string>> dic = new List<KeyValuePair<string, string>>();
			string pattrn = @"(\w*?)\((.*?)\)";
			foreach (Match match in Regex.Matches(attrs, pattrn)) {
				Debug.Log("Tr( " + match.Groups[1].Value + ") values: " + match.Groups[2].Value);
				dic.Add(new KeyValuePair<string, string>(match.Groups[1].Value, match.Groups[2].Value));
			}
			return dic;
		}

		private static char[] sepCharacters = new char[] { ' ', ',', '\n', '\t', '\r' };
		public static string[] ExtractValuesFromString(string inputText)
		{
			return inputText.Split(sepCharacters, StringSplitOptions.RemoveEmptyEntries);
		}

		public static float[] ConvertAttrNumber( string attrs){
			string[] s_attrs = ExtractValuesFromString(attrs);
			float[] f_attrs = new float[s_attrs.Length];

			for (int it = 0; it < s_attrs.Length; it++) {
				float.TryParse(s_attrs[it], out f_attrs[it]);
			}
			return f_attrs;
		}


		/** Path Parsing Methods */
		public static List<KeyValuePair<string, string>> StringPathSep(string attrs)
		{
			List<KeyValuePair<string, string>> dic = new List<KeyValuePair<string, string>>();
			string pattrn = @"([a-zA-Z])([0-9\.\,\ ]*)";
			foreach (Match match in Regex.Matches(attrs, pattrn)) {
				dic.Add(new KeyValuePair<string, string>(match.Groups[1].Value, match.Groups[2].Value));
			}
			return dic;
		}



	}

}