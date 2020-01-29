using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginService.Bll.Models
{
    public class CUser
    {
        public CUser()
        {
        }

        public CUser(String login, String password)
        {
            Login = login;
            Password = password;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Login { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
