namespace Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablepolozky : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "LastRevision", c => c.DateTime());
            AlterColumn("dbo.Cars", "LastZTE", c => c.DateTime());
            AlterColumn("dbo.Cars", "LastZTL", c => c.DateTime());
            AlterColumn("dbo.Users", "TokenValidTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "TokenValidTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cars", "LastZTL", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cars", "LastZTE", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cars", "LastRevision", c => c.DateTime(nullable: false));
        }
    }
}
