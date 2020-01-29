using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LoginService.Bll.Models;
using LoginService.Contracts;

namespace LoginService.Bll.Providers
{
    public class CAuthProvider
    {
        public Boolean TryAuthorize(String login, String password, out CAuthToken token)
        {
            token = null;
            String normalizedLogin = login.ToUpper();
            String passwordHash = GetHash(password);

            using (var context = new CLoginServiceContext())
            {
                CUser user =
                    context.Users.SingleOrDefault(u => u.Login == normalizedLogin && u.Password == passwordHash);
                if (user != null)
                {
                    token = new CAuthToken()
                    {
                        Id = user.Id,
                        Login = user.Login
                    };
                    return true;
                }
            }

            return false;
        }

        public Boolean CheckLoginExist(String login)
        {
            String normalizedLogin = login.ToUpper();

            using (var context = new CLoginServiceContext())
            {
                CUser user =
                    context.Users.SingleOrDefault(u => u.Login == normalizedLogin);
                return user != null;
            }
        }

        public Boolean RegisterUser(String login, String password)
        {
            String normalizedLogin = login.ToUpper();
            String passwordHash = GetHash(password);

            using (var context = new CLoginServiceContext())
            {
                var user = new CUser(normalizedLogin, passwordHash);
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        private static String GetHash(String data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                Byte[] bytes = Encoding.UTF8.GetBytes(data);
                Byte[] hashedBytes = sha256Hash.ComputeHash(bytes);

                // Convert byte array to a string   
                var builder = new StringBuilder();
                for (var i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}