

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace USVG {
	public class SVGGeometry : SVGElement {
		protected Vector2[] vectors_2d;

		protected Mesh msh;
		protected MeshFilter filter;


		protected SVGGeometry(Dictionary<string, string> _attrList) : base(_attrList)
		{

		}


		public override void Render(SVGElement parent)
		{
			if (_gameobject == null) {
				_gameobject = new GameObject();
				_gameobject.AddComponent(typeof(MeshRenderer));
				if (parent != null)
					_gameobject.transform.parent = parent.gameObject.transform;
			}

			if (vectors_2d != null && vectors_2d.Length > 0){
				// Use the triangulator to get indices for creating triangles
				Triangulator tr = new Triangulator(vectors_2d);
				int[] indices = tr.Triangulate();

				// Create the Vector3 vertices
				Vector3[] vertices = new Vector3[vectors_2d.Length];
				for (int i = 0; i < vertices.Length; i++) {
					vertices[i] = new Vector3(vectors_2d[i].x, vectors_2d[i].y, 0);
					Debug.Log("i" + i + " vertex: " + vectors_2d[i]);
				}

				// Create the mesh
				if (msh == null) msh = new Mesh();
				msh.vertices = vertices;
				msh.triangles = indices;

				//Normals
				Vector3[] normals = new Vector3[vectors_2d.Length];
				for (int i = 0; i < normals.Length; i++) {
					normals[i] = new Vector3(0, 0, 1);
				}
				msh.RecalculateNormals();
				msh.RecalculateBounds();


				if (filter == null) filter = gameObject.GetComponent<MeshFilter>();
				if (filter == null)
					filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;

				filter.mesh = msh;
			}
		}
	}

}