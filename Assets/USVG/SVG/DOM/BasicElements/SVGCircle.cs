using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public class SVGCircle : SVGElement {
		private float _cx, _cy, _r;

		public float cx { get { return _cx; } }

		public float cy { get { return _cy; } }

		public float r { get { return _r; } }

		public SVGCircle(Dictionary<string, string> _attrList) : base(_attrList)
		{
			string cx, cy, r;
			_attrList.TryGetValue("cx", out cx);
			_attrList.TryGetValue("cy", out cy);
			_attrList.TryGetValue("r", out r);

			_cx = float.Parse(cx);
			_cy = float.Parse(cy);
			_r = float.Parse(r);
		}

		protected override void GenerateGameObject(Transform parent)
		{
			Debug.LogError("No Implementado!");
		}
	}

}