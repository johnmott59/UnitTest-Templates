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
        public void SingleRoomBuildingTest()
        {
            string message = "";

            SingleRoomBuilding srb = new SingleRoomBuilding
            {
                Width = 40,
                Length = 60,
                Height = 12,
                Thickness = 5,
                RoofOffset = -2,
                HorizontalScale = 1,
                VerticalScale = 1
            };

            // Add door on front wall
            srb.Door = new Hole
            {
                Boundary = new BoundaryRectangle(3, 7),
                Offset = new Point3D(20, 0, 2)
            };

            // Add front window
            srb.FrontWindow = new Hole
            {
                Boundary = new BoundaryRectangle(4, 3),
                Offset = new Point3D(10, 0, 6)
            };

            // Add left window
            srb.LeftWindow = new Hole
            {
                Boundary = new BoundaryRectangle(4, 3),
                Offset = new Point3D(30, 0, 6)
            };

            // Add rear window
            srb.RearWindow = new Hole
            {
                Boundary = new BoundaryRectangle(4, 3),
                Offset = new Point3D(30, 0, 6)
            };

            // Add right window
            srb.RightWindow = new Hole
            {
                Boundary = new BoundaryRectangle(4, 3),
                Offset = new Point3D(30, 0, 6)
            };

            // Round trip the properties
            XElement ele = srb.GetProperties();
            srb.LoadProperties(ele, out message);

            ele = srb.Compile();

            // Wrap in scene container
            XElement scene = new XElement("scene", ele);

            GetMesh(scene, "c:\\work\\SingleRoomBuilding_APILIB.fbx", true);
        }
    }
}
