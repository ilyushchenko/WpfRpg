using System;

namespace LoginService.Contracts
{
    public class CAuthToken
    {
        public Guid Id { get; set; }
        public String Login { get; set; }
    }
}
