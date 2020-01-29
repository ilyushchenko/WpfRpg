using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using LoginService.Contracts;
using Newtonsoft.Json;

namespace MainService.Controllers
{
    [RoutePrefix("api/Authorization")]
    public class AuthorizationController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult LoginUser([FromBody] CAuthorizationData model)
        {
            if (!ValidateAuthModel(model, out String errorMessage))
            {
                return BadRequest(errorMessage);
            }

            using (var client = new HttpClient())
            {
                String authServiceUrl = GetAuthServiceUrl();
                //var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8);
                var content = new ObjectContent<CAuthorizationData>(model,new JsonMediaTypeFormatter());
                HttpResponseMessage response =
                    client.PostAsync(authServiceUrl + "api/Authorization/Login", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var token = JsonConvert.DeserializeObject<CAuthToken>(result);
                    return Ok(token);
                }

                return BadRequest();
            }
        }

        [Route("Registration")]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] CAuthorizationData model)
        {
            if (!ValidateAuthModel(model, out String errorMessage))
            {
                return BadRequest(errorMessage);
            }

            using (var client = new HttpClient())
            {
                String authServiceUrl = GetAuthServiceUrl();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8);
                HttpResponseMessage response =
                    client.PostAsync(authServiceUrl + "api/Authorization/Registration", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }

                return BadRequest();
            }
        }

        private Boolean ValidateAuthModel(CAuthorizationData model, out String error)
        {
            error = String.Empty;
            if (String.IsNullOrWhiteSpace(model.Login))
            {
                error = "Login is required field";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.Password))
            {
                error = "Password is required field";
                return false;
            }

            return true;
        }

        private String GetAuthServiceUrl()
        {
            return ConfigurationManager.AppSettings["AuthServiceUrl"];
        }
    }
}