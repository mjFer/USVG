using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace USVG {
	public class SVGPolyline : SVGGeometry {
		private List<Vector2> _listPoints;

		public List<Vector2> listPoints { get { return _listPoints; } }

		public SVGPolyline(Dictionary<string, string> _attrList) : base(_attrList)
		{
			string stringPoints;
			_attrList.TryGetValue("points", out stringPoints);
			_listPoints = ExtractPoints(stringPoints);
		}

		private static List<Vector2> ExtractPoints(string inputText)
		{
			List<Vector2> _return = new List<Vector2>();
			string[] _lstStr = StringParser.ExtractValuesFromString(inputText);

			int len = _lstStr.Length;

			for (int i = 0; i < len - 1; i++) {
				string value1 = _lstStr[i];
				string value2 = _lstStr[i + 1];
				float _length1 = float.Parse(value1);
				float _length2 = float.Parse(value2);
				Vector2 _point = new Vector2(_length1, _length2);
				_return.Add(_point);
				i++;
			}
			return _return;
		}


		public override void Render(SVGElement parent)
		{
			vectors_2d = _listPoints.ToArray();

			base.Render(parent);
		}
	}

}