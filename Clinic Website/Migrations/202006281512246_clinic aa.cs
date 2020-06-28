namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clinicaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinics", "StartTime", c => c.Int(nullable: false));
            AlterColumn("dbo.Clinics", "EndTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinics", "EndTime", c => c.String(nullable: false));
            AlterColumn("dbo.Clinics", "StartTime", c => c.String(nullable: false));
        }
    }
}
