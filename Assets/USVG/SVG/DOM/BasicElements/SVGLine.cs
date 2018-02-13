using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public class SVGLine : SVGGeometry {
		private float _x1, _x2, _y1, _y2;

		public float x1 { get { return _x1; } }

		public float x2 { get { return _x2; } }

		public float y1 { get { return _y1; } }

		public float y2 { get { return _y2; } }

		public SVGLine(Dictionary<string, string> _attrList) : base(_attrList)
		{
			string x1, x2, y1, y2;
			_attrList.TryGetValue("x1", out x1);
			_attrList.TryGetValue("x2", out x2);
			_attrList.TryGetValue("y1", out y1);
			_attrList.TryGetValue("y2", out y2);

			_x1 = float.Parse(x1);
			_x2 = float.Parse(x2);
			_y1 = float.Parse(y1);
			_y2 = float.Parse(y2);
			
		}

		public override void Render(SVGElement parent, Material baseMaterial)
		{
			Debug.LogError("No Implementado!");
		}
	}

}