using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Graphics;

namespace Hoopoe.GH.Shapes
{
    public class CurveToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CurveToShape class.
        /// </summary>
        public CurveToShape()
          : base("Curve To Shape", "CrvShp", "Convert a curve to a shape", "Display", "Drawing")
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
            pManager.AddCurveParameter("Curve", "C", "A curve to convert to a shape", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphic", "G", "A Graphic object", GH_ParamAccess.item);
            pManager[1].Optional = true;
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
            Curve curve = null;
            if (!DA.GetData(0, ref curve)) return;
            Shape shape = new Shape(curve);

            Graphic graphic = new Graphic();
            if (DA.GetData(1, ref graphic)) shape.Graphic = graphic; ;
            
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
                return Properties.Resources.Hoopoe_Curve24;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("443893ff-6117-450f-8a04-bbdb71985985"); }
        }
    }
}