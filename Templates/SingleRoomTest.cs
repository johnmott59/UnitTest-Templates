using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using APILib;
using System.Xml.Linq;
using System.IO;

namespace ShapeTemplateLibUnitTest
{
    public partial class Program
    {
     
        public void SingleRoomTest()
        {
            string message = "";

            SingleRoomBuilding srb = new SingleRoomBuilding();

            XElement pr = srb.GetProperties();
            srb.LoadProperties(pr, out message);

            XElement ele = srb.Compile();

            GetMesh(ele, "c:\\work\\SingleRoom_APILIB.fbx", true);

            GetMesh(ele, "c:\\work\\SingleRoom_APIWEB.fbx", false);
        }


    }
}

