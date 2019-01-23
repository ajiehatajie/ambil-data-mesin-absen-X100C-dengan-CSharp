using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Threading;

namespace AbsensiCabang
{
    class Program
    {
        public string IP;
        static void Main(string[] args)
        {
            Console.WriteLine("console app send absen is running don't close this app \n");
            Console.WriteLine("this app sleep every 45 seconds \n");
            Log log = new Log();
        kembali:
            FetchApi sendAbsen = new FetchApi();
            SendMail sendmail = new SendMail();
            string url = Configurasi.Url().ToString();
            string MachineIp = Configurasi.IPMesin().ToString();
            string MachinePort = Configurasi.PortMesin();
            string cabang = Configurasi.Cabang().ToString();
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
            int iMachineNumber = 1;
            string jadwal = Configurasi.Jadwal().ToString();
            string DataTerbaru = Configurasi.terbaru().ToString();
        

         
            DataSet ds = new DataSet();
          
            string datelog = DateTime.Now.ToString("yyyy-MM-dd");
            string CekDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            
            string jam = DateTime.Now.ToString("HH:mm");

            if (jam == jadwal)
            {

                Console.WriteLine("jadwal push jam {0} mulai ",jadwal.ToString());

                #region proses absen

                try
                {
                    #region baca mesin absen
                    int idwErrorCode = 0;

                    string sdwEnrollNumber = "";
                    int idwVerifyMode = 0;
                    int idwInOutMode = 0;
                    int idwYear = 0;
                    int idwMonth = 0;
                    int idwDay = 0;
                    int idwHour = 0;
                    int idwMinute = 0;
                    int idwSecond = 0;
                    int idwWorkcode = 0;

                    int iGLCount = 0;
                    int iIndex = 0;

                    Boolean bIsConnected = axCZKEM1.Connect_Net(MachineIp, Convert.ToInt32(MachinePort));

                    if (bIsConnected)
                    {
                        iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                        axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                        Console.WriteLine("mesin absen {0} is connected  with port {1} status is {2} \n \n", MachineIp, MachinePort, bIsConnected);

                        axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device

                        if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                        {
                            #region looping data absen
                            Console.WriteLine("start read data to machine \n");
                            Console.WriteLine("please wait.. \n");
                            DataTable dt = new DataTable("logabsen");
                            dt.Columns.Add("id");
                            dt.Columns.Add("badgenumber");
                            dt.Columns.Add("time");
                            dt.Columns.Add("cabang");
                            while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                             out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                            {
                                iGLCount++;
                                string timeAbsen = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();

                                var startDate = DateTime.Parse(datelog);

                                var endDate = new DateTime(int.Parse(idwYear.ToString()), int.Parse(idwMonth.ToString()), int.Parse(idwDay.ToString()));
                                var dateDiff = endDate.Subtract(startDate);

                                if (DataTerbaru == null || DataTerbaru == "n" || DataTerbaru == "N" || DataTerbaru == "")
                                {
                                    dt.Rows.Add(iGLCount, sdwEnrollNumber, timeAbsen, cabang);
                                }
                                else if (DataTerbaru == "Ya" || DataTerbaru =="Y")
                                {
                                    if (dateDiff.TotalDays == 0)
                                    {
                                        dt.Rows.Add(iGLCount, sdwEnrollNumber, timeAbsen, cabang);
                                    }
                                    
                                }
                                
                             
                              

                            }

                            Console.WriteLine("finish read data to machine total {0} \n", iGLCount);
                            ds.Tables.Add(dt);
                            #endregion
                        }
                        else
                        {

                            axCZKEM1.GetLastError(ref idwErrorCode);

                            if (idwErrorCode != 0)
                            {
                                Console.WriteLine("Reading data from terminal failed,ErrorCode: {0} ", idwErrorCode.ToString());
                            }
                            else
                            {
                                Console.WriteLine("No data from terminal returns!");
                            }
                        }

                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                        axCZKEM1.Disconnect();



                        #region send data Absen
                        int i = 0;
                        int y = 0;
                        Int32 totalData = ds.Tables[0].Rows.Count;

                        Console.WriteLine("Please wait ... \n");
                        Console.WriteLine("Absen is sending to HO \n");

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            string badge = dr["badgenumber"].ToString();
                            string checktime = dr["time"].ToString();
                            string checktype = "O";



                            string API_URL = url + "api/v1/absensi/" + badge + '/' + checktime + '/' + checktype + '/' + cabang;

                            Boolean sendApi = sendAbsen.FetchApiAbsensi(API_URL, cabang);

                            if (sendApi == true)
                            {
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                          

                        }
                        Console.WriteLine("Send Data {0} dari total {1} Success Send Data {2} , Failed Send {3} ", i, totalData, i, y);

                        sendmail.SendEmailKeIT("Sukses Kirim Absen Cabang " + cabang, "Total Kirim Data success " + i + " \n Gagal Kirim data " + y);

                        #endregion

                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss") + " Mesin Failed to Connected.");
                    }

                    #endregion

                    Thread.Sleep(45000); // 45 detik
                    Console.WriteLine(DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss") + " Belum Ada Jadwal Push.");

                    bIsConnected = false;

                }
                catch (Exception e)
                {
                    string Log_proses = null;
                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                    Log_proses = e.Message;
                    axCZKEM1.Disconnect();


                    log.CreateLog("error-log", e.StackTrace, e.Message, null);
                  
                    //sendmail("Notif Error Push", e.Message);
                }
                #endregion

            }
            else
            {
                //1 detik 1000
                Thread.Sleep(45000); // 45 detik
                Console.WriteLine(DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss") + " Belum Ada Jadwal Push.");

               
            }
            goto kembali;
        
        
        }

    }
}
