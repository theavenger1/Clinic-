namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clinicdeleteclasuse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.Appointments", new[] { "ClinicId" });
            AlterColumn("dbo.Appointments", "ClinicId", c => c.Int());
            CreateIndex("dbo.Appointments", "ClinicId");
            AddForeignKey("dbo.Appointments", "ClinicId", "dbo.Clinics", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.Appointments", new[] { "ClinicId" });
            AlterColumn("dbo.Appointments", "ClinicId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "ClinicId");
            AddForeignKey("dbo.Appointments", "ClinicId", "dbo.Clinics", "Id", cascadeDelete: false);
        }
    }
}
