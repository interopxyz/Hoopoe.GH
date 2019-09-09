using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Sm = System.Windows.Media;
using System.Windows.Media.Imaging;
using Sh = System.Windows.Shapes;

using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Rhino.Geometry;
using Hp = Hoopoe;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Hoopoe.GH
{
    public class HoopoeViewer : GH_Component
    {
        public Image img = null;
        string message = "Nothing here";
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created. 
        /// </summary>
        public HoopoeViewer()
          : base("Drawing Viewer", "Canvas", "A viewer for drawings", "Display", "Drawing")
        {
        }

        public override void CreateAttributes()
        {
            img = Properties.Resources.Hoopoe_A;
            m_attributes = new Attributes_Custom(this);
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
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Hp.Drawing drawing = new Hp.Drawing();
            if (!DA.GetData<Hp.Drawing>(0, ref drawing)) return;
            Sm.DrawingVisual dwg = drawing.ToGeometryVisual();

            double width = drawing.Width;
            double height = drawing.Height;
            if (width < 100) width = 100;
            if (height< 100) height = 100;

            img = dwg.ToBitmap(width,height);
            message = drawing.ToString();
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            Message = message;
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Properties.Resources.Viewer24;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0862e44a-ed62-4339-8de5-96a540d47c86"); }
        }
    }
    

    public class Attributes_Custom : GH_ComponentAttributes
    {
        public Attributes_Custom(GH_Component owner) : base(owner) { }

        private Rectangle ButtonBounds { get; set; }
        protected override void Layout()
        {
            base.Layout();
            HoopoeViewer comp = Owner as HoopoeViewer;

            int width = comp.img.Width;
            int height = comp.img.Height;
            Rectangle rec0 = GH_Convert.ToRectangle(Bounds);

            int cWidth = rec0.Width;
            int cHeight = rec0.Height;

            rec0.Width = width;
            rec0.Height += height;

            Rectangle rec1 = rec0;
            rec1.Y = rec1.Bottom - height;
            rec1.Height = height;
            rec1.Width = width;

            Bounds = rec0;
            ButtonBounds = rec1;

        }

        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);
            HoopoeViewer comp = Owner as HoopoeViewer;

            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule capsule = GH_Capsule.CreateCapsule(ButtonBounds, GH_Palette.Normal, 0, 0);
                capsule.Render(graphics, Selected, Owner.Locked, true);
                capsule.AddOutputGrip(this.OutputGrip.Y);
                capsule.Dispose();
                capsule = null;

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                RectangleF textRectangle = ButtonBounds;

                graphics.DrawImage(comp.img, Bounds.X + 2, m_innerBounds.Y - (ButtonBounds.Height - Bounds.Height), comp.img.Width - 4, comp.img.Height - 2);

                format.Dispose();
            }
        }
    }
}
