using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {

	public delegate void onRenderCallback(string name, UnityEngine.Object obj);

	public abstract class SVGElement {
		Dictionary<string, string> attrList;
		SVGTransformList transformlist;
		protected string name;
		protected int element_number;
		protected SVGColor fillColor;

		protected float fillOpacity;

		protected bool hasFill;
		protected bool hasStroke;
		protected bool visible;

		protected SVGColor strokeColor;
		protected float strokeOpacity;
		protected float strokeWidth;
		protected SVGStroke stroke;

		protected GameObject _gameobject;

		public GameObject gameObject{
			get { return _gameobject; }
		}

		protected SVGElement(Dictionary<string, string> _attrList) {
			attrList = _attrList;

			stroke = null;

			fillColor = null;
			strokeColor = null;
			fillOpacity = 1.0f;
			strokeOpacity = 1.0f;
			strokeWidth = 0;
			hasFill = true;
			hasStroke = false;
			visible = true;
			element_number = SVGGenerals.getElementId();
			if (attrList.ContainsKey("id")){
				name = attrList["id"];
			}else{
				name = this.ToString() + "-" + element_number;
			}


			foreach (KeyValuePair<string,string> attr in attrList){
				switch(attr.Key){
					case "transform":
						transformlist = new SVGTransformList();
						transformlist.ParseAttributes(attr.Value);
						break;
					case "fill":
						if (String.Compare(attr.Value, "none") == 0) {
							hasFill = false;
						} else if (String.Compare(attr.Value, "transparent") == 0) {
							hasFill = false;
						} else {
							hasFill = true;
							fillColor = new SVGColor(attr.Value);
						}
						break;
					case "opacity":
						fillOpacity = StringParser.StringAttrFloat(attr.Value);
						strokeOpacity = fillOpacity;
						break;
					case "fill-opacity":
						fillOpacity = StringParser.StringAttrFloat(attr.Value);
						break;
					case "stroke":
						if (String.Compare(attr.Value, "none") == 0) {
							hasStroke = false;
						} else {
							hasStroke = true;
							strokeColor = new SVGColor(attr.Value);
						}
						break;
					case "stroke-width":
						strokeWidth = StringParser.StringAttrFloat(attr.Value);
						break;
					case "stroke-opacity":
						strokeOpacity = StringParser.StringAttrFloat(attr.Value);
						break;
					case "display":
						if (String.Compare(attr.Value, "none") == 0) {
							visible = false;
						} else
							visible = true;
						break;
					case "style":
						//ParseStyle(value);
						Debug.Log("Attributo style no implementado aun:" + attr.Value);
						break;
					default:
						//Debug.Log("Attributo no implementado: " + attr.Key);
						break;
				}
			}

			if(hasStroke){
				stroke = new SVGStroke(strokeColor, strokeWidth, strokeOpacity);
			}

		}


		public abstract void Render(SVGElement parent, Material baseMaterial, onRenderCallback cb = null);
	}

}