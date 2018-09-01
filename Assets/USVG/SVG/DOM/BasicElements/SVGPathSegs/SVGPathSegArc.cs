using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SVGPathSegArc : SVGPathSeg {
	float _rx;
	float _ry;
	float _xAxRot;
	bool _largeArcFlag;
	bool _sweepFlag;
	float _x;
	float _y;

	//converted parameters
	double _cx;
	double _cy;
	//start angle
	double _theta;
	//delta angle
	double _delta;
	//end angle
	double _alpha;
	//clockwise?
	bool cw;

	//A rx ry x-axis-rotation large-arc-flag sweep-flag x y
	//a rx ry x-axis - rotation large - arc - flag sweep - flag dx dy
	public SVGPathSegArc(float rx, float ry, float xAxRot, bool largeArcFlag,bool sweepFlag, float x, float y, bool isRel, SVGPathSeg prevSeg) : base(isRel, prevSeg){
		_rx = rx;
		_ry = ry;
		_xAxRot = xAxRot;
		_largeArcFlag = largeArcFlag;
		_sweepFlag = sweepFlag;
		_x = x;
		_y = y;
		EndpointToCenterArcParams();

		//Debug.Log(" rx:" + _rx + " ry:" + _ry + " xAxisRotation:" + _xAxRot + " largeArcFlag:" + _largeArcFlag + " sweepFlag:" + _sweepFlag + " x:" + _x + " y:" + _y);
		//Debug.Log("Calculated params: cx:" + _cx + " cy:" + _cy + " theta:" + _theta + " delta:" + _delta);

	}

	public override Vector2 getCursor()
	{
		if (!endCursorCalculated) {
			if (_coord_type == PathCoordType.SVG_PATH_ABSOLUTE)
				endCursor = new Vector2(_x, _y);
			else
				endCursor = new Vector2(_x + _prevSeg.getCursor().x, _y + _prevSeg.getCursor().y);
			endCursorCalculated = true;
		}
		return endCursor;
	}

	public override float GetLenght()
	{
		Vector2[] points = GetPoints(4);
		float lenght = 0;
		for (int i = 0; i < points.Length - 2; i++) {
			lenght += Vector2.Distance(points[i], points[i + 1]);
		}
		return lenght;
	}


	public void XXX()
	{
		float s = (float)_theta;
		float e = s + (float)_delta;
		bool neg = e < s;
		float sign = neg ? -1 : 1;

		NormalizeAngle(ref s);
		NormalizeAngle(ref e);

		float remain;
		if(neg){
			remain = Mathf.DeltaAngle(e, s);
		}else{
			remain = Mathf.DeltaAngle(s, e);
		}
		


		Debug.Log("------AAA start" + s + " end" + e + " sign " + sign + " lenght" + remain);

	}

	void NormalizeAngle(ref float angle){
		if(angle>0){
			while(angle > Math.PI * 2){
				angle -= (float)Math.PI * 2;
			}
		}else{
			while (angle < 0) {
				angle += (float)Math.PI * 2;
			}
		}

	}

	public override Vector2[] GetPoints(int nSegments)
	{
		List<Vector2> points = new List<Vector2>();

		int i;
		float dt = 1.0f / (nSegments - 1);

		//float sign = 1.0f;
		//if (_delta < 0) {
		//	sign = -1.0f;
		//}else{
		//}
		
		for (i = 0; i < nSegments; i++)
			points.Add( ArcPoint( i * dt * (float)_delta + (float)_theta));

		return points.ToArray();

	}

	Vector2 ArcPoint( float t)
	{
		return new Vector2(
			(float)(_cx + _rx * Math.Cos(_xAxRot) * Math.Cos(t) - _ry * Math.Sin(_xAxRot) * Math.Sin(t)),
			(float)(_cy + _rx * Math.Sin(_xAxRot) * Math.Cos(t) + _ry * Math.Cos(_xAxRot) * Math.Sin(t)) );
	}

	//Based on code from Edaqua https://mortoray.com/2017/02/16/rendering-an-svg-elliptical-arc-as-bezier-curves/
	void EndpointToCenterArcParams()
	{
		double rX = _rx;
		double rY = _ry;

		Vector2 p1 = _prevSeg.getCursor();
		Vector2 p2 = getCursor();

		//(F.6.5.1)
		double dx2 = (p1.x - p2.x) / 2.0;
		double dy2 = (p1.y - p2.y) / 2.0;
		double x1p = Math.Cos(_xAxRot) * dx2 + Math.Sin(_xAxRot) * dy2;
		double y1p = -Math.Sin(_xAxRot) * dx2 + Math.Cos(_xAxRot) * dy2;

		//(F.6.5.2)
		double rxs = rX * rX;
		double rys = rY * rY;
		double x1ps = x1p * x1p;
		double y1ps = y1p * y1p;
		// check if the radius is too small `pq < 0`, when `dq > rxs * rys` (see below)
		// cr is the ratio (dq : rxs * rys) 
		double cr = x1ps / rxs + y1ps / rys;
		if (cr > 1) {
			//scale up rX,rY equally so cr == 1
			var s = Math.Sqrt(cr);
			rX = s * rX;
			rY = s * rY;
			rxs = rX * rX;
			rys = rY * rY;
		}
		double dq = (rxs * y1ps + rys * x1ps);
		double pq = (rxs * rys - dq) / dq;
		double q = Math.Sqrt(Math.Max(0, pq)); //use Max to account for float precision
		if (_largeArcFlag == _sweepFlag)
			q = -q;
		double cxp = q * rX * y1p / rY;
		double cyp = -q * rY * x1p / rX;

		//Center
		_cx = Math.Cos(_xAxRot) * cxp - Math.Sin(_xAxRot) * cyp + (p1.x + p2.x) / 2;
		_cy = Math.Sin(_xAxRot) * cxp + Math.Cos(_xAxRot) * cyp + (p1.y + p2.y) / 2;

		//Start Angle
		_theta = svgAngle(1, 0, (x1p - cxp) / rX, (y1p - cyp) / rY);
		
		//End Angle
		_alpha = svgAngle(1, 0, (-x1p - cxp) / rX, (-y1p - cyp) / rY);

		//Delta angle
		_delta = svgAngle(
			(x1p - cxp) / rX, (y1p - cyp) / rY,
			(-x1p - cxp) / rX, (-y1p - cyp) / rY);
		if (_delta < 0)
			_delta = -_delta;
		_delta = _delta % (Math.PI * 2);
		if (!_sweepFlag) {
			_delta -= 2 * Math.PI;
			//_delta = -_delta;
		}
		
		if(_delta > 0){
			cw = false;
		}


		Debug.Log("..start:" + _theta + " end: " + _alpha + " delta: " + _delta);


		//if (_delta < 0) {
		//	while (_delta < -2 * Math.PI)
		//		_delta += 2 * Math.PI;
		//} else {
		//	while (_delta > 2 * Math.PI)
		//		_delta -= 2 * Math.PI;
		//}

		//XXX();
		_rx = (float)rX;
		_ry = (float)rY;
		
	}

	//static float svgAngle(double ux, double uy, double vx, double vy)
	//{
	//	Vector2 u = new Vector2((float)ux, (float)uy);
	//	Vector2 v = new Vector2((float)vx, (float)vy);
	//	//(F.6.5.4)
	//	float dot = Vector2.Dot(u, v);
	//	float len = Vector2.Distance(Vector2.zero, u) * Vector2.Distance(Vector2.zero, v);
	//	float ang = Mathf.Acos(Mathf.Clamp(dot / len, -1, 1)); //floating point precision, slightly over values appear
	//	if ((u.x * v.y - u.y * v.x) < 0)
	//		ang = -ang;
	//	return ang;
	//}

	//Angle between vectors
	static double svgAngle(double ux, double uy, double vx, double vy)
	{
		Vector2 u = new Vector2((float)ux, (float)uy);
		Vector2 v = new Vector2((float)vx, (float)vy);

		double dot = u.x * v.x + u.y * v.y;    // dot product between [x1, y1] and [x2, y2]
		double det = u.x * v.y - u.y * v.x;  //determinant
		double angle = Math.Atan2(det, dot);    //atan2(y, x) or atan2(sin, cos)
		return angle;
	}



}
