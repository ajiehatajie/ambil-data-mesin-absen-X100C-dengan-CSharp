using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace AbsensiCabang
{
    class Configurasi
    {

        public static string Cabang()
        {

            string conf = Path.GetFullPath(@"conf\\cabang.conf");
            string line = null;
            string pathconf = null;
            StreamReader sr = File.OpenText(conf);
            while ((line = sr.ReadLine()) != null)
            {
                pathconf += line;
            } sr.Close();
            if (pathconf == null)
            {
                pathconf = "";
            }
            else
            {
                pathconf = pathconf.Trim();
            }


            return pathconf;
        }
        public static string Jadwal()
        {

            string conf = Path.GetFullPath(@"conf\\jadwal.conf");
            string line = null;
            string pathconf = null;
            StreamReader sr = File.OpenText(conf);
            while ((line = sr.ReadLine()) != null)
            {
                pathconf += line;
            } sr.Close();
            if (pathconf == null)
            {
                pathconf = "";
            }
            else
            {
                pathconf = pathconf.Trim();
            }


            return pathconf;
        }

        public static string terbaru()
        {

            string conf = Path.GetFullPath(@"conf\\terbaru.conf");
            string line = null;
            string pathconf = null;
            StreamReader sr = File.OpenText(conf);
            while ((line = sr.ReadLine()) != null)
            {
                pathconf += line;
            } sr.Close();
            if (pathconf == null)
            {
                pathconf = "";
            }
            else
            {
                pathconf = pathconf.Trim();
            }


            return pathconf;
        }
        public static string Url()
        {

            string conf = Path.GetFullPath(@"conf\\url.conf");
            string line = null;
            string pathconf = null;
            StreamReader sr = File.OpenText(conf);
            while ((line = sr.ReadLine()) != null)
            {
                pathconf += line;
            } sr.Close();
            if (pathconf == null)
            {
                pathconf = "";
            }
            else
            {
                pathconf = pathconf.Trim();
            }


            return pathconf;
        }

        public static string IPMesin()
        {

            string conf = Path.GetFullPath(@"conf\\ip.conf");
            string line = null;
            string pathconf = null;
            StreamReader sr = File.OpenText(conf);
            while ((line = sr.ReadLine()) != null)
            {
                pathconf += line;
            } sr.Close();
            if (pathconf == null)
            {
                pathconf = "";
            }
            else
            {
                pathconf = pathconf.Trim();
            }


            return pathconf;
        }
        public static string PortMesin()
        {

            string conf = Path.GetFullPath(@"conf\\port.conf");
            string line = null;
            string pathconf = null;
            StreamReader sr = File.OpenText(conf);
            while ((line = sr.ReadLine()) != null)
            {
                pathconf += line;
            } sr.Close();
            if (pathconf == null)
            {
                pathconf = "4370";
            }
            else
            {
                pathconf = pathconf.Trim();
            }


            return pathconf;
        }


    }
}
