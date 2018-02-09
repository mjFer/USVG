using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using USVG;



public class Node {
	public SVGNodeType Name;
	public Dictionary<string, string> Attributes;
	public NodeContainer parent;

	public Node(SVGNodeType n, Dictionary<string, string> a, NodeContainer p)
	{
		Name = n;
		Attributes = a;
		parent = p;
	}

	public enum SVGNodeType {
		Rect,
		Line,
		Circle,
		Ellipse,
		PolyLine,
		Polygon,
		Path,
		SVG,
		G,
		LinearGradient,
		RadialGradient,
		Defs,
		Title,
		Desc,
		Stop
	}

	public static Node.SVGNodeType NodeNameToType(string name)
	{
		Node.SVGNodeType retVal;
		switch (name) {
			case "rect": retVal = SVGNodeType.Rect; break;
			case "line": retVal = SVGNodeType.Line; break;
			case "circle": retVal = SVGNodeType.Circle; break;
			case "ellipse": retVal = SVGNodeType.Ellipse; break;
			case "polyline": retVal = SVGNodeType.PolyLine; break;
			case "polygon": retVal = SVGNodeType.Polygon; break;
			case "path": retVal = SVGNodeType.Path; break;
			case "svg": retVal = SVGNodeType.SVG; break;
			case "g": retVal = SVGNodeType.G; break;
			case "linearGradient": retVal = SVGNodeType.LinearGradient; break;
			case "radialGradient": retVal = SVGNodeType.RadialGradient; break;
			case "defs": retVal = SVGNodeType.Defs; break;
			case "title": retVal = SVGNodeType.Title; break;
			case "desc": retVal = SVGNodeType.Desc; break;
			case "stop": retVal = SVGNodeType.Stop; break;
			default: throw new System.Exception("Unknown element type '" + name + "'!");
		}
		return retVal;
	}
}

public class NodeContainer : Node {
	public List<Node> childs;

	public NodeContainer(SVGNodeType n, Dictionary<string, string> a, NodeContainer p) : base(n,a, p)
	{
		Name = n;
		Attributes = a;
		childs = new List<Node>();
	}

	public void addChild(Node child){
		childs.Add(child);
	}
}

public class SVGParser : SmallXmlParser.IContentHandler {
	private SmallXmlParser _parser = new SmallXmlParser();
	private NodeContainer currentContainer = null;
	private NodeContainer firstContainer = null;


	public SVGParser(){
	}

	public SVGParser(string text){
		_parser.Parse(new StringReader(text), this);
	}


	public void OnStartParsing(SmallXmlParser parser){
	}
	
	public void OnInlineElement(string name, Dictionary<string, string> attrs){
		currentContainer.addChild(new Node(Node.NodeNameToType(name), new Dictionary<string, string>(attrs), currentContainer));
	}

	//SubElement Begins G o svg element
	public void OnStartElement(string name, Dictionary<string, string> attrs){
		NodeContainer nc = new NodeContainer(Node.NodeNameToType(name), new Dictionary<string, string>(attrs), currentContainer);
		if(currentContainer != null){
			currentContainer.addChild(nc);	
		}
		currentContainer = nc;
		if (firstContainer == null) {
			firstContainer = currentContainer;
		}
	}

	//SubElement Ends G o svg element
	public void OnEndElement(string name){	
		currentContainer = currentContainer.parent;
	}

	public void GetElements(List<SVGElement> elementList, NodeContainer parent){
		foreach(Node node in parent.childs){
			switch (node.Name) {
				case Node.SVGNodeType.Rect:
					elementList.Add(new SVGRect(node.Attributes));
					break;
				case Node.SVGNodeType.Line:
					elementList.Add(new SVGLine(node.Attributes));
					break;
				case Node.SVGNodeType.Circle:
					elementList.Add(new SVGCircle(node.Attributes));
					break;
				case Node.SVGNodeType.Ellipse:
					elementList.Add(new SVGEllipse(node.Attributes));
					break;
				case Node.SVGNodeType.PolyLine:
					elementList.Add(new SVGPolyline(node.Attributes));
					break;
				case Node.SVGNodeType.Polygon:
					elementList.Add(new SVGPolygon(node.Attributes));
					break;
				case Node.SVGNodeType.Path:
					elementList.Add(new SVGPath(node.Attributes));
					break;

				case Node.SVGNodeType.SVG:
					SVGSVG svg = new SVGSVG(node.Attributes);
					if ((node as NodeContainer).childs.Count > 0) {
						List<SVGElement> subElementList = new List<SVGElement>();
						GetElements(subElementList, node as NodeContainer);
						svg.addChildren(subElementList);
					}
					elementList.Add(svg);
					break;
				case Node.SVGNodeType.G:
					SVGG svgg = new SVGG(node.Attributes);
					if ((node as NodeContainer).childs.Count > 0) {
						List<SVGElement> subElementList = new List<SVGElement>();
						GetElements(subElementList, node as NodeContainer);
						svgg.addChildren(subElementList);
					}
					elementList.Add(svgg);
					break;

				//case Node.SVGNodeName.LinearGradient: paintable.AppendLinearGradient(new SVGLinearGradientElement(this, Node.Attributes)); break;
				//case Node.SVGNodeName.RadialGradient: paintable.AppendRadialGradient(new SVGRadialGradientElement(this, Node.Attributes)); break;

				//case Node.SVGNodeName.Defs: GetElementList(elementList, paintable, render, summaryTransformList); break;
				//case Node.SVGNodeName.Title: GetElementList(elementList, paintable, render, summaryTransformList); break;
				//case Node.SVGNodeName.Desc: GetElementList(elementList, paintable, render, summaryTransformList); break;
			}

		}

	}

	public void GetElementList(List<SVGElement> elementList)
	{
		GetElements(elementList, firstContainer);
	}

	
}
