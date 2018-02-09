using System;
using System.Collections.Generic;
using UnityEngine;

namespace USVG {
	public class SVGPath : SVGElement {

		public SVGPath(Dictionary<string, string> _attrList) : base(_attrList)
		{

		}

		protected override void GenerateGameObject(Transform parent)
		{
			Debug.LogError("No Implementado!");
		}

	}
}