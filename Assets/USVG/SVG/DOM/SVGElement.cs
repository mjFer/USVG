using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public abstract class SVGElement {
		Dictionary<string, string> attrList;
		SVGTransformList transformlist;
		protected string name;

		protected GameObject _gameobject;

		public GameObject gameObject{
			get { return _gameobject; }
		}

		protected SVGElement(Dictionary<string, string> _attrList) {
			attrList = _attrList;

			if(attrList.ContainsKey("id")){
				name = attrList["id"];
			}else{
				name = this.ToString() + "-" + SVGGenerals.getElementId();
			}


			foreach (KeyValuePair<string,string> attr in attrList){
				switch(attr.Key){
					case "transform":
						transformlist = new SVGTransformList();
						transformlist.ParseAttributes(attr.Value);
						break;
					default:
						Debug.Log("Attributo no implementado: " + attr.Key);
						break;
				}
			}

		}


		public abstract void Render(SVGElement parent);
	}

}