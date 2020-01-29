using System;

namespace LoginService.Contracts
{
    public class CAuthorizationData
    {
        public CAuthorizationData(String login, String password)
        {
            Login = login;
            Password = password;
        }

        public String Login { get; set; }
        public String Password { get; set; }
    }
}