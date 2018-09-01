

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
		protected Renderer renderer;


		protected SVGGeometry(Dictionary<string, string> _attrList) : base(_attrList)
		{

		}


		public override void Render(SVGElement parent, Material baseMaterial, onRenderCallback cb)
		{
			if (_gameobject == null) {
				_gameobject = new GameObject(name);
				_gameobject.AddComponent(typeof(MeshRenderer));
				if (parent != null)
					_gameobject.transform.parent = parent.gameObject.transform;
			}

			if (vectors_2d != null && vectors_2d.Length > 0){
				SVGGenerals.OptimizePoints(ref vectors_2d);
				// Use the triangulator to get indices for creating triangles
				Triangulator tr = new Triangulator(vectors_2d);
				int[] indices = tr.Triangulate();

				// Create the Vector3 vertices
				Vector3[] vertices = new Vector3[vectors_2d.Length];
				for (int i = 0; i < vertices.Length; i++) {
					vertices[i] = new Vector3(vectors_2d[i].x, vectors_2d[i].y, 0);
				}

				// Create the mesh
				if (msh == null) msh = new Mesh();
				msh.vertices = vertices;
				msh.triangles = indices;

				if (fillColor != null) {
					// Use vertex colors
					Color[] colors = new Color[vertices.Length];
					for (int i = 0; i < colors.Length; i++) {
						colors[i] = new Color(fillColor.R, fillColor.G, fillColor.B, fillOpacity);
					}
					msh.colors = colors;
				}

				//Normals
				//Vector3[] normals = new Vector3[vectors_2d.Length];
				//for (int i = 0; i < normals.Length; i++) {
				//	normals[i] = new Vector3(0, 0, 1);
				//}
				msh.RecalculateNormals();
				msh.RecalculateBounds();
				msh.RecalculateTangents();

				//Generate Path mesh
				if (stroke != null) {
					stroke.GenerateMesh(vertices);
					Mesh strokeMesh = stroke.GetMesh();
					CombineInstance combine = new CombineInstance();
					combine.mesh = strokeMesh;
					combine.transform = _gameobject.transform.localToWorldMatrix;
					msh.CombineMeshes(new CombineInstance[] { combine });
				}

				if (filter == null) filter = gameObject.GetComponent<MeshFilter>();
				if (filter == null)
					filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;

				filter.sharedMesh = msh;

				if (renderer == null) renderer = gameObject.GetComponent<Renderer>();
				renderer.sharedMaterial = baseMaterial;
				//renderer.material = baseMaterial;
				//renderer.material.name = name + "-material";
				//renderer.sortingLayerName = "USVG";
				renderer.sortingOrder = element_number;

				//renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
				//if (fillColor != null){
				//	renderer.material.color = new Color(fillColor.R, fillColor.G, fillColor.B, fillOpacity);
				//	renderer.material.renderQueue = 3000 + element_number;
				//}

				if (cb != null) {
					cb.Invoke(this.name + "_msh", msh);
					//cb.Invoke(renderer.material.name, renderer.material);
				}
			}
		}
	}

}