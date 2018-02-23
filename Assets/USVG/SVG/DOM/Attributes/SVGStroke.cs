using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

public class SVGStroke {
	Mesh _mesh;
	SVGColor _color;
	Vector3[] _points;
	float _width;
	float _opacity;

	//stroke-linecap
	//stroke-linejoin
	//stroke-dasharray

	public SVGStroke(Dictionary<string, string> _attrList)
	{
		_color = new SVGColor(_attrList["stroke"]);  
		
		if(_attrList.ContainsKey("stroke-width")){
			float.TryParse(_attrList["stroke-width"], out _width);
		}
		if (_attrList.ContainsKey("stroke-opacity")) {
			float.TryParse(_attrList["stroke-opacity"], out _opacity);
		}
		if (_attrList.ContainsKey("stroke-linecap")) {
			Debug.LogWarning("stroke-linecap not implemented");
		}
		if (_attrList.ContainsKey("stroke-linejoin")) {
			Debug.LogWarning("stroke-linejoin not implemented");
		}
		if (_attrList.ContainsKey("stroke-dasharray")) {
			Debug.LogWarning("stroke-dasharray not implemented");
		}

	}

	public SVGStroke( SVGColor color, float width, float opacity)
	{
		_color = color;
		_width = width;
		_opacity = opacity;
	}


	public void GenerateMesh( Vector3[] points){
		_points = points;

		List<Vector3> mesh_points = new List<Vector3>();
		List<int> triangle_list = new List<int>();

		for(int i=0; i<_points.Length-1; i++){
			Vector2 p1 = _points[i];
			Vector2 p2 = _points[i+1];

			float dx = p2.x - p1.x;
			float dy = p2.y - p1.y;

			//Normales
			Vector2 norm1 = new Vector2(-dx, dy);
			Vector2 norm2 = new Vector2( dx,-dy);
			norm1.Normalize();
			norm2.Normalize();

			//Puntos nuevos
			Vector2 np1 = p1 + norm1;
			Vector2 np2 = p1 + norm2;

			mesh_points.Add(np1);
			mesh_points.Add(np2);

			if(i>0){
				//Primer Triangulo
				triangle_list.Add((i - 1) * 2);
				triangle_list.Add((i - 0) * 2);
				triangle_list.Add((i ) * 2 - 1);
				//Segundo Triangulo
				triangle_list.Add( i * 2 - 1);
				triangle_list.Add( i  * 2);
				triangle_list.Add( i * 2 + 1);
			}


		}

		_mesh = new Mesh();
		_mesh.vertices = mesh_points.ToArray();
		_mesh.triangles = triangle_list.ToArray();
		Color[] colors = new Color[mesh_points.Count];
		for (int i = 0; i < colors.Length; i++) {
			colors[i] = new Color(_color.R, _color.G, _color.B, _opacity);
		}
		_mesh.colors = colors;
	}

	public Mesh GetMesh(){
		return _mesh;
	}

}
