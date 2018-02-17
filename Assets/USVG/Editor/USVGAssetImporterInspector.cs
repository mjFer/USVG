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
		var baseMaterial = serializedObject.FindProperty("baseMaterial");

		var OptimizeVectors = serializedObject.FindProperty("optimizeVectors");
		var OptimizeVectorsThreshold = serializedObject.FindProperty("optimizeVectorsThreshold");


		EditorGUILayout.PropertyField(circleNSegments);
		EditorGUILayout.PropertyField(elipseNSegments);
		EditorGUILayout.PropertyField(roundedRectNSegments);
		EditorGUILayout.PropertyField(pathNSegments);
		EditorGUILayout.PropertyField(baseMaterial);
		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(OptimizeVectors);
		if(OptimizeVectors.boolValue)
			EditorGUILayout.PropertyField(OptimizeVectorsThreshold);



		base.ApplyRevertGUI();
	}
}