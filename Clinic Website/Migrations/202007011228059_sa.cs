namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppointmentStatus", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppointmentStatus", "Name", c => c.Int(nullable: false));
        }
    }
}
