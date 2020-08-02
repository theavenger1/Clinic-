namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slotidinap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Slot", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Slot");
        }
    }
}
