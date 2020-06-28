namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CLIC : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinics", "ClinicImage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinics", "ClinicImage", c => c.String(nullable: false));
        }
    }
}
