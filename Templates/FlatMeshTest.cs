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
        public void FlatMeshTest()
        {
            string message = "";

            FlatMesh fm = new FlatMesh();
            fm.Boundary = new BoundaryRectangle(100, 100);
            fm.HoleList = new Hole[] {
                new Hole()
                {
                    Boundary = new BoundaryEllipse(20, 20),
                    Offset = new Point3D(40, 40, 0)
                },
                new Hole()
                {
                    Boundary = new BoundaryEllipse(10, 30),
                    Offset = new Point3D(60, 60, 0)
                }
            };

            // round trip the properties

            XElement ele = fm.GetProperties();
            fm.LoadProperties(ele, out message);

            ele = fm.Compile();

            GetMesh(ele, "c:\\work\\FlatMesh_APILIB.fbx", true);

            GetMesh(ele, "c:\\work\\FlatMesh_APIWEB.fbx", false);


        }


    }
}

