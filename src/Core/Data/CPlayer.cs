using System;
using System.Runtime.Serialization;

namespace Core.Data
{
    [DataContract]
    public class CPlayer
    {
        public CPlayer(String session, String login, Guid id)
        {
            Id = id;
            Login = login;
            Session = session;
        }

        [DataMember]
        public String Session { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Login { get; set; }
    }
}