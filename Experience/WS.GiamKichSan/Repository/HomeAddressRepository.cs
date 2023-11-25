using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WS.GiamKichSan.Models;

namespace WS.GiamKichSan.Repository
{
    public class HomeAddressRepository
    {
        public async Task<bool> Insert(string ipAddress)
        {
            HomeAddress_Entity item = new HomeAddress_Entity();
            item.IsActive = true;
            item.DateCreate = DateTime.Now;
            item.IPAddress = ipAddress;

            HttpClient httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(SessionGlobal.Url + "/MyIPs/HomeAddress", content);
            Console.WriteLine("response.IsSuccessStatusCode  : " + response.IsSuccessStatusCode);
            httpClient.Dispose();

            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetIPInternet()
        {
            HttpClient httpClient = new HttpClient();
            var ipAddress = string.Empty;
            HttpResponseMessage response = await httpClient.GetAsync("http://checkip.dyndns.org");
            if (response.IsSuccessStatusCode)
            {
                string htmltext = await response.Content.ReadAsStringAsync();
                int first = htmltext.IndexOf("Address: ") + 9;
                int last = htmltext.LastIndexOf("</body>");
                ipAddress = htmltext.Substring(first, last - first);
            }
            httpClient.Dispose();
            return ipAddress;
        }
    }
}
