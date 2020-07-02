namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AvailableTimesLists", "Taken", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AvailableTimesLists", "Taken");
        }
    }
}
