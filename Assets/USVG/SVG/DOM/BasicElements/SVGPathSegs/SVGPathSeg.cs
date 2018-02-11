using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SVGPathSeg{

	public enum PathCoordType{
		SVG_PATH_RELATIVE,
		SVG_PATH_ABSOLUTE
	};
	protected PathCoordType _coord_type;
	protected SVGPathSeg _prevSeg;

	public SVGPathSeg(bool isRel, SVGPathSeg prevSeg){
		if (isRel) {
			_coord_type = PathCoordType.SVG_PATH_RELATIVE;
		}else{
			_coord_type = PathCoordType.SVG_PATH_ABSOLUTE;
		}
		_prevSeg = prevSeg;

	}

	public abstract float GetLenght();

	public abstract Vector2[] GetPoints(int nSegments);

	public abstract Vector2 getCursor();


}

