namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addavaialbletimelist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableTimesLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayListId = c.Int(nullable: false),
                        Slot_start = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayLists", t => t.DayListId, cascadeDelete: true)
                .Index(t => t.DayListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AvailableTimesLists", "DayListId", "dbo.DayLists");
            DropIndex("dbo.AvailableTimesLists", new[] { "DayListId" });
            DropTable("dbo.AvailableTimesLists");
        }
    }
}
