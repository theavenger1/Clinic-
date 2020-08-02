namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetimelist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeSlotLists", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.TimeSlotLists", new[] { "ClinicId" });
            DropTable("dbo.TimeSlotLists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeSlotLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClinicId = c.Int(nullable: false),
                        Slot_start = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TimeSlotLists", "ClinicId");
            AddForeignKey("dbo.TimeSlotLists", "ClinicId", "dbo.Clinics", "Id", cascadeDelete: true);
        }
    }
}
