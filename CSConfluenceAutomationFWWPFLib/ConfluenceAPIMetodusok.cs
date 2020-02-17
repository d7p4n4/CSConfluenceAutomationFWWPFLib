using Dapplo.Confluence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

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

        public string TransformXMLToHTML(string inputXml, string xsltString)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

        public string DeletePage(string jelszo, string felhasznaloNev, string URL, string oldalNeve, string terAzonosito, int idHossza)
        {

            string oldalAzonosito = GetOldalIDNevAlapjan(felhasznaloNev, jelszo, terAzonosito, URL, oldalNeve, idHossza);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), URL + "/" + oldalAzonosito + "?status=current"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(felhasznaloNev + ":" + jelszo));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    //var response = await httpClient.SendAsync(request).Result;
                    HttpResponseMessage message = httpClient.SendAsync(request).Result;
                    string description = string.Empty;
                    string result = message.Content.ReadAsStringAsync().Result;
                    description = result;
                    return result;
                }
            }
        }

        public bool IsPageExists(string URL, string cim, string terAzonosito, string felhasznaloNev, string jelszo, int idHossza)
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

        public AddNewPageResult AddConfluencePage(string cim, string terAzonosito, string szuloOsztalyNeve, string html, string URL, string felhasznaloNev, string jelszo, int idHossza)
        {
            AddNewPageResult addNewPageResult = new AddNewPageResult();

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

            if (message.IsSuccessStatusCode)
            {
                NewPageSuccessResponse JSONObjSuccess = new NewPageSuccessResponse();
                JSONObjSuccess = JsonConvert.DeserializeObject<NewPageSuccessResponse>(result);

                addNewPageResult.SuccessResponse = JSONObjSuccess;
            }
            else 
            { 

                NewPageFailedResponse JSONObjFailed = new NewPageFailedResponse();
                JSONObjFailed = JsonConvert.DeserializeObject<NewPageFailedResponse>(result);

                addNewPageResult.FailedResponse = JSONObjFailed;

            }

            return addNewPageResult;
           
        }

        public string UpdateConfluencePage(string cim, string terAzonosito, string html, string URL, string felhasznaloNev, string jelszo, string verzioSzam, int idHossza)
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
               
        public async Task<string> KepFeltoltes(string felhasznaloNev, string jelszo, string terAzonosito, string URL, string oldalNeve, byte[] kepFajlBajtjai, string fajlNev, int idHossza)
        {
            if (oldalNeve.Equals(""))
            {
                oldalNeve = APPSETTINGS_OLDALNEVE;
            }

            ByteArrayContent kepByteTomb = new ByteArrayContent(kepFajlBajtjai);

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

        public async Task<string> Kepfeltoltes(string felhasznaloNev, string jelszo, string terAzonosito, string URL, string oldalNeve, string kepFajlBase64, string fajlNev, int idHossza)
        {

            return await KepFeltoltes(
                felhasznaloNev
                , jelszo
                , terAzonosito
                , URL
                , oldalNeve
                , Convert.FromBase64String(kepFajlBase64)
                , fajlNev
                , idHossza
                );
        }
        
        public string GetOldalIDNevAlapjan(string felhasznaloNev, string jelszo, string terAzonosito, string URL, string oldalNeve, int idHossza)
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

    }
}
