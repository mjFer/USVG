using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegCubicBezShortTo : SVGPathSeg {
	float _x2, _y2;
	float _x, _y;
	
	public SVGPathSegCubicBezShortTo(float x2, float y2, float x, float y, bool isRel) : base(isRel){
		_x2 = x2;
		_y2 = y2;
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

