using UnityEngine;

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

	}
}
