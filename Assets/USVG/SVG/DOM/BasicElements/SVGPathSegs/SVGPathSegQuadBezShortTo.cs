using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegQuadBezShortTo : SVGPathSegQuadBezTo {
	
	public SVGPathSegQuadBezShortTo(float x, float y, bool isRel, SVGPathSeg prevSeg) 
		: base(0, 0, x, y, isRel, prevSeg){

		if (prevSeg.GetType() == typeof(SVGPathSegQuadBezShortTo)) {
			SVGPathSegQuadBezShortTo prev = _prevSeg as SVGPathSegQuadBezShortTo;
			//_x1 = -prev.X1;
			//_y1 = -prev.Y1;
			_x1 = prev.getCursor().x - prev.dX1;
			_y1 = prev.getCursor().y - prev.dY1;
			if (_coord_type == PathCoordType.SVG_PATH_RELATIVE) {
				_x1 -= prev.getCursor().x;
				_y1 -= prev.getCursor().y;
			}
		} else if (prevSeg.GetType() == typeof(SVGPathSegQuadBezTo)) {
			SVGPathSegQuadBezTo prev = _prevSeg as SVGPathSegQuadBezTo;
			//_x1 = -prev.X1;
			//_y1 = -prev.Y1;
			_x1 = prev.getCursor().x - prev.dX1;
			_y1 = prev.getCursor().y - prev.dY1;
			if (_coord_type == PathCoordType.SVG_PATH_RELATIVE) {
				_x1 -= prev.getCursor().x;
				_y1 -= prev.getCursor().y;
			}
		}
		// else {
		//	_x1 = 2 * prevSeg.getCursor().x;
		//	_y1 = 2 * prevSeg.getCursor().y;
		//}
		

	}

}

