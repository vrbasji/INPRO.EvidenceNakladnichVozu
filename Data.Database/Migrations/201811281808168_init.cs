namespace Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "RevisionPeriod", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "RevisionPeriod", c => c.Time(nullable: false, precision: 7));
        }
    }
}
