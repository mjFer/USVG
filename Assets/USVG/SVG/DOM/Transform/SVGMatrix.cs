
using UnityEngine;

namespace USVG {

	public class SVGMatrix{
		public float a;
		public float b;
		public float c;
		public float d;
		public float e;
		public float f;

		void SetValues(float _a, float _b, float _c,
				float _d, float _e, float _f)
		{
			a = _a;
			b = _b;
			c = _c;
			d = _d;
			e = _e;
			f = _f;
		}

		public SVGMatrix(float _a, float _b, float _c,
						float _d, float _e, float _f){
			SetValues(_a, _b, _c, _d, _e, _f);
		}

		public SVGMatrix() : this(1, 0, 0, 1, 0, 0) { }

		public SVGMatrix Multiply(SVGMatrix sm){
			float a_new = a * sm.a + c * sm.b;
			float b_new = b * sm.a + d * sm.b;
			float c_new = a * sm.c + c * sm.d;
			float d_new = b * sm.c + d * sm.d;
			float e_new = a * sm.e + c * sm.f + e; 
			float f_new = b * sm.e + d * sm.f + f;
			SetValues(a_new, b_new, c_new, d_new, e_new, f_new);
			return this;
		}

		public SVGMatrix Inverse(){
			float det = a * c - c * b;
			if (det == 0)
				return null;
			float a_new = d / det;
			float b_new = -b / det;
			float c_new = -c / det;
			float d_new = a / det;
			float e_new = (c * f - e * d) / det;
			float f_new = (e * b - a * f) / det;
			SetValues(a_new, b_new, c_new, d_new, e_new, f_new);
			return this;
		}
		public SVGMatrix Translate(float x,float y){
			SetValues(a, b, c, d, a * x + c * y + e, b * x + d * y + f);
			return this;
		}
		public SVGMatrix Scale(float scaleFactor){
			SetValues(a * scaleFactor, b * scaleFactor, c * scaleFactor, d * scaleFactor, e, f);
			return this;
		}
		public SVGMatrix ScaleNonUniform(float scaleFactorX,float scaleFactorY){
			SetValues(a * scaleFactorX, b * scaleFactorX,
					  c * scaleFactorY, d * scaleFactorY,
					  e, f);
			return this;
		}
		public SVGMatrix Rotate(float angle){
			float cos_a = Mathf.Cos(angle * Mathf.Deg2Rad);
			float sin_a = Mathf.Sin(angle * Mathf.Deg2Rad);

			SetValues(a * cos_a + c * sin_a, b * cos_a + d * sin_a,
					  c * cos_a - a * sin_a, d * cos_a - b * sin_a,
					  e, f);
			return this;
		}
		//SVGMatrix RotateFromVector(float x,float y);
		public SVGMatrix FlipX(){
			SetValues( - a, -b, c, d, e, f);
			return this;
		}
		public SVGMatrix FlipY(){
			SetValues(a, b, -c, -d, e, f);
			return this;
		}
		public SVGMatrix SkewX(float angle){
			float tan_a = Mathf.Tan(angle * Mathf.Deg2Rad);
			SetValues(a, b,
					  c + a * tan_a, d + b * tan_a,
					  e, f);
			return this;
		}
		public SVGMatrix SkewY(float angle){
			float tan_a = Mathf.Tan(angle * Mathf.Deg2Rad);
			SetValues(a + c * tan_a, b + d * tan_a,
					  c, d,
					  e, f);
			return this;
		}
	}


	
}
