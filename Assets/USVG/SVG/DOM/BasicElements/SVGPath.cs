using System;
using System.Collections.Generic;
using UnityEngine;

namespace USVG {

	class GenPath{
		public Vector2[] points;
		public bool closed;
	};


	public class SVGPath : SVGGeometry {
		List<SVGPathSeg> segList;
		List<Vector2> generatedPoints;
		List<GenPath> Paths;

		List<Mesh> meshes;

		public SVGPath(Dictionary<string, string> _attrList) : base(_attrList)
		{
			segList = new List<SVGPathSeg>();

			if (_attrList.ContainsKey("d")){
				//Debug.Log(_attrList["d"]);
				List<KeyValuePair<string, string>> dic = StringParser.StringPathSep(_attrList["d"]);
				foreach(KeyValuePair<string, string> keypar in dic){
					ParsePathType(keypar.Key[0], keypar.Value);
				}
			}

		}

		public override void Render(SVGElement parent, Material baseMaterial, onRenderCallback cb)
		{
			int i = 0;
			Paths = new List<GenPath>();
			generatedPoints = new List<Vector2>();
			foreach(SVGPathSeg seg in segList){
				i++;
				Vector2[] newPoints = seg.GetPoints(SVGGenerals.pathNSegments);

				if (generatedPoints.Count > 0 && newPoints != null) {
					if (newPoints[0] == generatedPoints[generatedPoints.Count - 1]) {
						generatedPoints.RemoveAt(generatedPoints.Count - 1);
						Debug.Log("Remuevo duplicado!");
					}
				}

				if(newPoints != null) generatedPoints.AddRange(newPoints);

				if(seg.GetType() == typeof(SVGPathSegClose)){
					GenPath path = new GenPath();
					SVGGenerals.OptimizePoints(ref generatedPoints);
					path.points = generatedPoints.ToArray();
					path.closed = true;
					Paths.Add(path);
					generatedPoints.Clear();
				}
				
			}

			if(generatedPoints.Count>0){
				GenPath path = new GenPath();
				SVGGenerals.OptimizePoints(ref generatedPoints);
				path.points = generatedPoints.ToArray();
				path.closed = false;
				Paths.Add(path);
				generatedPoints.Clear();
			}

			if(Paths.Count == 0){
				Debug.LogWarning("Un path sin segmentos?");
				return;
			}
			vectors_2d = Paths[0].points;
			//foreach (Vector2 vec in vectors_2d) {
			//	Debug.Log(name + "-Vector:" + vec);
			//}

			//base.Render(parent, baseMaterial);

			if (_gameobject == null) {
				_gameobject = new GameObject(name);
				_gameobject.AddComponent(typeof(MeshRenderer));
				if (parent != null)
					_gameobject.transform.parent = parent.gameObject.transform;
			}


			GenerateMeshes();


			if (filter == null) filter = gameObject.GetComponent<MeshFilter>();
			if (filter == null)
				filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;

			filter.mesh = msh;

			if (renderer == null) renderer = gameObject.GetComponent<Renderer>();
			//renderer.sharedMaterials[0] = baseMaterial;
			renderer.sharedMaterial = baseMaterial;

			//renderer. = "USVG";
			renderer.sortingOrder = element_number * 2;

			//renderer.material.name = this.name + "-material";
			//renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			//if (fillColor != null) {
			//	renderer.material.color = new Color(fillColor.R, fillColor.G, fillColor.B, fillOpacity);
			//	renderer.material.renderQueue = 3000 + element_number;
			//}

			if (cb != null) {
				cb.Invoke(this.name, msh);
				//cb.Invoke(this.name + "-material", renderer.material);
			}
		}

		private void GenerateMeshes(){
			if (msh == null) msh = new Mesh();
			CombineInstance[] combine = new CombineInstance[Paths.Count];
			int it = 0;
			foreach (GenPath path in Paths) {
				SVGGenerals.OptimizePoints(ref path.points);
				// Use the triangulator to get indices for creating triangles
				Triangulator tr = new Triangulator(path.points);
				int[] indices = tr.Triangulate();

				// Create the Vector3 vertices
				Vector3[] vertices = new Vector3[path.points.Length];
				for (int i = 0; i < vertices.Length; i++) {
					vertices[i] = new Vector3(path.points[i].x, path.points[i].y, 0);
				}

				// Create the mesh
				Mesh _msh = new Mesh();
				_msh.vertices = vertices;
				_msh.triangles = indices;

				if (fillColor != null) {
					// Use vertex colors
					Color[] colors = new Color[path.points.Length];
					for (int i = 0; i < colors.Length; i++) {
						colors[i] = new Color(fillColor.R, fillColor.G, fillColor.B, fillOpacity);
					}
					_msh.colors = colors;
				}
				

				//Normals
				Vector3[] normals = new Vector3[path.points.Length];
				for (int i = 0; i < normals.Length; i++) {
					normals[i] = new Vector3(0, 0, 1);
				}
				_msh.RecalculateBounds();

				combine[it].mesh = _msh;
				it++;
				
			}
			msh.CombineMeshes(combine, false, false, false);
		}


		private void ParsePathType(char type, string values){
			float[] vals = StringParser.StringPathValues(values);

			SVGPathSeg last = null;
			if (segList.Count > 0)
				last = segList[segList.Count - 1];

			switch (type){
				case 'm':
				case 'M':
					segList.Add( new SVGPathSegMoveTo(vals[0], vals[1], type == 'm' ? true : false, last));
					break;
				case 'l':
				case 'L':
					segList.Add(new SVGPathSegLineTo(vals[0], vals[1], type == 'l' ? true : false, last));
					break;
				case 'H':
				case 'h':
					segList.Add(new SVGPathSegLineTo(vals[0], 0, type == 'h' ? true : false, last, SVGPathSegLineTo.LineToType.HORIZONTAL));
					break;
				case 'V':
				case 'v':
					segList.Add(new SVGPathSegLineTo(0, vals[0], type == 'v' ? true : false, last, SVGPathSegLineTo.LineToType.VERTICAL));
					break;
				case 'C':
				case 'c':
					segList.Add(new SVGPathSegCubicBezTo(vals[0], vals[1], vals[2], vals[3], vals[4], vals[5], type == 'c' ? true : false, last));
					break;
				case 'S':
				case 's':
					segList.Add(new SVGPathSegCubicBezShortTo(vals[0], vals[1], vals[2], vals[3], type == 's' ? true : false, last));
					break;
				case 'Q':
				case 'q':
					segList.Add(new SVGPathSegQuadBezTo(vals[0], vals[1], vals[2], vals[3], type == 'q' ? true : false, last));
					break;
				case 'T':
				case 't':
					segList.Add(new SVGPathSegQuadBezShortTo(vals[0], vals[1], type == 't' ? true : false, last));
					break;
				case 'Z':
				case 'z':
					segList.Add(new SVGPathSegClose( type == 'z' ? true : false, last));
					break;
				case 'A':
				case 'a':
					//A rx ry x-axis-rotation large-arc-flag sweep-flag x y
					//a rx ry x-axis - rotation large - arc - flag sweep - flag dx dy
					segList.Add(new SVGPathSegArc(vals[0], vals[1], vals[2], !(vals[3]==0), !(vals[4]==0), vals[5], vals[6], type == 'a' ? true : false, last));
					break;
				default:
					break;
			}
		}

	}
}