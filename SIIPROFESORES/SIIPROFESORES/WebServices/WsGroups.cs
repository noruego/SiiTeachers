using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SIIPROFESORES.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SIIPROFESORES.WebServices
{
    class WsGroups
    {
        public async Task<List<Group>> getGroups()
        {
            List<Group> listSubjects = new List<Group>();
            try
            {
                HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.ip);
            var authData = string.Format("{0}:{1}", "root", "root");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            var resp = await httpClient.GetAsync("SIIWS_PATM/api/wslista/getlistaProf/" + Settings.token);

            var cadena = resp.Content.ReadAsStringAsync().Result;
            var objJson = JObject.Parse(cadena);

            var arrJson = objJson.SelectToken("lista").ToList();

           
                foreach (var institucion in arrJson)
                {
                    Group subject = new Group();
                    //kardex = JsonConvert.DeserializeObject<Kardex>(institucion.ToString());
                    //Object obj = institucion["student"].ToObject<Student>();
                    subject.matter = institucion["matter"].ToObject<Matter>();
                    subject.teacher = institucion["teacher"].ToObject<Teacher>();
                    subject.idGroup = Convert.ToInt32(institucion["idGroup"]);
                    listSubjects.Add(subject);
                }
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return listSubjects;
        }
    }
}
