using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Build vertices to represent an axis-aligned box.
/// </summary>
/// <param name="x1">Control Point 1 X</param>
/// <param name="y1">Control Point 1 Y</param>
/// <param name="x2">Control Point 2 X</param>
/// <param name="y2">Control Point 2 Y</param>
/// <param name="x">Final Point 2 X</param>
/// <param name="y">Final Point 2 Y</param>
public class SVGPathSegCubicBezTo : SVGPathSeg {
	protected float _x1, _y1;
	protected float _x2, _y2;
	protected float _x, _y;

	public float dX2 {
		get {
			if (_coord_type == PathCoordType.SVG_PATH_ABSOLUTE)
				return _x2 - _prevSeg.getCursor().x;
			else
				return _x2;
		}
	}
	public float dY2 {
		get {
			if (_coord_type == PathCoordType.SVG_PATH_ABSOLUTE)
				return _y2 - _prevSeg.getCursor().y;
			else
				return _y2;
		}
	}


	public SVGPathSegCubicBezTo(float x1, float y1, float x2, float y2, float x, float y, bool isRel, SVGPathSeg prevSeg) : base(isRel, prevSeg){
		_x1 = x1;
		_y1 = y1;
		_x2 = x2;
		_y2 = y2;
		_x = x;
		_y = y;
	}

	public override float GetLenght()
	{
		Vector2[] points = GetPoints(4);
		float lenght = 0;
		for (int i = 0; i < points.Length - 2; i++) {
			lenght += Vector2.Distance(points[i], points[i + 1]);
		}
		return lenght;
	}

	public Vector2 PointOnCubicBezier( float t)
	{
		float ax, bx, cx;
		float ay, by, cy;
		float tSquared, tCubed;
		Vector2 result;

		Vector2 cursor = _prevSeg.getCursor();
		Vector2 final_point = new Vector2(_x, _y);
		if (_coord_type == PathCoordType.SVG_PATH_RELATIVE)
			final_point += cursor;

		/* cálculo de los coeficientes polinomiales */

		cx = 3.0f * (_x1 - cursor.x);
		bx = 3.0f * (_x2 - _x1) - cx;
		ax = final_point.x - cursor.x - cx - bx;

		cy = 3.0f * (_y1 - cursor.y);
		by = 3.0f * (_y2 - _y1) - cy;
		ay = final_point.y - cursor.y - cy - by;

		/* calculate the curve point at parameter value t */

		tSquared = t * t;
		tCubed = tSquared * t;

		result.x = (ax * tCubed) + (bx * tSquared) + (cx * t) + cursor.x;
		result.y = (ay * tCubed) + (by * tSquared) + (cy * t) + cursor.y;

		return result;
	}


	public override Vector2[] GetPoints( int numberOfPoints)
	{
		float dt;
		int i;
		List<Vector2> points = new List<Vector2>();

		dt = 1.0f / (numberOfPoints - 1);

		for (i = 0; i < numberOfPoints; i++)
			points.Add(PointOnCubicBezier( i * dt));

		return points.ToArray();
	}


	public override Vector2 getCursor()
	{
		if (!endCursorCalculated) {
			if (_coord_type == PathCoordType.SVG_PATH_ABSOLUTE)
				endCursor = new Vector2(_x, _y);
			else
				endCursor = new Vector2(_x + _prevSeg.getCursor().x, _y + _prevSeg.getCursor().y);
			endCursorCalculated = true;
		}
		return endCursor;
	}
}

