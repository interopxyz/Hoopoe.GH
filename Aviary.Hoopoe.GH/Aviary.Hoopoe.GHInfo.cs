using System;
using System.Drawing;
using Grasshopper;
using Grasshopper.Kernel;

namespace Aviary.Hoopoe.GH
{
    public class AviaryHoopoeGHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "AviaryHoopoeGH";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("01c6de72-e236-41a2-aa4d-676ef67a0da0");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
    public class AviaryCategoryIcon : GH_AssemblyPriority
    {
        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddCategoryIcon("Aviary 1", Properties.Resources.Aviary_Logo_sm);
            Instances.ComponentServer.AddCategorySymbolName("Aviary 1", 'A');
            return GH_LoadingInstruction.Proceed;
        }
    }
}
