using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
namespace AbsensiCabang
{
    class FetchApi
    {
        Log log = new Log();
        SendMail sendmail = new SendMail();
        public Boolean FetchApiAbsensi(string url, string Cabang)
        {
            Log log = new Log();
            SendMail sendmail = new SendMail();

            string content_result = null;
            string error_log_position = null;

            var requesta = WebRequest.Create(url);
            requesta.Timeout = 9000000;
            try
            {
                var resa = requesta.GetResponse();
                using (var reader = new StreamReader(resa.GetResponseStream()))
                {
                    content_result = reader.ReadToEnd();
                }
                resa.Close();

                //log.CreateLog("succes-send-api", url + " | proses Send API ", null);

                return true;
            }
            catch (Exception error)
            {
                error_log_position = "Send API" + error.Message;
                log.CreateLog("error-push-send-api", error.StackTrace, error.Message + " | proses Send API " + url, null);
                //sendmail.SendEmailKeIT("Error Send Absensi Cabang " + Cabang , error.StackTrace + "\n" + error.Message + url);
                return false;


            }


        }
    }
}
