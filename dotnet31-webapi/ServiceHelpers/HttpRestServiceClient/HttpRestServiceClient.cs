using cmes_webapi.Common.Configuration;
using cmes_webapi.ServiceHelper.Contracts;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace cmes_webapi.Common.Services
{

    public static class HttpRestServiceClient 
    {
        //private static readonly HttpClient client = new HttpClient();
        public static R Send<T, R>(string url, T param)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;
            value = response.Content.ReadAsJsonAsync<R>().Result;
            //value = JsonConvert.DeserializeObject<R>(response.Content.ToString());

            return value;
        }

        public static R Send<T, R>(string url, T param, ref string output)
        {
            HttpClient client = new HttpClient();
            R value = default(R);
            //  client.Timeout = TimeSpan.FromMilliseconds(10);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;
            output = response.Content.ReadAsStringAsync().Result.ToString();
            value = response.Content.ReadAsJsonAsync<R>().Result;
            return value;
        }

        public static R Send<T, R>(string url, T param, Authentication.Credential credential, ref string output)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (credential != null)
            {
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(credential.UserName + ":" + credential.Password));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);
            }

            HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;

            output = response.Content.ReadAsStringAsync().Result.ToString();
            value = response.Content.ReadAsJsonAsync<R>().Result;
            return value;
        }

        public static R SendGet<T, R>(string url, T param, ref string output)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            output = response.Content.ReadAsStringAsync().Result.ToString();
            value = response.Content.ReadAsJsonAsync<R>().Result;

            return value;
        }

        public static R SendGet<R>(string url, ref string output)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            output = response.Content.ReadAsStringAsync().Result.ToString();
            value = response.Content.ReadAsJsonAsync<R>().Result;

            return value;
        }

        public static R SendGet<R>(string url, Authentication.Credential credential, ref string output)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (credential != null)
            {
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(credential.UserName + ":" + credential.Password));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);
            }

            //HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;
            HttpResponseMessage response = client.GetAsync(url).Result;

            output = response.Content.ReadAsStringAsync().Result.ToString();
            value = response.Content.ReadAsJsonAsync<R>().Result;

            return value;
        }

        public static R SendPut<R>(string url, Authentication.Credential credential, ref string output)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (credential != null)
            {
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(credential.UserName + ":" + credential.Password));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);
            }
            var httpContent = new StringContent("", Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;
            HttpResponseMessage response = client.PutAsync(url, httpContent).Result;

            output = response.Content.ReadAsStringAsync().Result.ToString();
            value = response.Content.ReadAsJsonAsync<R>().Result;

            return value;
        }

        public static async Task<R> SendAsync<T, R>(string url, T param)
        {
            HttpClient client = new HttpClient();
            R value = default(R);

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;

            value = await response.Content.ReadAsJsonAsync<R>();

            return value;
        }

        public static void SendAsync<T>(string url, T param)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(url, param).Result;
        }
        public static void SendAsync(string url, int ObjectID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(url, ObjectID).Result;
        }

        public static bool? PortOpen(int port)
        {

            bool open = false;
            if (port == 0)
                return null;
            try
            {
                using (System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp))
                {    //tc.Connect(< server ipaddress >, < port number >);
                    string hostname = GlobalSettings.Server;//   "127.0.0.1";
                    IPAddress ipa = Dns.GetHostAddresses(hostname)[0];

                    sock.Connect(ipa, port);
                    if (sock.Connected == true)  // Port is in use and connection is successful
                        open = true;
                    sock.Close();
                }
                return open;
            }
            catch (System.Net.Sockets.SocketException)
            {
                open = false;
                return open;
            }

            catch (Exception ex)
            {
                string error = ex.Message;
                return open;
            }
        }


        public class ResponseObject
        {
            public int ObjectID { get; set; }
            public string Status { get; set; }
            public int? SecondaryObjectID { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorDescription { get; set; }
            public string Action { get; set; }
            public string StringObjectID { get; set; }
            /// <summary>
            /// Generic property 
            /// </summary>
            public int ParameterID { get; set; }

            public ResponseObject()
            {
                this.ObjectID = 0;
                this.ErrorCode = "";
                this.ErrorDescription = "";
                this.StringObjectID = "";
            }
        }

    }
}
