using System;
using System.Collections.Generic;
using System.Linq;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Aviary.Wind.Graphics;

namespace Aviary.Hoopoe.GH
{
    public class MeshToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MeshToShape class.
        /// </summary>
        public MeshToShape()
          : base("Mesh To Shape", "MeshShp", "Convert a mesh to a compound shape", "Aviary 1", "Drawing")
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
            pManager.AddMeshParameter("Mesh", "M", "A mesh whose naked edges define a compound shape", GH_ParamAccess.item);
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
            Mesh mesh = null;
            if (!DA.GetData(0, ref mesh)) return;
            Polyline[] polylines = mesh.GetNakedEdges();
            List<Curve> curves = new List<Curve>();
            foreach(Polyline pline in polylines)
            {
                curves.Add(pline.ToNurbsCurve());
            }

            Shape shape = new Shape(curves);

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
                return Properties.Resources.Hoopoe_Mesh;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("653bb1c0-882b-41c4-82c1-9cce9bb6ca71"); }
        }
    }
}