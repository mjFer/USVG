using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using USVG;


public static class SVGGenerals {
	public static int circleNSegments = 20;
	public static int elipseNSegments = 20;
	public static int roundedRectNSegments = 20;
	public static int pathNSegments = 20;


	public static bool OptimizeMergePoints = false;
	public static float OptimizeThreshold = 0.01f;
	public static int OptimizationSteps = 1;

	private static int lastNodeId = -1;

	public static int getElementId() {
		return lastNodeId++;
	}

	public static void OptimizePoints(ref Vector2[] orig)
	{
		if (OptimizeMergePoints) {
			List<Vector2> vectors_list = new List<Vector2>();
			vectors_list.AddRange(orig);

			for (int j = 0; j < OptimizationSteps; j++) {
				for (int i = 0; i < vectors_list.Count - 1; i++) {
					if (Vector2.Distance(vectors_list[i], vectors_list[i + 1]) < OptimizeThreshold) {
						vectors_list.RemoveAt(i + 1);
					}
				}
			}
			orig = vectors_list.ToArray();
		}
		
	}

	public static void OptimizePoints(ref List<Vector2> vectors_list)
	{
		if (OptimizeMergePoints) {
			for (int j = 0; j < OptimizationSteps; j++) {
				for (int i = 0; i < vectors_list.Count - 1; i++) {
					if (Vector2.Distance(vectors_list[i], vectors_list[i + 1]) < OptimizeThreshold) {
						vectors_list.RemoveAt(i + 1);
					}
				}
			}
		}

	}



}
