using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public abstract class SVGElement {
		Dictionary<string, string> attrList;

		protected SVGElement(Dictionary<string, string> _attrList) {
			attrList = _attrList;
		}

		protected abstract void GenerateGameObject(Transform parent);
	}

}