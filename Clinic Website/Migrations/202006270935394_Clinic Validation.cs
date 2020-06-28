namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinics", "ClinicImage", c => c.String(nullable: false));
            AlterColumn("dbo.Clinics", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Clinics", "StartTime", c => c.String(nullable: false));
            AlterColumn("dbo.Clinics", "EndTime", c => c.String(nullable: false));
            AlterColumn("dbo.Clinics", "MobileNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinics", "MobileNumber", c => c.String());
            AlterColumn("dbo.Clinics", "EndTime", c => c.String());
            AlterColumn("dbo.Clinics", "StartTime", c => c.String());
            AlterColumn("dbo.Clinics", "Address", c => c.String());
            AlterColumn("dbo.Clinics", "ClinicImage", c => c.String());
        }
    }
}
