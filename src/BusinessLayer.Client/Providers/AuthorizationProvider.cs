using System;
using System.Net;
using System.Net.Http;
using LoginService.Contracts;
using Newtonsoft.Json;

namespace BusinessLayer.Client.Providers
{
    public class CAuthorizationProvider
    {
        public Boolean TryAuthorize(String login, String password, out CAuthToken token)
        {
            var authData = new CAuthorizationData(login, password);
            HttpResponseMessage response = SHelper.PostRaw("api/Authorization/Login", authData);
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                String result = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<CAuthToken>(result);
                return true;
            }

            token = null;
            return false;
        }

        public Boolean Register(String login, String password)
        {
            var authData = new CAuthorizationData(login, password);

            HttpResponseMessage response = SHelper.PostRaw("api/Authorization/Registration", authData);
            return response.IsSuccessStatusCode;
        }
    }
}