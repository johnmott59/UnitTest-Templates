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
        public void StraightStairsWithRunListTest()
        {
            string message = "";

            StraightStairsWithRunList ss = new StraightStairsWithRunList
            {
                VerticalDistance = 10,
                HorizontalDistance = 10,
                Width = 30,
                Rise = 8,
                RunList = new int[] { 10, 12, 10, 14, 10 },
                LeftSideTexture = "leftside.png",
                RightSideTexture = "rightside.png",
                StairTexture = "stair.png"
            };

            // Round trip the properties
            XElement ele = ss.GetProperties();
            ss.LoadProperties(ele, out message);

            ele = ss.Compile();

            GetMesh(ele, "c:\\work\\StraightStairsWithRunList_APILIB.fbx", true);
        }
    }
}
