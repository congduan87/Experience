using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using WS.GiamKichSan.Repository;

namespace WS.GiamKichSan
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer1;
        private HomeAddressRepository homeAddressRepository { get; set; }
        public Service1()
        {
            InitializeComponent();
            timer1 = new Timer();
        }

        protected override void OnStart(string[] args)
        {
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory());
            Console.WriteLine($"Service is start at {DateTime.Now}");
            SessionGlobal.Url = "https://api.giamkichsan.com";
            homeAddressRepository = new HomeAddressRepository();
            timer1.Enabled = true;
            timer1.Elapsed += Timer1_Elapsed;
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (timer1.Interval != 30 * 60 * 1000)
                {
                    timer1.Interval = 30 * 60 * 1000; //number in milisecinds
                }
                var ipAddress = homeAddressRepository.GetIPInternet().Result;
                if (!ipAddress.Equals(SessionGlobal.IPAddress) && homeAddressRepository.Insert(ipAddress).Result)
                {
                    SessionGlobal.IPAddress = ipAddress;
                    Console.WriteLine($"Service is change at {DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Service is error '{ex.Message}' at {DateTime.Now}");
            }
        }

        protected override void OnStop()
        {
            Console.WriteLine($"Service is stop at {DateTime.Now}");
        }
    }
}
