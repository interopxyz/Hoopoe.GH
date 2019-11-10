using System;
using System.Collections.Generic;
using System.Linq;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Aviary.Wind.Graphics;

namespace Aviary.Hoopoe.GH
{
    public class BrepToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BrepToShape class.
        /// </summary>
        public BrepToShape()
          : base("Brep To Shape", "BrepShp", "Convert a brep's naked edges to a compound shape", "Aviary 1", "Drawing")
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
            pManager.AddBrepParameter("Brep", "B", "A mesh whose naked edges define a compound shape", GH_ParamAccess.item);
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
            Brep brep = null;
            if (!DA.GetData(0, ref brep)) return;
            Curve[] edges = brep.DuplicateNakedEdgeCurves(true, true);
            Curve[] curves = Curve.JoinCurves(edges);

            Shape shape = new Shape(curves.ToList());

            Graphic graphic = Graphics.FillBlack;
            if (DA.GetData(1, ref graphic)) shape.Graphic = new Graphic(graphic); ;

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
                return Properties.Resources.Hoopoe_Surface;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3f202314-d98f-40c7-b6f4-0fc466d0d5ef"); }
        }
    }
}