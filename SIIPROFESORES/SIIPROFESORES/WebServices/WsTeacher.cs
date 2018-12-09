using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SIIPROFESORES.Models;
using System.Net.Http.Headers;

namespace SIIPROFESORES.WebServices
{
    class WsTeacher
    {
        public async Task<Teacher> getTeacher()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.ip);
            var authData = string.Format("{0}:{1}", "root", "root");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            var resp = await httpClient.GetAsync("/SIIWS_PATM/api/wsteacher/getTeacher/" + Settings.idTeacher + "/" +Settings.token);

            var json = resp.Content.ReadAsStringAsync().Result;
            Teacher teacher =new Teacher();
            if (json != null)
            {
                teacher = JsonConvert.DeserializeObject<Teacher>(json);

                Settings.user_name = teacher.name + " " + teacher.father_lastname + " " + teacher.mother_lastname;
                Settings.user_email = teacher.email;
                Settings.user_image = teacher.image;
                Settings.user_num_co = teacher.nocontrol;
            }
            return teacher;
        }
    }
}
