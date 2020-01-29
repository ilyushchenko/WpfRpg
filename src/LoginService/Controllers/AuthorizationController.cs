using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginService.Bll.Providers;
using LoginService.Contracts;
using Newtonsoft.Json;

namespace LoginService.Controllers
{
    [RoutePrefix("api/Authorization")]
    public class AuthorizationController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult LoginUser([FromBody] CAuthorizationData model)
        {
            if (String.IsNullOrWhiteSpace(model.Login))
            {
                return BadRequest("Login is required field");
            }

            if (String.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Password is required field");
            }

            var authProvider = new CAuthProvider();
            Boolean authorizationResult = authProvider.TryAuthorize(model.Login, model.Password, out CAuthToken token);
            if (!authorizationResult)
            {
                return BadRequest("Incorrect login or password");
            }

            return Ok(token);
        }

        [Route("Registration")]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] CAuthorizationData model)
        {
            if (String.IsNullOrWhiteSpace(model.Login))
            {
                return BadRequest("Login is required field");
            }

            if (String.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Password is required field");
            }

            var authProvider = new CAuthProvider();
            Boolean userExist = authProvider.CheckLoginExist(model.Login);
            if (userExist)
            {
                return BadRequest($"Login {model.Login} already exist");
            }

            authProvider.RegisterUser(model.Login, model.Password);
            return Ok();
        }
    }
}