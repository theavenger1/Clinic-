namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addratenullable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "_Rate", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Rate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Rate", c => c.Int(nullable: true));
            DropColumn("dbo.AspNetUsers", "_Rate");
        }
    }
}
