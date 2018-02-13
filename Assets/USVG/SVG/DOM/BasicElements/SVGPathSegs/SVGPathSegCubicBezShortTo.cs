using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegCubicBezShortTo : SVGPathSegCubicBezTo {

	
	public SVGPathSegCubicBezShortTo(float x2, float y2, float x, float y, bool isRel, SVGPathSeg prevSeg) 
		: base( 0, 0, x2, y2, x, y, isRel, prevSeg){

		if (prevSeg.GetType() == typeof(SVGPathSegCubicBezShortTo)) {
			SVGPathSegCubicBezShortTo prev = _prevSeg as SVGPathSegCubicBezShortTo;
			_x1 = prev.getCursor().x + prev.dX2;
			_y1 = prev.getCursor().y - prev.dY2;
		} else if (prevSeg.GetType() == typeof(SVGPathSegCubicBezTo)) {
			SVGPathSegCubicBezTo prev = _prevSeg as SVGPathSegCubicBezTo;
			_x1 = prev.getCursor().x + prev.dX2;
			_y1 = prev.getCursor().y - prev.dY2;
		}
		//else{
		//	_x1 = 2 * prevSeg.getCursor().x;
		//	_y1 = 2 * prevSeg.getCursor().y;
		//}

	}

	
}

