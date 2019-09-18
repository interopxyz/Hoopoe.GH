# Hoopoe.GH
The Grasshopper 3d implementation of Hoopoe, the vector graphics library for Aviary

Download the latest WIP at: [Aviary 1.01.0001 Download](https://www.food4rhino.com/app/aviary)

![definition](https://github.com/interopxyz/Hoopoe.GH/blob/Dev_PreCommit/Assets/Hoopoe_DrawingViewer.PNG?raw=true)

## README

The Hoopoe Grasshopper plugin converts Grasshopper geometry into illustrative drawings letting the user easily control the stroke and fill appeance of each shape in the drawing. The drawings can then be visualized in the Grasshopper canvas with the Drawing Viewer and exported to high resolution images including pngs, tiffs, and jpegs or SVG files that retain the editable graphics and geometry. Additionally basic effects of blurring and drop shadow can be added. Drawings are automatically scaled and centered for ease of visualization and output, but can also be cropped and framed with a rectangle for more control over position and size. 
Each shape's stroke has a variety of properties that can be set including width, color, and dash pattern. While the shape's fill can currently be switched between a solid color and either a linear or radial gradient. These graphic settings are shared across the entire Aviary ecosystem.

![ribbon](https://github.com/interopxyz/Hoopoe.GH/blob/Dev_PreCommit/Assets/Hoopoe_Ribbon.png?raw=true)

Though still a work in progress, the goal of Hoopoe GH is to provide visual parity across the viewer, raster, and vector outputs. 

### Dependencies

 - [Rhinoceros 3d](https://www.rhino3d.com/)
 - [Rhinocommon](https://www.nuget.org/packages/RhinoCommon/5.12.50810.13095)
 - [Aviary.Wind](https://github.com/interopxyz/Wind)
 - [Aviary.Wind.GH](https://github.com/interopxyz/Wind.GH)
