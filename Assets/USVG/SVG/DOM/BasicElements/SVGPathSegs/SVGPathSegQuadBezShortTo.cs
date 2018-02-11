using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegQuadBezShortTo : SVGPathSeg {
	float _x, _y;
	
	public SVGPathSegQuadBezShortTo(float x, float y, bool isRel) : base(isRel){
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

