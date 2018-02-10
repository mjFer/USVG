using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using USVG;


public static class SVGGenerals {
	public static int circleNSegments = 20;
	public static int elipseNSegments = 20;
	public static int roundedRectNSegments = 20;

	private static int lastNodeId = -1;

	public static int getElementId(){
		return lastNodeId++;
	}

	
}
