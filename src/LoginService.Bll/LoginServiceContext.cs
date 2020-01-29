using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginService.Bll.Models;

namespace LoginService.Bll
{
    internal class CLoginServiceContext : DbContext
    {
        public CLoginServiceContext() : base("LoginServiceDatabaseConnection")
        {
            
        }

        public DbSet<CUser> Users { get; set; }
    }
}
