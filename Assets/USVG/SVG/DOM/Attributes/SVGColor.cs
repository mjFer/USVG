using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

public class SVGColor {
	float _r;
	float _g;
	float _b;

	public SVGColor(string atribute){
		if(_colorNames == null){
			_colorNames = new Dictionary<string, string>();
			colorNamesGen();
		}
		ParseColor(atribute);

	}

	public float R {get {return _r;}}
	public float G {get {return _g;}}
	public float B {get {return _b;}}

	//matchea rgb(205,133,63)
	string pattern1 = @"rgb\((\d*?),(\d*?),(\d*?)\)";
	//matchea rgb(80.392%, 52.157%, 24.706%)
	string pattern2 = @"rgb\((.*?)%,(.*?)%,(.*?)%\)";
	private void ParseColor(string atr){
		Regex r = new Regex(pattern1);
		Regex r2 = new Regex(pattern2);
		if (r.IsMatch(atr)) {
			Match match = r.Match(atr);
			float.TryParse(match.Groups[0].Value, out _r);
			float.TryParse(match.Groups[1].Value, out _g);
			float.TryParse(match.Groups[2].Value, out _b);
		} else if (r2.IsMatch(atr)) {
			Match match = r.Match(atr);
			float.TryParse(match.Groups[0].Value, out _r);
			float.TryParse(match.Groups[1].Value, out _g);
			float.TryParse(match.Groups[2].Value, out _b);
			_r /= 100.0f;
			_g /= 100.0f;
			_b /= 100.0f;
		} else if (atr[0] == '#') {
			if (atr.Length == 4) {
				//ejemplo #FFF
				ParseHex3(atr.Substring(1));
			} else if (atr.Length == 7) {
				//ejemplo #FFFFFF
				ParseHex6(atr.Substring(1));
			}
		} else if (atr.StartsWith("url")) {
			Debug.LogWarning("url not implemented");
		} else {
			//ejemplo red
			string hexCode = _colorNames[atr];
			ParseHex6(hexCode);
		}

	}


	private int HexToInt(char hexChar)
	{
		switch (hexChar) {
			case '0': return 0;
			case '1': return 1;
			case '2': return 2;
			case '3': return 3;
			case '4': return 4;
			case '5': return 5;
			case '6': return 6;
			case '7': return 7;
			case '8': return 8;
			case '9': return 9;
			case 'A': return 10;
			case 'B': return 11;
			case 'C': return 12;
			case 'D': return 13;
			case 'E': return 14;
			case 'F': return 15;
		}
		return 0;
	}

	private void ParseHex6(string hex)
	{
		_r = (HexToInt(hex[1]) + HexToInt(hex[0]) * 16.000f) / 255.0f;
		_g = (HexToInt(hex[3]) + HexToInt(hex[2]) * 16.000f) / 255.0f;
		_b = (HexToInt(hex[5]) + HexToInt(hex[4]) * 16.000f) / 255.0f;	
	}

	private void ParseHex3(string hex)
	{
		_r = HexToInt(hex[0]) / 16.0f;
		_g = HexToInt(hex[1]) / 16.0f;
		_b = HexToInt(hex[2]) / 16.0f;
	}

