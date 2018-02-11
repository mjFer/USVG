using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SVGPathSeg{

	public enum PathCoordType{
		SVG_PATH_RELATIVE,
		SVG_PATH_ABSOLUTE
	};
	PathCoordType _coord_type;

	public SVGPathSeg(bool isRel){
		if (isRel) {
			_coord_type = PathCoordType.SVG_PATH_RELATIVE;
		}else{
			_coord_type = PathCoordType.SVG_PATH_ABSOLUTE;
		}
	}

	public abstract Vector2 GetLenght();

	public abstract Vector2 GetPointAtLenght(int nSegments);

}


//SVGPathElement.createSVGPathSegClosePath()

//SVGPathElement.createSVGPathSegMovetoAbs()
//SVGPathElement.createSVGPathSegMovetoRel()

//SVGPathElement.createSVGPathSegLinetoAbs()
//SVGPathElement.createSVGPathSegLinetoRel()

//SVGPathElement.createSVGPathSegCurvetoCubicAbs()
//SVGPathElement.createSVGPathSegCurvetoCubicRel()

//SVGPathElement.createSVGPathSegCurvetoQuadraticAbs()
//SVGPathElement.createSVGPathSegCurvetoQuadraticRel()

//SVGPathElement.createSVGPathSegArcAbs()
//SVGPathElement.createSVGPathSegArcRel()

//SVGPathElement.createSVGPathSegLinetoHorizontalAbs()
//SVGPathElement.createSVGPathSegLinetoHorizontalRel()

//SVGPathElement.createSVGPathSegLinetoVerticalAbs()
//SVGPathElement.createSVGPathSegLinetoVerticalRel()

//SVGPathElement.createSVGPathSegCurvetoCubicSmoothAbs()
//SVGPathElement.createSVGPathSegCurvetoCubicSmoothRel()

//SVGPathElement.createSVGPathSegCurvetoQuadraticSmoothAbs()
//SVGPathElement.createSVGPathSegCurvetoQuadraticSmoothRel()