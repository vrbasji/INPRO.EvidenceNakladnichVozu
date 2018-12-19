namespace Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prace_s_brzdami : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Series", "AirBreakWeight", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "HandBreakWeight", c => c.Int(nullable: false));
            DropColumn("dbo.AirBreaks", "AirBreakWeight");
            DropColumn("dbo.HandBreaks", "HandBreakWeight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HandBreaks", "HandBreakWeight", c => c.Int(nullable: false));
            AddColumn("dbo.AirBreaks", "AirBreakWeight", c => c.Int(nullable: false));
            DropColumn("dbo.Series", "HandBreakWeight");
            DropColumn("dbo.Series", "AirBreakWeight");
        }
    }
}
