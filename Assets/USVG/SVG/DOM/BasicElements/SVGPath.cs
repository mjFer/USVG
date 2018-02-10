using System;
using System.Collections.Generic;
using UnityEngine;

namespace USVG {
	public class SVGPath : SVGGeometry {

		public SVGPath(Dictionary<string, string> _attrList) : base(_attrList)
		{

		}

		public override void Render(SVGElement parent)
		{
			Debug.LogError("No Implementado!");
		}

	}
}