using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace USVG {
	public class SVGRect : SVGGeometry {
		private const int nSegments = 20;

		private readonly float _x, _y, _width, _height, _rx, _ry;

		public float x { get { return _x; } }

		public float y { get { return _y; } }

		public float width { get { return _width; } }

		public float height { get { return _height; } }

		public float rx { get { return _rx; } }

		public float ry { get { return _ry; } }

		public SVGRect(Dictionary<string, string> _attrList) : base(_attrList)
		{
			string x, y, width, height, rx, ry;
			_attrList.TryGetValue("x", out x);
			_attrList.TryGetValue("y", out y);
			_attrList.TryGetValue("width", out width);
			_attrList.TryGetValue("height", out height);
			_attrList.TryGetValue("rx", out rx);
			_attrList.TryGetValue("ry", out ry);

			float.TryParse(x, out _x);
			float.TryParse(y, out _y);
			float.TryParse(width, out _width);
			float.TryParse(height, out _height);
			float.TryParse(rx, out _rx);
			float.TryParse(ry, out _ry);

		}

		public override void Render(SVGElement parent, Material baseMaterial)
		{

			if(_rx != 0 || _ry != 0){
				//FIXME: aca tiene que ser un rect rounded
				vectors_2d = GeometryTools.CreateRoundedRectangle(_x, _y, _width, _height, _rx, _ry, nSegments);
			} else{
				vectors_2d = GeometryTools.CreateRectangle(_x, _y, _width, _height);
			}

			base.Render(parent, baseMaterial);

		}
	}
}
