using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Graphics;

namespace Hoopoe.GH.Shapes
{
    public class PointToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PointToShape class.
        /// </summary>
        public PointToShape()
          : base("Point To Shape", "PtShp", "Convert a point to a circular Shape", "Display", "Drawing")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "A point to a circular shape", GH_ParamAccess.item);
            pManager.AddNumberParameter("Radius", "R", "The radius of the point representation", GH_ParamAccess.item, 2);
            pManager[1].Optional = true;
            pManager.AddGenericParameter("Graphic", "G", "A Graphic object", GH_ParamAccess.item);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Shape", "S", "A Hoopoe Shape Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d point = new Point3d();
            double radius = 2.0;
            if (!DA.GetData(0, ref point)) return;
            if (!DA.GetData(1, ref radius)) return;
            Shape shape = new Shape(new Circle(new Plane(point,Vector3d.ZAxis),radius).ToNurbsCurve());

            Graphic graphic = Graphics.FillBlack;
            if (DA.GetData(2, ref graphic)) shape.Graphic = graphic; ;

            DA.SetData(0, shape);
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
                return Properties.Resources.Hoopoe_Point24;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("59b0edf9-d200-4832-a742-b8eb2e002226"); }
        }
    }
}