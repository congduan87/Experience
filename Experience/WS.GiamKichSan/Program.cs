using System.ServiceProcess;
using WS.GiamKichSan.Repository;

namespace WS.GiamKichSan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }

        //static void Main()
        //{
        //    SessionGlobal.Url = "http://localhost:35508";
        //    var homeAddressRepository = new HomeAddressRepository();

        //    var ipAddress = homeAddressRepository.GetIPInternet().Result;
        //    if (!ipAddress.Equals(SessionGlobal.IPAddress) && homeAddressRepository.Insert(ipAddress).Result)
        //    {
        //        SessionGlobal.IPAddress = ipAddress;
        //    }
        //}
    }
}
