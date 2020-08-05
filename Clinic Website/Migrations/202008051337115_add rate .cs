namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Rate");
        }
    }
}
