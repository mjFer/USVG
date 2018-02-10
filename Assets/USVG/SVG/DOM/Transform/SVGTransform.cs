using UnityEngine;

using System;


namespace USVG {
	public class SVGTransform {
		SVGMatrix _matrix;
		SVGTransformType _type;
		float _angle;

		public enum SVGTransformType {
			SVG_TRANSFORM_UNKNOWN = 0,
			SVG_TRANSFORM_MATRIX = 1,
			SVG_TRANSFORM_TRANSLATE = 2,
			SVG_TRANSFORM_SCALE = 3,
			SVG_TRANSFORM_ROTATE = 4,
			SVG_TRANSFORM_SKEWX = 5,
			SVG_TRANSFORM_SKEWY = 6,
		};

		public SVGMatrix Matrix{ get{ return _matrix; } }
		public float Angle { get { return _angle; } }

		public SVGTransform()
		{
			_matrix = new SVGMatrix();
			_type = SVGTransformType.SVG_TRANSFORM_MATRIX;
			_angle = 0;
		}


		public void setMatrix(SVGMatrix matrix)
		{
			_matrix = matrix;
		}

		public void setTranslate(float tx, float ty)
		{
			_type = SVGTransformType.SVG_TRANSFORM_TRANSLATE;
			_matrix = new SVGMatrix().Translate(tx, ty);
		}

		public void setScale(float sx, float sy)
		{
			_type = SVGTransformType.SVG_TRANSFORM_SCALE;
			_matrix = new SVGMatrix().ScaleNonUniform(sx, sy);
		}

		public void setRotate(float angle, float cx, float cy)
		{
			_type = SVGTransformType.SVG_TRANSFORM_ROTATE;
			_matrix = new SVGMatrix().Translate(cx, cy).Rotate(angle).Translate(-cx, -cy);
		}

		public void setSkewX(float angle)
		{
			_type = SVGTransformType.SVG_TRANSFORM_SKEWX;
			_matrix = new SVGMatrix().SkewX(angle);
		}

		public void setSkewY(float angle)
		{
			_type = SVGTransformType.SVG_TRANSFORM_SKEWY;
			_matrix = new SVGMatrix().SkewY(angle);
		}

		public void TransformParseAttr(string type, string parms){
			float[] values = StringParser.ConvertAttrNumber(parms);
			float y;
			float x;

			switch(type){
				case "matrix":
					_type = SVGTransformType.SVG_TRANSFORM_MATRIX;
					setMatrix(new SVGMatrix(values[0], values[1], values[2], values[3], values[4], values[5]));
					break;
				case "translate":
					_type = SVGTransformType.SVG_TRANSFORM_TRANSLATE;
					y = values.Length == 2 ? y = values[1] : 0;
					setTranslate(values[0], y);
					break;
				case "scale":
					_type = SVGTransformType.SVG_TRANSFORM_SCALE;
					y = values.Length == 2 ? y = values[1] : 0;
					setScale(values[0], y);
					break;
				case "rotate":
					_type = SVGTransformType.SVG_TRANSFORM_ROTATE;
					x = values.Length >= 2 ? x = values[1] : 0;
					y = values.Length >= 3 ? y = values[0] : 0;
					setRotate(values[0], x, y);
					break;
				case "skewX":
					_type = SVGTransformType.SVG_TRANSFORM_SKEWX;
					setSkewX(values[0]);
					break;
				case "skewY":
					_type = SVGTransformType.SVG_TRANSFORM_SKEWY;
					setSkewY(values[0]);
					break;
				default:
					Debug.Log("Transformacion Desconocida!");
					break;
			}

		}

	}
}
