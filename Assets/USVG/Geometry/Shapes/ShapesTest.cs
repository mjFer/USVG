using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using USVG;

[ExecuteInEditMode]
public class ShapesTest : MonoBehaviour {

	public enum Tipo{
		RECTANGLE,
		ROUNDED_RECTANGLE,
		CIRCLE,
		ELIPSE,
		CAPSULE,
		GEAR
	};

	public Tipo tipo;

	public float Height;
	public float Width;
	public float Radius;
	public float yRadius;
	public int Edges;

	//Para Gear exclusivamente
	public int NumberOfTheeth;
	public float TipPercentage;
	public float ToothHeight;


	void Start()
	{
		// Create Vector2 vertices
		Vector2[] vertices2D = null;

		switch(tipo){
			case Tipo.CAPSULE:
				vertices2D = USVG.PolygonTools.CreateCapsule(Height, Radius, Edges);
				break;
			case Tipo.CIRCLE:
				vertices2D = PolygonTools.CreateCircle(Radius, Edges);
				break;
			case Tipo.ELIPSE:
				vertices2D = PolygonTools.CreateEllipse(Radius, yRadius, Edges);
				break;
			case Tipo.GEAR:
				vertices2D = PolygonTools.CreateGear(Radius, NumberOfTheeth, TipPercentage, ToothHeight);
				break;
			case Tipo.ROUNDED_RECTANGLE:
				vertices2D = PolygonTools.CreateRoundedRectangle(Height, Width, Radius, yRadius, Edges);
				break;
			default:
			case Tipo.RECTANGLE:
				vertices2D = PolygonTools.CreateRectangle(Height, Width);
				break;
		}


		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i = 0; i < vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		// Create the mesh
		Mesh msh = new Mesh();
		msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals();
		msh.RecalculateBounds();

		// Set up game object with mesh;
		gameObject.AddComponent(typeof(MeshRenderer));
		MeshFilter filter = gameObject.GetComponent<MeshFilter>();
		if(filter == null)
			filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;

		filter.mesh = msh;
	}
}
