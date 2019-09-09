using System;
using System.Collections.Generic;
using System.Drawing;

using Sm = System.Windows.Media;
using Si = System.Windows.Media.Imaging;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Hp = Hoopoe;
using Grasshopper.Kernel.Parameters;
using System.IO;

namespace Hoopoe.GH.Outputs
{
    public class ExportSVG : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ExportSVG class.
        /// </summary>
        public ExportSVG()
          : base("Export SVG", "SVG", "Description", "Display", "Drawing")
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
            pManager.AddGenericParameter("Drawing", "D", "---", GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "P", "---", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Name", "N", "---", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Save", "S", "---", GH_ParamAccess.item, false);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Filepath", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Hp.Drawing drawing = new Hp.Drawing();
            if (!DA.GetData<Hp.Drawing>(0, ref drawing)) return;
            
            string path = "C:\\Users\\Public\\Documents\\";
            string name = DateTime.UtcNow.ToString("yyyy-dd-M_HH-mm-ss"); ;
            bool save = false;
            bool hasPath = DA.GetData(1, ref path);
            bool hasName = DA.GetData(2, ref name);
            if (!DA.GetData(3, ref save)) return;
            
            if (!hasPath) { if (this.OnPingDocument().FilePath != null) { path = Path.GetDirectoryName(this.OnPingDocument().FilePath) + "\\"; } } else { path += "//"; }

            string filepath = path + name + ".svg";

            if (save)
            {
                StreamWriter Writer = new StreamWriter(filepath);
                Writer.Write(drawing.ToSVG());
                Writer.Close();
            }

            DA.SetData(0, filepath);
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
                return Properties.Resources.ExportSVG24;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4876f005-76ae-411c-909f-289a0f078532"); }
        }
    }
}