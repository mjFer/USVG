using System;
using System.Collections.Generic;
using UnityEngine;

namespace USVG {
	public class SVGPath : SVGGeometry {
		List<SVGPathSeg> segList;

		public SVGPath(Dictionary<string, string> _attrList) : base(_attrList)
		{
			segList = new List<SVGPathSeg>();

			if (_attrList.ContainsKey("d")){
				Debug.Log(_attrList["d"]);
				List<KeyValuePair<string, string>> dic = StringParser.StringPathSep(_attrList["d"]);
				foreach(KeyValuePair<string, string> keypar in dic){
					ParsePathType(keypar.Key[0], keypar.Value);
				}
			}

		}

		public override void Render(SVGElement parent)
		{
			Debug.LogError("No Implementado!");
		}


		private void ParsePathType(char type, string values){
			float[] vals = StringParser.ConvertAttrNumber(values);

			switch (type){
				case 'm':
				case 'M':
					segList.Add( new SVGPathSegMoveTo(vals[0], vals[1], type == 'm' ? true : false));
					break;
				case 'l':
				case 'L':
					segList.Add(new SVGPathSegLineTo(vals[0], vals[1], type == 'l' ? true : false));
					break;
				case 'H':
				case 'h':
					segList.Add(new SVGPathSegLineTo(vals[0], 0, type == 'h' ? true : false));
					break;
				case 'V':
				case 'v':
					segList.Add(new SVGPathSegLineTo(0, vals[1], type == 'v' ? true : false));
					break;
				case 'C':
				case 'c':
					segList.Add(new SVGPathSegCubicBezTo(vals[0], vals[1], vals[2], vals[3], vals[4], vals[5], type == 'c' ? true : false));
					break;
				case 'S':
				case 's':
					segList.Add(new SVGPathSegCubicBezShortTo(vals[0], vals[1], vals[2], vals[3], type == 's' ? true : false));
					break;
				case 'Q':
				case 'q':
					segList.Add(new SVGPathSegQuadBezTo(vals[0], vals[1], vals[2], vals[3], type == 'q' ? true : false));
					break;
				case 'T':
				case 't':
					segList.Add(new SVGPathSegQuadBezShortTo(vals[0], vals[1], type == 't' ? true : false));
					break;
				default:
					break;
			}
		}

	}
}