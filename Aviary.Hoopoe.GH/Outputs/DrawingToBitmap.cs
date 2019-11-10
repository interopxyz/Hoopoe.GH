using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Sm = System.Windows.Media;
using System.Windows.Media.Imaging;
using Sh = System.Windows.Shapes;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Rhino.Geometry;

namespace Aviary.Hoopoe.GH.Outputs
{
    public class DrawingToBitmap : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DrawingToBitmap class.
        /// </summary>
        public DrawingToBitmap()
          : base("Drawing to Bitmap", "Draw To Bitmap", "Viewer for an Aviary drawing", "Aviary 1", "Drawing")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Drawing", "D", "An Aviary drawing object", GH_ParamAccess.item);
            pManager.AddIntegerParameter("PPI", "S", "The pixel per inch value acts as a scalar multiplier. Must be 96 or above", GH_ParamAccess.item, 96);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "A transparent bitmap of the drawing", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Drawing drawing = new Drawing();
            if (!DA.GetData<Drawing>(0, ref drawing)) return;
            Sm.DrawingVisual dwg = drawing.ToGeometryVisual();

            int dpi = 96;
            DA.GetData(1, ref dpi);
            if (dpi < 96) dpi = 96;

            double width = drawing.Width;
            double height = drawing.Height;

            BitmapEncoder encoding = new PngBitmapEncoder();

            DA.SetData(0,dwg.ToBitmap(width, height,dpi, encoding));
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
                return Properties.Resources.DrawingToBitmap;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("acae9659-e301-4e17-85eb-706c77b2ac91"); }
        }
    }
}