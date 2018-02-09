using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public class SVGSVG : SVGElement, ISVGContainer {
		List<SVGElement> childs;

		public SVGSVG(Dictionary<string, string> _attrList) : base(_attrList){
			childs = new List<SVGElement>();
		}

		protected override void GenerateGameObject(Transform parent)
		{
			Debug.LogError("No Implementado!");
		}
		

		public void addChild(SVGElement child)
		{
			childs.Add(child);
		}

		public void addChildren(List<SVGElement> children)
		{
			childs.AddRange(children);
		}
	}

}