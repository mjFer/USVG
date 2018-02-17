using System.IO;
using System.Text;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using USVG;
using System.Collections.Generic;

[ScriptedImporter(1, "svg")]
public class USVGAssetImporter : ScriptedImporter {
	[SerializeField]
	int circleNSegments = 10;
	[SerializeField]
	int elipseNSegments = 10;
	[SerializeField]
	int roundedRectNSegments = 10;
	[SerializeField]
	int pathNSegments = 10;

	[SerializeField]
	Material baseMaterial;

	[SerializeField]
	bool optimizeVectors = true;
	[SerializeField]
	float optimizeVectorsThreshold = 5;

	public override void OnImportAsset(AssetImportContext ctx)
	{
		SVGGenerals.circleNSegments = circleNSegments;
		SVGGenerals.elipseNSegments = elipseNSegments;
		SVGGenerals.roundedRectNSegments = roundedRectNSegments;
		SVGGenerals.pathNSegments = pathNSegments;

		SVGGenerals.OptimizeMergePoints = optimizeVectors;
		SVGGenerals.OptimizeThreshold = optimizeVectorsThreshold;

		if (!baseMaterial)
			baseMaterial = new Material(Shader.Find("Sprites/Default"));
		ctx.AddSubAsset("baseMaterial",baseMaterial);

		string text = File.ReadAllText(ctx.assetPath, Encoding.UTF8);
		SVGParser parser = new SVGParser(text);
		List<SVGElement> elms = new List<SVGElement>();
		parser.GetElementList(elms);
		foreach (SVGElement ele in elms) {
			Debug.Log(ele.ToString());
			ele.Render(null, baseMaterial, ctx.AddSubAsset);
			ctx.SetMainAsset("MainAsset", ele.gameObject);
		}

		


	}
}