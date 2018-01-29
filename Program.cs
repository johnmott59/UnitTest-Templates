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
using System.Net;
using System.IO;

namespace ShapeTemplateLibUnitTest
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            p.FlatMeshTest();

            p.PanelTest();

            p.SingleRoomTest();

            p.GearTest();

            p.StraitStairs();

            p.SimpleLayout();

        }

        public void GetMesh(XElement ele, string outputFile,bool bUseAPILib)
        {
            string TempFileName = "";
            APIStatus sts;
            if (!bUseAPILib)
            {
                string url = "http://www.meshola.com/API/APIAlpha";
                string command = "getmesh";
                string xmldata = ele.ToString();

                // Post to the API url and get back a file
                using (WebClient client = new WebClient())
                {
                    var reqparm = new System.Collections.Specialized.NameValueCollection();
                    reqparm.Add("uk", "");
                    reqparm.Add("command", command);
                    reqparm.Add("xmldata", xmldata);
                    reqparm.Add("outputfile", "HelloStairs.fbx");
                    Console.WriteLine("before API call");
                    byte[] responsebytes = client.UploadValues(url, "POST", reqparm);
                    MemoryStream ms = new MemoryStream(responsebytes);
                    Console.WriteLine("after API call");
                    TempFileName = Path.GetTempFileName();
                    File.WriteAllBytes(TempFileName, responsebytes);
                }
            }
            else
            {
                API oAPI = new API();
                sts = oAPI.APIEntry(eAPICommand.GetMesh, ele);
                if (sts.Success == false) throw new Exception("Failed to get mesh from API");
                TempFileName = sts.OutputFile;
            }

            File.Delete(outputFile);
            File.Copy(TempFileName, outputFile);
        }
    }
}

