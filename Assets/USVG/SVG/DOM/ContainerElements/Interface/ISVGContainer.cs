using System;
using System.Collections.Generic;
using USVG;

public interface ISVGContainer{
	void addChild(SVGElement child);
	void addChildren(List<SVGElement> children);
}