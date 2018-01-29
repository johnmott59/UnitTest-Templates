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
        public void StraitStairs()
        {
            string message = "";

            StraightStairs ss = new StraightStairs();

            // round trip the properties

            XElement ele = ss.GetProperties();
            ss.LoadProperties(ele, out message);

            ele = ss.Compile();

            GetMesh(ele, "c:\\work\\StraightStairs_APILIB.fbx", true);

            GetMesh(ele, "c:\\work\\StraightStairs_APIWEB.fbx", false);


        }


    }
}

