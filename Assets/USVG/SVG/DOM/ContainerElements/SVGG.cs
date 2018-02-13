using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public class SVGG : SVGElement, ISVGContainer {
		List<SVGElement> childs;

		public SVGG(Dictionary<string, string> _attrList) : base(_attrList){
			childs = new List<SVGElement>();
		}

		public override void Render(SVGElement parent, Material baseMaterial)
		{
			if (_gameobject == null) {
				_gameobject = new GameObject(name);
				if (parent != null)
					_gameobject.transform.parent = parent.gameObject.transform;
			}

			foreach (SVGElement child in childs){
				child.Render(this, baseMaterial);
			}
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