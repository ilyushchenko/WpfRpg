namespace LoginService.Bll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CUsers", new[] { "Login" });
            DropTable("dbo.CUsers");
        }
    }
}
