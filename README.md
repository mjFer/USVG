# USVG - Unity SVG to geometry Importer

## Overview
 
This plugin works is a custom asset importer, it allows you to import SVG files directly into the Unity project, converting them into simple geometry **Not texture!**

**Its Drag and drop!**

**IMPORTANT**: After Unity's announcement of the version 2018.2 which incorporates official support for vector graphics, it does not make sense to continue developing this plugin (at least for now).  Feel free to improve this code as you wish.

**IMPORTANT2**: Its Really buggy, requires some work to make it stable.

I will be really pleased if it turns out useful for you. Also if you employ my solution for your work please put me in the credits


## Features
At the moment it supports basic SVG Shapes:
 * Rect (and rounded rect too)
 * Circle
 * Elipse
 * Line
 * Polyline
 * Path
 * Polygon

Supports Transformations, Fill.


## On the Way (TODO List)
 * Arcs (on Paths)
 * clipPath (in order to make it Adobe Illustrator compatible)
 * Strokes (now are parsed but no rendered)
 * Gradiends with shaders (maybe?)
 
 
 
## Example of imported SVG
![Config Options](https://github.com/mjFer/USVG/blob/master/img/panel.png)


## Config Options
![Config Options](https://github.com/mjFer/USVG/blob/master/img/homer.png)

## Others
I used the following documentation as a reference for class diagramming (DOM Models)
 *  https://developer.mozilla.org/es/docs/Web/SVG 
 *  https://www.w3.org/TR/SVG11/
