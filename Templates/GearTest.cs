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

        public void GearTest()
        {
            GearBase gb = new GearBase();
            gb.Width = 40;

            gb.GearHoles.HoleList = new Hole[1];
            gb.GearHoles.HoleList[0] = new Hole()
            {
                Boundary = new BoundaryEllipse(100, 100),
                Offset = new Point3D(0, 0, 0)
            };

            XElement pr = gb.GetProperties();

            XElement ele = gb.Compile();

            GetMesh(ele, "c:\\work\\GearBase_APILIB.fbx", true);

            GetMesh(ele, "c:\\work\\GearBase_APIWEB.fbx", false);

        }
    }
}

