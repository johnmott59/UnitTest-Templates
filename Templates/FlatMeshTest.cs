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
            string logPath = "c:\\work\\FlatMeshTest.log.txt";
            File.WriteAllText(logPath, ""); // Clear log file

            void Log(string msg)
            {
                string line = $"[{DateTime.Now:HH:mm:ss.fff}] {msg}";
                Console.WriteLine(line);
                File.AppendAllText(logPath, line + "\r\n");
            }

            Log("=== FLATMESH TEST START ===");
            string message = "";

            // Step 1: Create FlatMesh with holes
            Log("Step 1: Creating FlatMesh (100x100) with 2 elliptical holes");
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
            Log($"  - Boundary: BoundaryRectangle 100x100");
            Log($"  - Hole 1: BoundaryEllipse 20x20 at offset (40, 40, 0)");
            Log($"  - Hole 2: BoundaryEllipse 10x30 at offset (60, 60, 0)");

            // Step 2: Get properties (serialize to XML)
            Log("Step 2: Serializing FlatMesh to XML via GetProperties()");
            XElement ele = fm.GetProperties();
            string step2Path = "c:\\work\\FlatMesh_step2_properties.xml";
            ele.Save(step2Path);
            Log($"  - Saved to: {step2Path}");
            Log($"  - XML root element: {ele.Name}");
            Log($"  - HoleList element exists: {ele.Descendants("holelist").Any()}");
            int holeCount = ele.Descendants("hole").Count();
            Log($"  - Hole elements in XML: {holeCount}");

            // Step 3: Round-trip test (load properties back)
            Log("Step 3: Round-trip test - loading properties back into new FlatMesh");
            fm.LoadProperties(ele, out message);
            Log($"  - LoadProperties result: {(string.IsNullOrEmpty(message) ? "OK" : message)}");
            Log($"  - HoleList after reload: {(fm.HoleList != null ? fm.HoleList.Length.ToString() : "null")} holes");

            // Step 4: Compile to BasicShape
            Log("Step 4: Compiling FlatMesh (should return same XML for BasicShape)");
            ele = fm.Compile();
            string step4Path = "c:\\work\\FlatMesh_step4_compiled.xml";
            ele.Save(step4Path);
            Log($"  - Saved to: {step4Path}");
            holeCount = ele.Descendants("hole").Count();
            Log($"  - Hole elements in compiled XML: {holeCount}");

            // Step 4b: Wrap in <scene> container (required by GetMesh)
            Log("Step 4b: Wrapping FlatMesh in <scene> container");
            XElement scene = new XElement("scene", ele);
            string step4bPath = "c:\\work\\FlatMesh_step4b_scene.xml";
            scene.Save(step4bPath);
            Log($"  - Saved to: {step4bPath}");
            Log($"  - Root element: {scene.Name.LocalName}");
            Log($"  - Child count: {scene.Elements().Count()}");

            // Step 5: Generate mesh via APILib
            Log("Step 5: Calling APILib to generate mesh");
            APILib.DebugLogger.WriteLineCallback = (msg) => Log($"  [APILib] {msg}");

            string fbxPath = "c:\\work\\FlatMesh_APILIB.fbx";
            GetMesh(scene, fbxPath, true);

            Log($"  - FBX file saved to: {fbxPath}");
            Log($"  - FBX file size: {new FileInfo(fbxPath).Length} bytes");

            Log("=== FLATMESH TEST COMPLETE ===");
            Log($"Log saved to: {logPath}");
        }


    }
}