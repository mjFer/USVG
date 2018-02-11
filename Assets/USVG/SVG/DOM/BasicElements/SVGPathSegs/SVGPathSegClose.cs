using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SVGPathSegClose : SVGPathSeg {
	
	public SVGPathSegClose( bool isRel, SVGPathSeg prevSeg) : base(isRel, prevSeg){
	}

	public override Vector2 getCursor()
	{	
		if (_prevSeg != null)
			return _prevSeg.getCursor();
		return Vector2.zero;	
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

