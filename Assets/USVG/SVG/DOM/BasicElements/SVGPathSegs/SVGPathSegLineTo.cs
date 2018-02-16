using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SVGPathElement.createSVGPathSegMovetoAbs()
//SVGPathElement.createSVGPathSegMovetoRel()
public class SVGPathSegLineTo : SVGPathSeg {
	float _x, _y;

	Vector2[] _points = null;
	
	public enum LineToType{
		NORMAL,
		VERTICAL,
		HORIZONTAL
	};
	LineToType lineType;

	public SVGPathSegLineTo(float x, float y,  bool isRel, SVGPathSeg prevPath, LineToType type= LineToType.NORMAL) : base(isRel, prevPath){
		_x = x;
		_y = y;
		lineType = type;
		switch (type){
			case LineToType.NORMAL:
				break;
			case LineToType.HORIZONTAL:
				if(_coord_type == PathCoordType.SVG_PATH_ABSOLUTE)
					_y = prevPath.getCursor().y;
				break;
			case LineToType.VERTICAL:
				if (_coord_type == PathCoordType.SVG_PATH_ABSOLUTE)
					_x = prevPath.getCursor().x;
				break;
		}

	}

	public override float GetLenght()
	{
		if (_points == null) GetPoints(0);
		return Vector2.Distance(_prevSeg.getCursor(), _points[_points.Length - 1]);	
	}

	public override Vector2 getCursor()
	{
		if (_points == null) GetPoints(0);
		return _points[_points.Length - 1];
	}

	public override Vector2[] GetPoints(int nSegments)
	{
		if (_points == null) {
			Vector2 cursor = _prevSeg.getCursor();
			List<Vector2> tmp = new List<Vector2>(); ;
			if (_prevSeg.GetType() == typeof(SVGPathSegMoveTo))
				tmp.Add(cursor);
			if (_coord_type == PathCoordType.SVG_PATH_RELATIVE) {
				tmp.Add(new Vector2(_x + cursor.x, _y + cursor.y));
			} else {
				tmp.Add(new Vector2(_x, _y));
			}
			_points = tmp.ToArray();
		}

		return _points;
	}
}

