namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointmentdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "DayofApp", c => c.Int(nullable: false));
            AlterColumn("dbo.Appointments", "TimeStart", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "TimeStart", c => c.String());
            DropColumn("dbo.Appointments", "DayofApp");
        }
    }
}
