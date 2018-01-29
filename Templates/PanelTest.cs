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

        public void PanelTest()
        {
            int thickness = 30;
            Panel p = new Panel();
            FlatMesh fm = new FlatMesh();
            fm.Boundary = new BoundaryRectangle(100, 100);
            fm.HoleList = new Hole[] {
                new Hole()
                {
                    ID = "fm",
                    Boundary = new BoundaryEllipse(20, 20),
                    Offset = new Point3D(40, 40, 0)
                }
            };

            p.FrontMesh = fm;

            FlatMesh bm = new FlatMesh();
            bm.Boundary = new BoundaryRectangle(100, 100, thickness);
            bm.HoleList = new Hole[] {
                new Hole()
                {
                    ID="bm",
                    Boundary = new BoundaryEllipse(20, 20),
                    Offset = new Point3D(40, 40, thickness)
                }
            };

            p.BackMesh = bm;

            p.ConnectedHoleList = new List<Panel.ConnectedHole>();
            p.ConnectedHoleList.Add(new Panel.ConnectedHole()
            {
                FrontID = "fm",
                BackID = "bm"
            });

            XElement ele = p.Compile();

            GetMesh(ele, "c:\\work\\Panel_APILIB.fbx", true);

            GetMesh(ele, "c:\\work\\Panel_APIWEB.fbx", false);

        }

    }
}

