using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(USVGAssetImporter))]
public class USVGImporterEditor : ScriptedImporterEditor {


	public override void OnInspectorGUI()
	{

		
		var circleNSegments = serializedObject.FindProperty("circleNSegments");
		var elipseNSegments = serializedObject.FindProperty("elipseNSegments");
		var roundedRectNSegments = serializedObject.FindProperty("roundedRectNSegments");
		var pathNSegments = serializedObject.FindProperty("pathNSegments");
		//var baseMaterial = serializedObject.FindProperty("baseMaterial");

		var OptimizeVectors = serializedObject.FindProperty("optimizeVectors");
		var OptimizeVectorsThreshold = serializedObject.FindProperty("optimizeVectorsThreshold");
		var optimizationSteps = serializedObject.FindProperty("optimizationSteps");

		var mergeMeshes = serializedObject.FindProperty("mergeMeshes");

		EditorGUILayout.LabelField("Reolution Params");
		EditorGUILayout.PropertyField(circleNSegments);
		EditorGUILayout.PropertyField(elipseNSegments);
		EditorGUILayout.PropertyField(roundedRectNSegments);
		EditorGUILayout.PropertyField(pathNSegments);
		//EditorGUILayout.PropertyField(baseMaterial);
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Optimization Params");
		EditorGUILayout.PropertyField(OptimizeVectors);
		if (OptimizeVectors.boolValue) {
			EditorGUILayout.PropertyField(OptimizeVectorsThreshold);
			EditorGUILayout.IntSlider(optimizationSteps, 1, 5);
		}
		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(mergeMeshes);

		base.ApplyRevertGUI();
	}
}