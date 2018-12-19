namespace Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authentication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Token", c => c.String());
            AddColumn("dbo.Users", "TokenValidTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TokenValidTo");
            DropColumn("dbo.Users", "Token");
        }
    }
}
