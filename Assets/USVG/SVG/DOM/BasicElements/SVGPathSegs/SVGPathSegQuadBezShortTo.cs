using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegQuadBezShortTo : SVGPathSegQuadBezTo {
	
	public SVGPathSegQuadBezShortTo(float x, float y, bool isRel, SVGPathSeg prevSeg) 
		: base(0, 0, x, y, isRel, prevSeg){

		if (prevSeg.GetType() == typeof(SVGPathSegQuadBezShortTo)) {
			SVGPathSegQuadBezShortTo prev = _prevSeg as SVGPathSegQuadBezShortTo;
			_x1 = prev.X1;
			_y1 = prev.Y1;
		}else if (prevSeg.GetType() == typeof(SVGPathSegQuadBezTo)){
			SVGPathSegQuadBezTo prev = _prevSeg as SVGPathSegQuadBezTo;
			_x1 = prev.X1;
			_y1 = prev.Y1;
		}
		

	}

}

