using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSConfluenceAutomationFWWPFLib
{
    public class ConfluenceAPIMetodusok
    {
        
        private static readonly log4net.ILog _naplo = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string APPSETTINGS_TERAZONOSITO = ConfigurationManager.AppSettings["TerAzonosito"];
        public string APPSETTINGS_SZULOOSZTALYNEVE = ConfigurationManager.AppSettings["SzuloOsztalyNeve"];
        public string APPSETTINGS_OLDALNEVE = ConfigurationManager.AppSettings["OldalNeve"];

        public ConfluenceAPIMetodusok() {

        }


        public bool IsPageExists(string URL, string cim, string terAzonosito, string felhasznaloNev, string jelszo, int idHossza)
        {
            try
            {

                string oldalAzonosito = GetOldalIDNevAlapjan(felhasznaloNev, jelszo, terAzonosito, URL, cim, idHossza);

                try
                {
                    int oldalAzonositoSzam = Convert.ToInt32(oldalAzonosito);
                }
                catch (Exception exception)
                {
                    return false;
                }

                bool eredmeny = false;
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), URL + "/" + oldalAzonosito + "?status=any"))
                    {
                        var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(felhasznaloNev + ":" + jelszo));
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                        //var response = await httpClient.SendAsync(request).Result;
                        HttpResponseMessage message = httpClient.SendAsync(request).Result;
                        string description = string.Empty;
                        string result = message.Content.ReadAsStringAsync().Result;
                        description = result;

                        ConfluenceAPIResponse JSONObj = new ConfluenceAPIResponse();
                        JSONObj = JsonConvert.DeserializeObject<ConfluenceAPIResponse>(result);
                        if (JSONObj.statusCode == null)
                        {
                            eredmeny = true;
                        }
                    }
                }
                return eredmeny;
            }
            catch (Exception exception)
            {
                _naplo.Error(exception.StackTrace);
                return false;
            }

        

    }

        public string AddConfluencePage(string cim, string terAzonosito, string szuloOsztalyNeve, string html, string URL, string felhasznaloNev, string jelszo, int idHossza)
        {
            try
            {
                if (szuloOsztalyNeve.Equals(""))
                {
                    szuloOsztalyNeve = APPSETTINGS_SZULOOSZTALYNEVE;
                }
                if (terAzonosito.Equals(""))
                {
                    terAzonosito = APPSETTINGS_TERAZONOSITO;
                }
                if (cim.Equals(""))
                {
                    cim = APPSETTINGS_OLDALNEVE;
                }

                html = html.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\"", "'");

                string szuloOsztalyAzonosito = GetOldalIDNevAlapjan(felhasznaloNev, jelszo, terAzonosito, URL, szuloOsztalyNeve, idHossza);

                string DATA = "{\"type\":\"page\",\"ancestors\":[{\"type\":\"page\",\"id\":" + szuloOsztalyAzonosito +
                    "}],\"title\":\"" + cim + "\",\"space\":{\"key\":\"" + terAzonosito + "\"},\"body\":{\"storage\":{\"value\":\""
                    + html + "\",\"representation\":\"storage\"}}}";

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new System.Uri(URL);
                byte[] cred = UTF8Encoding.UTF8.GetBytes(felhasznaloNev + ":" + jelszo);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage message = client.PostAsync(URL, content).Result;
                string description = string.Empty;
                string result = message.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception exception)
            {
                _naplo.Error(exception.StackTrace);
                return "";
            }
        }

        public string UpdateConfluencePage(string cim, string terAzonosito, string html, string URL, string felhasznaloNev, string jelszo, string verzioSzam, int idHossza)
        {
            try
            {
                if (cim.Equals(""))
                {
                    cim = APPSETTINGS_OLDALNEVE;
                }

                html = html.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\"", "'");

                string oldalAzonositoja = GetOldalIDNevAlapjan(felhasznaloNev, jelszo, terAzonosito, URL, cim, idHossza);

                string DATA = "{\"version\":{\"number\":" + verzioSzam + "},\"title\":\"" + cim + "\",\"type\":\"page\",\"body\"" +
                    ":{\"storage\":{\"value\":\"" + html + "\",\"representation\":\"storage\"}}}";

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new System.Uri(URL + "/" + oldalAzonositoja);
                byte[] cred = UTF8Encoding.UTF8.GetBytes(felhasznaloNev + ":" + jelszo);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

                HttpResponseMessage message = client.PutAsync(URL + "/" + oldalAzonositoja, content).Result;
                string description = string.Empty;
                string result = message.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception exception)
            {
                _naplo.Error(exception.StackTrace);
                return "";
            }
        }


        public async Task<string> KepFeltoltes(string felhasznaloNev, string jelszo, string terAzonosito, string URL, string oldalNeve, string kepFajlEleresiUtja, string fajlNev, int idHossza)
        {
            try
            {
                if (oldalNeve.Equals(""))
                {
                    oldalNeve = APPSETTINGS_OLDALNEVE;
                }

                ByteArrayContent kepByteTomb = new ByteArrayContent(File.ReadAllBytes(kepFajlEleresiUtja));

                string oldalAzonositoja = GetOldalIDNevAlapjan(felhasznaloNev, jelszo, terAzonosito, URL, oldalNeve, idHossza);
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), URL + "/" + oldalAzonositoja + "/child/attachment"))
                    {
                        request.Headers.TryAddWithoutValidation("X-Atlassian-Token", "nocheck");

                        var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(felhasznaloNev + ":" + jelszo));
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                        var multipartContent = new MultipartFormDataContent();
                        multipartContent.Add(kepByteTomb, "file", fajlNev);
                        multipartContent.Add(new StringContent("This is my File"), "comment");
                        request.Content = multipartContent;

                        var response = await httpClient.SendAsync(request);
                        return response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception exception)
            {
                _naplo.Error(exception.StackTrace);
                return "";
            }
        }

        public string GetOldalIDNevAlapjan(string felhasznaloNev, string jelszo, string terAzonosito, string URL, string oldalNeve, int idHossza)
        {
            try
            {
                string eredmeny = "";
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), URL + "?title=" + oldalNeve + "&spaceKey=" + terAzonosito + "&expand=history"))
                    {
                        var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(felhasznaloNev + ":" + jelszo));
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                        //var response = await httpClient.SendAsync(request).Result;
                        HttpResponseMessage message = httpClient.SendAsync(request).Result;
                        string description = string.Empty;
                        string result = message.Content.ReadAsStringAsync().Result;
                        description = result;

                        eredmeny = result.Replace("{\"results\":[{\"id\":\"", "").Substring(0, idHossza);
                    }
                }
                return eredmeny;
            }
            catch (Exception exception)
            {
                _naplo.Error(exception.StackTrace);
                return "";
            }

        }
    }
}
