using System;
using System.Collections.Generic;
using Dr=System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Aviary.Hoopoe;
using Rhino.Geometry;
using Aviary.Wind;

namespace Aviary.Hoopoe.GH
{
    public class ComposeDrawing : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ComposeDrawing class.
        /// </summary>
        public ComposeDrawing()
          : base("Compose Drawing", "Drawing", "Compose a Drawing from curves and graphics", "Aviary 1", "Drawing")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Shapes*", "S", "An input for Shapes or Geometry", GH_ParamAccess.list);
            pManager.AddGenericParameter("Boundary", "B", "The rectangular cropping boundary", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Width", "W", "The width of the resulting image", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Height", "H", "The height of the resulting image", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddColourParameter("Backround", "C", "The background color", GH_ParamAccess.item,Dr.Color.Transparent);
            pManager[4].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

            pManager.AddGenericParameter("Drawing", "D", "An Aviary Drawing", GH_ParamAccess.item);
        }
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Drawing drawing = new Drawing();

            List<IGH_Goo> objects = new List<IGH_Goo>();
            if (!DA.GetDataList(0, objects)) return;

            Rectangle3d rect = new Rectangle3d();
            if (DA.GetData(1, ref rect)) drawing.Frame = rect;

            double width = 0;
            if (DA.GetData(2, ref width)) drawing.Width = width;

            double height = 0;
            if (DA.GetData(3, ref height)) drawing.Height = height;

            Dr.Color color = Dr.Color.Transparent;
            if (DA.GetData(4, ref color)) drawing.Background = color.ToWindColor();
            
            foreach (IGH_Goo goo in objects)
            {
                Curve curve = null;
                Arc arc = new Arc();
                Shape shape = new Shape();
                if (goo.CastTo<Shape>(out shape))
                {
                    drawing.Shapes.Add(shape);
                }
                else
                if (goo.CastTo<Curve>(out curve))
                {
                    shape = new Shape(curve);
                    drawing.Shapes.Add(shape);
                }
                else
                if (goo.CastTo<Arc>(out arc))
                {
                    shape = new Shape(arc.ToNurbsCurve());
                    drawing.Shapes.Add(shape);
                }
            }

            DA.SetData(0, drawing);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.Drawings;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7e0d851f-e15e-46f8-b197-be6af372ca8e"); }
        }
    }
}