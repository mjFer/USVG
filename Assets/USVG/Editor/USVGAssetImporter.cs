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

	//[SerializeField]
	Material baseMaterial= null;

	[SerializeField]
	bool optimizeVectors = true;
	[SerializeField]
	float optimizeVectorsThreshold = 5.0f;
	[SerializeField]
	int optimizationSteps = 5;

	[SerializeField]
	bool mergeMeshes = true;

	public override void OnImportAsset(AssetImportContext ctx)
	{
		SVGGenerals.circleNSegments = circleNSegments;
		SVGGenerals.elipseNSegments = elipseNSegments;
		SVGGenerals.roundedRectNSegments = roundedRectNSegments;
		SVGGenerals.pathNSegments = pathNSegments;

		SVGGenerals.OptimizeMergePoints = optimizeVectors;
		SVGGenerals.OptimizeThreshold = optimizeVectorsThreshold;
		SVGGenerals.OptimizationSteps = optimizationSteps;

		if (!baseMaterial) {
			baseMaterial = new Material(Shader.Find("Sprites/USVGSprite")); //Sprites/USVGSprite
			//baseMaterial = new Material(Shader.Find("Sprites/Default")); //Sprites/USVGSprite
		}
		ctx.AddObjectToAsset("baseMaterial",baseMaterial);

		string text = File.ReadAllText(ctx.assetPath, Encoding.UTF8);
		SVGParser parser = new SVGParser(text);
		List<SVGElement> elms = new List<SVGElement>();
		parser.GetElementList(elms);
		foreach (SVGElement ele in elms) {
			Debug.Log(ele.ToString());
			
			if (mergeMeshes) {
				ele.Render(null, baseMaterial);
				MergeAll(ctx, ele.gameObject);
			} else{
				ele.Render(null, baseMaterial, ctx.AddObjectToAsset);
				ctx.AddObjectToAsset("MainAsset", ele.gameObject);
				ctx.SetMainObject(ele.gameObject);
			}
			

		}


	}

	private void MergeAll(AssetImportContext ctx, GameObject parent)
	{
		MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		for (int i = 0; i < meshFilters.Length; i++) {
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
		}

		Mesh combinedMesh = new Mesh();
		combinedMesh.CombineMeshes(combine);


		string goName = "MergedGO";
		GameObject combinedObject = new GameObject(goName);
		var filter = combinedObject.AddComponent<MeshFilter>();
		filter.sharedMesh = combinedMesh;
		var renderer = combinedObject.AddComponent<MeshRenderer>();
		renderer.sharedMaterial = baseMaterial;

		ctx.AddObjectToAsset("MainMesh", combinedMesh);
		ctx.AddObjectToAsset("MainAsset", combinedObject);
		ctx.SetMainObject(combinedObject);

	}


}