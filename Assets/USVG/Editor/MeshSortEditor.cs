using UnityEngine;
using UnityEditor;
using System.Collections;

//[CustomEditor(typeof(MeshRenderer))]
public class MeshSortEditor : EditorWindow {

	[MenuItem("USVG/MeshSortEditor")]
	static void Init()
	{
		MeshSortEditor window = (MeshSortEditor)EditorWindow.GetWindow(typeof(MeshSortEditor));
		window.Show();
	}

	void OnGUI()
	{
		GameObject go = Selection.activeGameObject;

		if (go) {
			MeshRenderer renderer = go.GetComponent<MeshRenderer>();
			if (renderer != null) {

				EditorGUILayout.BeginHorizontal();
				EditorGUI.BeginChangeCheck();
				string name = EditorGUILayout.TextField("Sorting Layer Name", renderer.sortingLayerName);
				if (EditorGUI.EndChangeCheck()) {
					renderer.sortingLayerName = name;
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUI.BeginChangeCheck();
				int order = EditorGUILayout.IntField("Sorting Order", renderer.sortingOrder);
				if (EditorGUI.EndChangeCheck()) {
					renderer.sortingOrder = order;
				}
				EditorGUILayout.EndHorizontal();
			}
		}

	}
}
