using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Hoopoe.GH
{
    public class HoopoeGHInfo : GH_AssemblyInfo
  {
    public override string Name
    {
        get
        {
            return "HoopoeGH";
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
            return new Guid("3f974b42-bad5-42a5-93ed-09e895f2461b");
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
}
