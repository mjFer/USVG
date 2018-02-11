using System;
using System.Collections.Generic;
using UnityEngine;

namespace USVG {

	class GenPath{
		public Vector2[] points;
		public bool closed;
	};


	public class SVGPath : SVGGeometry {
		List<SVGPathSeg> segList;
		List<Vector2> generatedPoints;
		List<GenPath> Paths;

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
			int i = 0;
			Paths = new List<GenPath>();
			generatedPoints = new List<Vector2>();
			foreach(SVGPathSeg seg in segList){
				i++;
				Vector2[] newPoints = seg.GetPoints(20);
				if(newPoints != null) generatedPoints.AddRange(newPoints);

				if(seg.GetType() == typeof(SVGPathSegClose)){
					GenPath path = new GenPath();
					path.points = generatedPoints.ToArray();
					path.closed = true;
					Paths.Add(path);
					generatedPoints.Clear();
				}
				
			}

			if(generatedPoints.Count>0){
				GenPath path = new GenPath();
				path.points = generatedPoints.ToArray();
				path.closed = false;
				Paths.Add(path);
				generatedPoints.Clear();
			}

			//foreach (Vector2 vec in generatedPoints) {
			//	Debug.Log(name + "vector:" + vec);
			//}


			vectors_2d = Paths[0].points;

			base.Render(parent);
		}


		private void ParsePathType(char type, string values){
			float[] vals = StringParser.StringPathValues(values);

			SVGPathSeg last = null;
			if (segList.Count > 0)
				last = segList[segList.Count - 1];

			switch (type){
				case 'm':
				case 'M':
					segList.Add( new SVGPathSegMoveTo(vals[0], vals[1], type == 'm' ? true : false, last));
					break;
				case 'l':
				case 'L':
					segList.Add(new SVGPathSegLineTo(vals[0], vals[1], type == 'l' ? true : false, last));
					break;
				case 'H':
				case 'h':
					segList.Add(new SVGPathSegLineTo(vals[0], 0, type == 'h' ? true : false, last));
					break;
				case 'V':
				case 'v':
					segList.Add(new SVGPathSegLineTo(0, vals[0], type == 'v' ? true : false, last));
					break;
				case 'C':
				case 'c':
					segList.Add(new SVGPathSegCubicBezTo(vals[0], vals[1], vals[2], vals[3], vals[4], vals[5], type == 'c' ? true : false, last));
					break;
				case 'S':
				case 's':
					segList.Add(new SVGPathSegCubicBezShortTo(vals[0], vals[1], vals[2], vals[3], type == 's' ? true : false, last));
					break;
				case 'Q':
				case 'q':
					segList.Add(new SVGPathSegQuadBezTo(vals[0], vals[1], vals[2], vals[3], type == 'q' ? true : false, last));
					break;
				case 'T':
				case 't':
					segList.Add(new SVGPathSegQuadBezShortTo(vals[0], vals[1], type == 't' ? true : false, last));
					break;
				case 'Z':
				case 'z':
					segList.Add(new SVGPathSegClose( type == 'z' ? true : false, last));
					break;
				default:
					break;
			}
		}

	}
}