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
        public void BuildingWithWallsTest()
        {
            // Test 1: Default building (3 floors, standard dimensions)
            BuildingWithWallsTemplate building1 = new BuildingWithWallsTemplate();
            XElement ele = building1.Compile();
            GetMesh(ele, "c:\\work\\BuildingWithWalls_Default_APILIB.fbx", true);

            // Test 2: Tall office building (6 floors, larger footprint)
            BuildingWithWallsTemplate building2 = new BuildingWithWallsTemplate
            {
                FloorCount = 6,
                FloorHeight = 120,
                BuildingWidth = 600,
                BuildingLength = 800,
                BuildingWallWidth = 15,
                WindowWidth = 15,
                WindowHeight = 50
            };
            ele = building2.Compile();
            GetMesh(ele, "c:\\work\\BuildingWithWalls_TallOffice_APILIB.fbx", true);

            // Test 3: Stairwell on left side
            BuildingWithWallsTemplate building3 = new BuildingWithWallsTemplate
            {
                FloorCount = 4,
                StairWellDirection = BuildingWithWallsTemplate.eStairWellDirection.LeftToRight,
                StairwellOffset = new Point2D() { X = 50, Y = 100 }
            };
            ele = building3.Compile();
            GetMesh(ele, "c:\\work\\BuildingWithWalls_LeftStairs_APILIB.fbx", true);

            // Test 4: Compact building (2 floors, small footprint)
            BuildingWithWallsTemplate building4 = new BuildingWithWallsTemplate
            {
                FloorCount = 2,
                FloorHeight = 90,
                BuildingWidth = 300,
                BuildingLength = 400,
                StairWidth = 60,
                StairLength = 120,
                StairwellOffset = new Point2D() { X = 150, Y = 150 }
            };
            ele = building4.Compile();
            GetMesh(ele, "c:\\work\\BuildingWithWalls_Compact_APILIB.fbx", true);

            // Test 5: Custom window pattern
            BuildingWithWallsTemplate building5 = new BuildingWithWallsTemplate
            {
                FloorCount = 3,
                WindowPattern = new List<int>() { 1, 1, 1, 0, 1, 1, 1 }, // More windows
                WindowWidth = 20,
                WindowHeight = 60
            };
            ele = building5.Compile();
            GetMesh(ele, "c:\\work\\BuildingWithWalls_CustomWindows_APILIB.fbx", true);

            // Test 6: Back-to-front stairwell
            BuildingWithWallsTemplate building6 = new BuildingWithWallsTemplate
            {
                FloorCount = 5,
                StairWellDirection = BuildingWithWallsTemplate.eStairWellDirection.BackToFront,
                BuildingWidth = 500,
                BuildingLength = 700
            };
            ele = building6.Compile();
            GetMesh(ele, "c:\\work\\BuildingWithWalls_BackToFront_APILIB.fbx", true);
        }

        public void StairWellTemplateTest()
        {
            // Test standalone stairwell template
            StairWellTemplate1 stairwell = new StairWellTemplate1
            {
                FloorCount = 4,
                FloorHeight = 100,
                StairCount = 12,
                StairWidth = 40,
                StairLength = 150,
                StairWellLength = 200,
                StairWellWidth = 120
            };
            XElement ele = stairwell.Compile();
            GetMesh(ele, "c:\\work\\StairWellTemplate_Standalone_APILIB.fbx", true);
        }

        public void BuildingShellTemplateTest()
        {
            // Test standalone building shell (no stairwell)
            BuildingShellTemplate shell = new BuildingShellTemplate
            {
                FloorCount = 4,
                FloorHeight = 100,
                BuildingLength = 600,
                BuildingWidth = 500,
                BuildingWallWidth = 12,
                WindowWidth = 30,
                WindowHeight = 50,
                RoofCapHeight = 40  // Add a roof cap
            };
            XElement ele = shell.Compile();
            GetMesh(ele, "c:\\work\\BuildingShell_Standalone_APILIB.fbx", true);
        }
    }
}
