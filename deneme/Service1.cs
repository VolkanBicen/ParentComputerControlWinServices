using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Threading;
using System.Net;
using Newtonsoft.Json.Linq;
namespace deneme
{
    public partial class Service1 : ServiceBase
    {   //
        System.Timers.Timer tmr = new System.Timers.Timer();
        string dosyayolu = "D:\\deneme\\log.txt";
        //
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            File.AppendAllText(dosyayolu, "başladı" + DateTime.Now.ToString() + "\r\n");

            tmr.Elapsed += new ElapsedEventHandler(Tmr_Elapsed);
            tmr.Interval = 1000;
            tmr.Start();

            ThreadStart start = new ThreadStart(Th1);
            Thread th1thread = new Thread(start);

            ThreadStart start2 = new ThreadStart(Th2);
            Thread th2thread = new Thread(start2);

            ThreadStart start3 = new ThreadStart(Th3);
            Thread th3thread = new Thread(start3);

            th1thread.Start();

            th2thread.Start();

            th3thread.Start();
        }
        private static void Th1()
        {//uninstall dosya adı gelecek
            foreach (Process p in Process.GetProcesses("."))
            {
                string dosyayolu = "D:\\deneme\\log.txt";
                if (p.ProcessName == "IDMan")
                {
                    var processName = "IDMan";
                    Process[] processes = Process.GetProcessesByName(processName.ToString());
                    foreach (Process process in processes)
                    {
                        process.Kill();
                        File.AppendAllText(dosyayolu, "Process kill" + "\r\n");
                    }
                }
            }
        }
      
        private static void Th2()
        {
            /*string dosyayolu = "D:\\deneme\\log.txt";
             string url = "https://e-kontrol.volkanbicen.xyz/session/control?" +
                   "&token=" + txtKadi.Text;

             var request = (HttpWebRequest)WebRequest.Create(url);
             request.Method = "GET";
             request.ContentType = "application/x-www-form-urlencoded";
             var response = (HttpWebResponse)request.GetResponse();
             var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
             var output = JObject.Parse(responseString);
             /* MessageBox.Show(responseString);
            bool a = Convert.ToBoolean (output["data"]["active"]);
             //MessageBox.Show(a);
             if (a==false)
             {
                 File.AppendAllText(dosyayolu, "false" +"\r\n");
             }
             else if (a == true)
             {
                 File.AppendAllText(dosyayolu, "true" + "\r\n");
             }
             else
             {
                 File.AppendAllText(dosyayolu, "else" + "\r\n");
             }*/
            string dosyayolu = @"C:\Users\Volkan\source\repos\EbebeynPcKontrol\EbebeynPcKontrol\bin\Debug\log.txt";
            StreamReader sr1 = new StreamReader(dosyayolu);
            string[] metin = new string[3];
            for (int i = 0; i < 3; i++)
            {
                metin[i] = sr1.ReadLine();
            }
            string dosyayolu2 = "D:\\deneme\\log.txt";
              string url = "https://e-kontrol.volkanbicen.xyz/session/control?" +
                  "&token=" + metin[2];
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var output = JObject.Parse(responseString);

            string a = (output["data"]["active"]).ToString();

            if (a.Equals("True"))
            {
                File.AppendAllText(dosyayolu2, "Açık" + "\r\n");
            }
            else if (a.Equals("False"))
            {
                File.AppendAllText(dosyayolu2, "Kapalı" + "\r\n");
            }
        }
        public static void Th3()
        {
           
        }
        int a = 0, b = 0;
        public int Kontrol
        {
            
            get
            {
                if (a == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                

                foreach (Process p in Process.GetProcesses("."))
                {

                    if (p.ProcessName == "IDMan")
                    {

                        a = 1;
                    }
                    else
                    {
                        b = 1;
                    }
                }
               
            }
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {

           Th1();
           Th2();

        }
       

        protected override void OnStop()
        {
            File.AppendAllText(dosyayolu, "Durdu" + DateTime.Now.ToString() + "\r\n");
        }
    }
}
