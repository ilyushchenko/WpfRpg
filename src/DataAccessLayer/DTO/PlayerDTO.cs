using System;

namespace DataAccessLayer.DTO
{
    public class CPlayerDto
    {
        private CPlayerDto(Guid id, String login)
        {
            Id = id;
            Login = login;
        }

        public Guid Id { get; set; }
        public String Login { get; set; }

        public static CPlayerDto Create(Guid id, String login)
        {
            return new CPlayerDto(id, login);
        }

        public static CPlayerDto Create(String login)
        {
            return new CPlayerDto(Guid.NewGuid(), login);
        }
    }
}