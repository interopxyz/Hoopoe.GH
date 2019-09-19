using System;
using System.Collections.Generic;
using System.Drawing;
using Sm = System.Windows.Media;
using Si = System.Windows.Media.Imaging;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using System.IO;

using Hp = Aviary.Hoopoe;

namespace Aviary.Hoopoe.GH
{
    public class ExportBmp : GH_Component
    {

        /// <summary>
        /// Initializes a new instance of the ExportBmp class.
        /// </summary>
        public ExportBmp()
          : base("Export Bitmap", "Bitmap", "Save a Aviary Drawing to a bitmap file", "Aviary 1", "Drawing")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Drawing", "D", "An Aviary drawing object", GH_ParamAccess.item);
            pManager.AddIntegerParameter("DPI", "R", "Set the Dots Per Inch scaling of the image", GH_ParamAccess.item, 96);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Path", "P", "Set the filepath", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddTextParameter("Name", "N", "Set the filename (no extension)", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Extention", "E", "Select an Extension (png=0, jpeg=1, bmp=2, tiff=3, gif=4)", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;
            pManager.AddBooleanParameter("Save", "S", "Will save the file when true (recommend using a button)", GH_ParamAccess.item, false);
            pManager[5].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[4];
            param.AddNamedValue("Png", 0);
            param.AddNamedValue("Jpeg", 1);
            param.AddNamedValue("Bmp", 2);
            param.AddNamedValue("Tiff", 3);
            param.AddNamedValue("Gif",4);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Filepath", "F", "The resulting filepath", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {   
            Hp.Drawing drawing = new Hp.Drawing();
            if (!DA.GetData<Hp.Drawing>(0, ref drawing)) return;

            int dpi = 96;
            string path = "C:\\Users\\Public\\Documents\\";
            string name = DateTime.UtcNow.ToString("yyyy-dd-M_HH-mm-ss"); ;
            int extension = 0;
            string format = ".png";
            bool save = false;
            if(!DA.GetData(1, ref dpi)) return;
            bool hasPath = DA.GetData(2, ref path);
            bool hasName = DA.GetData(3, ref name);
            if(!DA.GetData(4, ref extension)) return;
            if (!DA.GetData(5, ref save)) return;

            Si.BitmapEncoder encoding = new Si.PngBitmapEncoder();
            switch(extension)
            {
                case 1:
                    encoding = new Si.JpegBitmapEncoder();
                    format = ".jpeg";
                    break;
                case 2:
                    encoding = new Si.BmpBitmapEncoder();
                    format = ".bmp";
                    break;
                case 3:
                    encoding = new Si.TiffBitmapEncoder();
                    format = ".tiff";
                    break;
                case 4:
                    encoding = new Si.GifBitmapEncoder();
                    format = ".gif";
                    break;
            }

            if (!hasPath) { if (this.OnPingDocument().FilePath != null) { path = Path.GetDirectoryName(this.OnPingDocument().FilePath) + "\\"; } } else { path += "//"; }

            string filepath = path + name + format;

            Sm.DrawingVisual dwg = drawing.ToGeometryVisual();
            Bitmap bitmap = dwg.ToBitmap(drawing.Width, drawing.Height, dpi, encoding);

            if(save)
            {
                bitmap.Save(filepath);
                bitmap.Dispose();
            }

            DA.SetData(0,filepath);
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
                return Properties.Resources.ExportBitmap;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6f6f7370-0e9f-459d-b06f-86146470f832"); }
        }
    }
}