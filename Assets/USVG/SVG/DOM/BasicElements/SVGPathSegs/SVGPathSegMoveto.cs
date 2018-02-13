using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SVGPathElement.createSVGPathSegMovetoAbs()
//SVGPathElement.createSVGPathSegMovetoRel()
public class SVGPathSegMoveTo : SVGPathSeg {
	float _x, _y;
	
	public SVGPathSegMoveTo(float x, float y, bool isRel, SVGPathSeg prevSeg) : base(isRel, prevSeg){
		_x = x;
		_y = y;
	}

	public override Vector2 getCursor()
	{
		if (!endCursorCalculated) {
			Vector2 inicial = new Vector2();
			if (_coord_type == PathCoordType.SVG_PATH_ABSOLUTE) {
				endCursor = new Vector2(_x, _y);
			} else {
				if (_prevSeg != null)
					inicial += _prevSeg.getCursor();
				endCursor = new Vector2(_x + inicial.x, _y + inicial.y);
			}
			endCursorCalculated = true;
		}
		return endCursor;
	}

	public override float GetLenght()
	{
		return 0;
	}

	public override Vector2[] GetPoints(int nSegments)
	{
		return null;
	}
}

