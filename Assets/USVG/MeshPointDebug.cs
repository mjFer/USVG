using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPointDebug : MonoBehaviour {
	public float pointSize = 10f;
	public int remarkPointIndex = 0;

	private MeshFilter filter = null;
	private Mesh mesh = null;

	private void OnDrawGizmos()
	{
		int number = 0;
		if(mesh ==null){
			filter = GetComponent<MeshFilter>();
			mesh = filter.mesh;
		}

		if (remarkPointIndex < 0)
			remarkPointIndex = 0;
		if (remarkPointIndex > mesh.vertices.Length - 1)
			remarkPointIndex = mesh.vertices.Length - 1;

		foreach (Vector3 vertex in mesh.vertices) {
			if (remarkPointIndex == number) {
				Debug.DrawLine(vertex + Vector3.left * pointSize, vertex + Vector3.right * pointSize * 2, Color.red);
				Debug.DrawLine(vertex + Vector3.up * pointSize, vertex + Vector3.down * pointSize * 2, Color.red);
			}else{
				Debug.DrawLine(vertex + Vector3.left * pointSize, vertex + Vector3.right * pointSize);
				Debug.DrawLine(vertex + Vector3.up * pointSize, vertex + Vector3.down * pointSize);
			}
			number++;
		}
	}


}
