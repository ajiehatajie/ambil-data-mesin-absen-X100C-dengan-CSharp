using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace AbsensiCabang
{
    public class SchemaInfo
    {
        public SchemaInfo() { port = 4370 ;}
        public string ip { get; set; }
        public Int32 port { get; set; }
        public string jadwal { get; set; }
        public string url { get; set; }
        public string terbaru { get; set; }

        public string cabang { get; set; }
    }

    class Configurasi
    {
        public string Load { get; set; }


        public static SchemaInfo LoadJson()
        {
            string conf = Path.GetFullPath(@"conf\\config.json");
           
            StreamReader sr = File.OpenText(conf);
            var json = sr.ReadToEnd();
            var result = JsonConvert.DeserializeObject<SchemaInfo>(json);
         
            return result;
        
           
        } 
    

    }
}
