# geometry_s
 Geometrical compution
 
 Since year 2014 I was interesting to automate my design engeneer work. I have started to learn C# language for this purpose. Time to time I was solving some geometrical tasks. It was very interesting for me to solve this tasks by myself. After tens ready functions I deside to gather everything together to a one library geometry_s.dll. During next years I was using the library for pet projects and buisenes projects. 
 
Main classes:
 GAPoint (2d point)
 GALine (3d line)
 GAPlane (3d plane)
 GAWievPoint (2d point)
 GAWievLine (2d line)
 
 GAGeometry (computational functions) - GAGeometry class allows to calculate:
	Lines intersection - 2d, 3d
	Angle between lines - 2d, 3d
	Angle between planes - 3d
	Angle between vectors - 3d
	Circle center by 3 points - 2d
	Trigonometrical calculation
		Cos(DEG)
		Sin(DEG)
		Tan(DEG)
		ASin(DEG)
		ACos(DEG)
		DEG->RAD
		RAD->DEG
	Distance between
		Points - 2d, 3d
		Lines
		Planes
		Point and Line - 2d, 3d
		Point and Plane - 3d
	Perpendicular
		Point to Line - 2d, 3d
		Point to Plane - 3d
		Line to Line - 3d
	Check state
		Is point on plane - 3d
		Is the same coordinates - 2d, 3d
		Is point on line - 2d, 3d
		Is point inside line - 2d, 3d
		Is point inside triangle - 2d, 3d
		Is point inside tetrahedron - 3d
		Is the same projection of points on plane - 3d
	Percent proportions
	Views positions - for drawing creation
	Normal scale by drawing standarts
	Median filter

 
Classes below need to be additional tested before using:
 GAArc
 GACilinder
 GACone
 GAFaceFlatConvex
 GAFaceFlatSimple
 GAHexagonCorrect
 GAHexagonPrismCorrect
 GALine_Big
 GAPentagonPiramCorrect
 GAPiramida
 GAPoint_Big
 GAPrism
 GASurface
 GATetrahedron
 GATriangle
 GATriangle_Big
 GAVector
 GAView
 GAViewArc
 GAViewLine
 GAViewPoint
 GAViewTriangle
 matrix3x3
 matrixTransform2d
 matrixTransform4x4
 matrixTransform4x4_2