	private static Dictionary<string, string> _colorNames = null;
	private static void colorNamesGen(){
		_colorNames.Add("aliceblue", "F0F8FF");
		_colorNames.Add("antiquewhite", "FAEBD7");
		_colorNames.Add("aqua", "00FFFF");
		_colorNames.Add("aquamarine", "7FFFD4");
		_colorNames.Add("azure", "F0FFFF");
		_colorNames.Add("beige", "F5F5DC");
		_colorNames.Add("bisque", "FFE4C4");
		_colorNames.Add("black", "000000");
		_colorNames.Add("blanchedalmond", "FFEBCD");
		_colorNames.Add("blue", "0000FF");
		_colorNames.Add("blueviolet", "8A2BE2");
		_colorNames.Add("brown", "A52A2A");
		_colorNames.Add("burlywood", "DEB887");
		_colorNames.Add("cadetblue", "5F9EA0");
		_colorNames.Add("chartreuse", "7FFF00");
		_colorNames.Add("chocolate", "D2691E");
		_colorNames.Add("coral", "FF7F50");
		_colorNames.Add("cornflowerblue", "6495ED");
		_colorNames.Add("cornsilk", "FFF8DC");
		_colorNames.Add("crimson", "DC143C");
		_colorNames.Add("cyan", "00FFFF");
		_colorNames.Add("darkblue", "00008B");
		_colorNames.Add("darkcyan", "008B8B");
		_colorNames.Add("darkgoldenrod", "B8860B");
		_colorNames.Add("darkgray", "A9A9A9");
		_colorNames.Add("darkgreen", "006400");
		_colorNames.Add("darkkhaki", "BDB76B");
		_colorNames.Add("darkmagenta", "8B008B");
		_colorNames.Add("darkolivegreen", "556B2F");
		_colorNames.Add("darkorange", "FF8C00");
		_colorNames.Add("darkorchid", "9932CC");
		_colorNames.Add("darkred", "8B0000");
		_colorNames.Add("darksalmon", "E9967A");
		_colorNames.Add("darkseagreen", "8FBC8F");
		_colorNames.Add("darkslateblue", "483D8B");
		_colorNames.Add("darkslategray", "2F4F4F");
		_colorNames.Add("darkturquoise", "00CED1");
		_colorNames.Add("darkviolet", "9400D3");
		_colorNames.Add("deeppink", "FF1493");
		_colorNames.Add("deepskyblue", "00BFFF");
		_colorNames.Add("dimgray", "696969");
		_colorNames.Add("dimgrey", "696969");
		_colorNames.Add("dodgerblue", "1E90FF");
		_colorNames.Add("firebrick", "B22222");
		_colorNames.Add("floralwhite", "FFFAF0");
		_colorNames.Add("forestgreen", "228B22");
		_colorNames.Add("fuchsia", "FF00FF");
		_colorNames.Add("gainsboro", "DCDCDC");
		_colorNames.Add("ghostwhite", "F8F8FF");
		_colorNames.Add("gold", "FFD700");
		_colorNames.Add("goldenrod", "DAA520");
		_colorNames.Add("gray", "808080");
		_colorNames.Add("green", "008000");
		_colorNames.Add("greenyellow", "ADFF2F");
		_colorNames.Add("honeydew", "F0FFF0");
		_colorNames.Add("hotpink", "FF69B4");
		_colorNames.Add("indianred", "CD5C5C");
		_colorNames.Add("indigo", "4B0082");
		_colorNames.Add("ivory", "FFFFF0");
		_colorNames.Add("khaki", "F0E68C");
		_colorNames.Add("lavender", "E6E6FA");
		_colorNames.Add("lavenderblush", "FFF0F5");
		_colorNames.Add("lawnGreen", "7CFC00");
		_colorNames.Add("lemonchiffon", "FFFACD");
		_colorNames.Add("lightblue", "ADD8E6");
		_colorNames.Add("lightcoral", "F08080");
		_colorNames.Add("lightcyan", "E0FFFF");
		_colorNames.Add("lightgoldenrodyellow", "FAFAD2");
		_colorNames.Add("lightgray", "D3D3D3");
		_colorNames.Add("lightgreen", "90EE90");
		_colorNames.Add("lightpink", "FFB6C1");
		_colorNames.Add("lightsalmon", "FFA07A");
		_colorNames.Add("lightseagreen", "20B2AA");
		_colorNames.Add("lightskyblue", "87CEFA");
		_colorNames.Add("lightslategray", "778899");
		_colorNames.Add("lightsteelblue", "B0C4DE");
		_colorNames.Add("lightyellow", "FFFFE0");
		_colorNames.Add("lime", "00FF00");
		_colorNames.Add("limegreen", "32CD32");
		_colorNames.Add("linen", "FAF0E6");
		_colorNames.Add("magenta", "FF00FF");
		_colorNames.Add("maroon", "800000");
		_colorNames.Add("mediumaquamarine", "66CDAA");
		_colorNames.Add("mediumblue", "0000CD");
		_colorNames.Add("mediumorchid", "BA55D3");
		_colorNames.Add("mediumpurple", "9370DB");
		_colorNames.Add("mediumseagreen", "3CB371");
		_colorNames.Add("mediumslateblue", "7B68EE");
		_colorNames.Add("mediumspringgreen", "00FA9A");
		_colorNames.Add("mediumturquoise", "48D1CC");
		_colorNames.Add("mediumvioletred", "C71585");
		_colorNames.Add("midnightblue", "191970");
		_colorNames.Add("mintcream", "F5FFFA");
		_colorNames.Add("mistyrose", "FFE4E1");
		_colorNames.Add("moccasin", "FFE4B5");
		_colorNames.Add("navajowhite", "FFDEAD");
		_colorNames.Add("navy", "000080");
		_colorNames.Add("oldlace", "FDF5E6");
		_colorNames.Add("olive", "808000");
		_colorNames.Add("olivedrab", "6B8E23");
		_colorNames.Add("orange", "FFA500");
		_colorNames.Add("orangered", "FF4500");
		_colorNames.Add("orchid", "DA70D6");
		_colorNames.Add("palegoldenrod", "EEE8AA");
		_colorNames.Add("palegreen", "98FB98");
		_colorNames.Add("paleturquoise", "AFEEEE");
		_colorNames.Add("palevioletred", "DB7093");
		_colorNames.Add("papayawhip", "FFEFD5");
		_colorNames.Add("peachpuff", "FFDAB9");
		_colorNames.Add("peru", "CD853F");
		_colorNames.Add("pink", "FFC0CB");
		_colorNames.Add("plum", "DDA0DD");
		_colorNames.Add("powderblue", "B0E0E6");
		_colorNames.Add("purple", "800080");
		_colorNames.Add("red", "FF0000");
		_colorNames.Add("rosybrown", "BC8F8F");
		_colorNames.Add("royalblue", "4169E1");
		_colorNames.Add("saddlebrown", "8B4513");
		_colorNames.Add("salmon", "FA8072");
		_colorNames.Add("sandybrown", "F4A460");
		_colorNames.Add("seagreen", "2E8B57");
		_colorNames.Add("seashell", "FFF5EE");
		_colorNames.Add("sienna", "A0522D");
		_colorNames.Add("silver", "C0C0C0");
		_colorNames.Add("skyblue", "87CEEB");
		_colorNames.Add("slateblue", "6A5ACD");
		_colorNames.Add("slategray", "708090");
		_colorNames.Add("snow", "FFFAFA");
		_colorNames.Add("springgreen", "00FF7F");
		_colorNames.Add("steelblue", "4682B4");
		_colorNames.Add("tan", "D2B48C");
		_colorNames.Add("teal", "008080");
		_colorNames.Add("thistle", "D8BFD8");
		_colorNames.Add("tomato", "FF6347");
		_colorNames.Add("turquoise", "40E0D0");
		_colorNames.Add("violet", "EE82EE");
		_colorNames.Add("wheat", "F5DEB3");
		_colorNames.Add("white", "FFFFFF");
		_colorNames.Add("whitesmoke", "F5F5F5");
		_colorNames.Add("yellow", "FFFF00");
		_colorNames.Add("yellowgreen", "9ACD32");
	}
}
