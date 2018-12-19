namespace Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prace_s_brzdami_oprava : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "AirBreakWeight", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "HandBreakWeight", c => c.Int(nullable: false));
            DropColumn("dbo.Series", "AirBreakWeight");
            DropColumn("dbo.Series", "HandBreakWeight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "HandBreakWeight", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "AirBreakWeight", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "HandBreakWeight");
            DropColumn("dbo.Cars", "AirBreakWeight");
        }
    }
}
