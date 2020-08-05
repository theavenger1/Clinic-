namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratedouble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rate", c => c.Double(nullable: false));
            DropColumn("dbo.AspNetUsers", "_Rate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "_Rate", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Rate");
        }
    }
}
