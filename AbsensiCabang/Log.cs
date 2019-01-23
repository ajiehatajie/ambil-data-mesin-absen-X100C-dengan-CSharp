using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace AbsensiCabang
{
    class Log
    {
        public void CreateLog(string SuccesOrError, string subject, string desc, string op = null)
        {
            if (!Directory.Exists(@"log"))
            {
                Directory.CreateDirectory(@"log");
            }
            if (!Directory.Exists(@"conf"))
            {
                Directory.CreateDirectory(@"conf");
            }

            StreamWriter log;
            string datelog = DateTime.Now.ToString("yyyy-MM-dd");


            if (!File.Exists(@"log\\" + SuccesOrError.ToString() + "-" + datelog.ToString() + ".log"))
            {
                log = new StreamWriter(@"log\\" + SuccesOrError.ToString() + "-" + datelog.ToString() + ".log");
            }
            else
            {
                log = File.AppendText(@"log\\" + SuccesOrError.ToString() + "-" + datelog.ToString() + ".log");
            }

            log.WriteLine(DateTime.Now + " | " + SuccesOrError.ToString() + "|" + subject.ToString() + " | " + desc.ToString());
            log.Close();
        }
    }
}
