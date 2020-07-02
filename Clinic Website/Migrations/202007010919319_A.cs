namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "DayofApp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "DayofApp", c => c.Int(nullable: false));
        }
    }
}
