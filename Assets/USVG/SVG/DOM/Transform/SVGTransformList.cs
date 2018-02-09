using UnityEngine;
using System.Collections.Generic;

namespace USVG {
	class SVGTransformList {
		List<SVGTransform> _transformList;

		public SVGTransformList(){
			_transformList = new List<SVGTransform>();
		}

		public void clear(){
			_transformList.Clear();
		}

		public void Initialize( SVGTransform newItem){
			clear();
			_transformList.Add(newItem);
		}

		public SVGTransform this[int index]{
			get {
				if (index < _transformList.Count) {
					return _transformList[index];
				}
				return null;
			}
		}

		public void InsertItemBefore( SVGTransform newItem, int index){
			_transformList.Insert(index, newItem);
		}

		public void ReplaceItem( SVGTransform newItem, int index){
			_transformList.RemoveAt(index);
			_transformList.Insert(index, newItem);
		}

		public void RemoveItem( int index){
			_transformList.RemoveAt(index);
		}

		public void AppendItem( SVGTransform newItem){
			_transformList.Add(newItem);
		}

		public void CreateSVGTransformFromMatrix(SVGMatrix matrix){
			SVGTransform tr = new SVGTransform();
			tr.setMatrix(matrix);
			_transformList.Add(tr);
		}

		public SVGTransform Consolidate(){
			if (_transformList.Count == 0)
				return null;
			SVGMatrix new_matrix = new SVGMatrix();
			for(int index=0; index < _transformList.Count; index++){
				new_matrix.Multiply(_transformList[index].Matrix);
			}
			_transformList.Clear();
			SVGTransform newTr = new SVGTransform();
			newTr.setMatrix(new_matrix);
			_transformList.Add(new SVGTransform());

			return newTr;
		}
	};
}