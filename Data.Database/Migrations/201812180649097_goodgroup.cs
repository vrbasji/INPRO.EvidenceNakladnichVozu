namespace Data.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goodgroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoodGroups",
                c => new
                    {
                        GoodGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GoodGroupId);
            
            AddColumn("dbo.Cars", "GoodGroup_GoodGroupId", c => c.Int());
            CreateIndex("dbo.Cars", "GoodGroup_GoodGroupId");
            AddForeignKey("dbo.Cars", "GoodGroup_GoodGroupId", "dbo.GoodGroups", "GoodGroupId");
            DropColumn("dbo.Cars", "GoodGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "GoodGroup", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cars", "GoodGroup_GoodGroupId", "dbo.GoodGroups");
            DropIndex("dbo.Cars", new[] { "GoodGroup_GoodGroupId" });
            DropColumn("dbo.Cars", "GoodGroup_GoodGroupId");
            DropTable("dbo.GoodGroups");
        }
    }
}
