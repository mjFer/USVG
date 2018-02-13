using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPointDebug : MonoBehaviour {
	public float pointSize = 10f;

	private MeshFilter filter = null;
	private Mesh mesh = null;

	private void OnDrawGizmos()
	{
		bool first = true;
		if(mesh ==null){
			filter = GetComponent<MeshFilter>();
			mesh = filter.mesh;
		}

		foreach (Vector3 vertex in mesh.vertices) {
			if (first) {
				Debug.DrawLine(vertex + Vector3.left * pointSize, vertex + Vector3.right * pointSize * 2, Color.red);
				Debug.DrawLine(vertex + Vector3.up * pointSize, vertex + Vector3.down * pointSize * 2, Color.red);
			}else{
				Debug.DrawLine(vertex + Vector3.left * pointSize, vertex + Vector3.right * pointSize);
				Debug.DrawLine(vertex + Vector3.up * pointSize, vertex + Vector3.down * pointSize);
			}
			first = false;
		}
	}


}
