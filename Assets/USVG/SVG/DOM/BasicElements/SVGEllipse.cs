using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public class SVGEllipse : SVGGeometry {
		private const int nSegments = 20;

		private float _cx, _cy, _rx, _ry;

		public float cx { get { return _cx; } }

		public float cy { get { return _cy; } }

		public float rx { get { return _rx; } }

		public float ry { get { return _ry; } }

		public SVGEllipse(Dictionary<string, string> _attrList) : base(_attrList)
		{
			string cx, cy, rx, ry;
			_attrList.TryGetValue("cx", out cx);
			_attrList.TryGetValue("cy", out cy);
			_attrList.TryGetValue("rx", out rx);
			_attrList.TryGetValue("ry", out ry);

			_cx = float.Parse(cx);
			_cy = float.Parse(cy);
			_rx = float.Parse(rx);
			_ry = float.Parse(ry);
		}

		public override void Render(SVGElement parent, Material baseMaterial, onRenderCallback cb)
		{
			vectors_2d = GeometryTools.CreateEllipse(_cx, _cy, _rx, _ry, nSegments);

			base.Render(parent, baseMaterial, cb);
		}
	}

}