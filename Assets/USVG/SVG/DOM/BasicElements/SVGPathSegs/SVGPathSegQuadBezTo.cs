using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegQuadBezTo : SVGPathSeg {
	float _x1, _y1;
	float _x, _y;
	
	public SVGPathSegQuadBezTo(float x1, float y1, float x, float y, bool isRel) : base(isRel){
		_x1 = x1;
		_y1 = y1;
		_x = x;
		_y = y;
	}

	public override Vector2 GetLenght()
	{
		throw new NotImplementedException();
	}

	public override Vector2 GetPointAtLenght(int nSegments)
	{
		throw new NotImplementedException();
	}
}

