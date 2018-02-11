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
			string pattrn = @"([a-zA-Z])([0-9\.\,\ \-]*)";
			foreach (Match match in Regex.Matches(attrs, pattrn)) {
				dic.Add(new KeyValuePair<string, string>(match.Groups[1].Value, match.Groups[2].Value));
			}
			return dic;
		}

		public static float[] StringPathValues(string attrs)
		{
			List<string> s_vals = new List<string>();
			string pattrn = @"(-?\d*\.{0,1}\d+)";
			foreach (Match match in Regex.Matches(attrs, pattrn)) {
				s_vals.Add(match.Groups[0].Value);
			}

			float[] f_vals = new float[s_vals.Count];

			for (int it = 0; it < s_vals.Count; it++) {
				float.TryParse(s_vals[it], out f_vals[it]);
			}
			return f_vals;
		}



	}

}