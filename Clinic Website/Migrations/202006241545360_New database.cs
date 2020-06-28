namespace Clinic_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newdatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplyForClinics", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.ApplyForClinics", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ApplyForClinics", new[] { "ClinicId" });
            DropIndex("dbo.ApplyForClinics", new[] { "UserId" });
            DropIndex("dbo.Clinics", new[] { "UserId" });
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prescription = c.String(),
                        TimeStart = c.String(),
                        Date_Created = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        ClinicId = c.Int(nullable: false),
                        PatientStateId = c.Int(nullable: false),
                        AppointmentStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppointmentStatus", t => t.AppointmentStatusId)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .ForeignKey("dbo.PatientStates", t => t.PatientStateId, cascadeDelete: true)
                .Index(t => t.ClinicId)
                .Index(t => t.PatientStateId)
                .Index(t => t.AppointmentStatusId);
            
            CreateTable(
                "dbo.AppointmentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppointmentId = c.Int(),
                        StatusId = c.Int(),
                        Date_Created = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId)
                .ForeignKey("dbo.AppointmentStatus", t => t.StatusId)
                .Index(t => t.AppointmentId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.DayLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClinicId = c.Int(nullable: false),
                        DayName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.TimeSlotLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClinicId = c.Int(nullable: false),
                        Slot_start = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.PatientStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        PatientId = c.String(nullable: false, maxLength: 128),
                        Date_Created = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        InProgress = c.Boolean(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientId)
                .Index(t => t.PatientId);
            
            AddColumn("dbo.Clinics", "AppointmentLength", c => c.Int(nullable: false));
            AddColumn("dbo.Clinics", "Address", c => c.String());
            AddColumn("dbo.Clinics", "StartTime", c => c.String());
            AddColumn("dbo.Clinics", "EndTime", c => c.String());
            AddColumn("dbo.Clinics", "MobileNumber", c => c.String());
            AddColumn("dbo.Clinics", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Clinics", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Clinics", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Clinics", "userId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Clinics", "userId");
            DropTable("dbo.ApplyForClinics");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplyForClinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        ClinicId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Appointments", "PatientStateId", "dbo.PatientStates");
            DropForeignKey("dbo.Appointments", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.PatientStates", "PatientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TimeSlotLists", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.DayLists", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.StatusHistories", "StatusId", "dbo.AppointmentStatus");
            DropForeignKey("dbo.StatusHistories", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "AppointmentStatusId", "dbo.AppointmentStatus");
            DropIndex("dbo.PatientStates", new[] { "PatientId" });
            DropIndex("dbo.TimeSlotLists", new[] { "ClinicId" });
            DropIndex("dbo.DayLists", new[] { "ClinicId" });
            DropIndex("dbo.Clinics", new[] { "userId" });
            DropIndex("dbo.StatusHistories", new[] { "StatusId" });
            DropIndex("dbo.StatusHistories", new[] { "AppointmentId" });
            DropIndex("dbo.Appointments", new[] { "AppointmentStatusId" });
            DropIndex("dbo.Appointments", new[] { "PatientStateId" });
            DropIndex("dbo.Appointments", new[] { "ClinicId" });
            AlterColumn("dbo.Clinics", "userId", c => c.String(maxLength: 128));
            DropColumn("dbo.Clinics", "Longitude");
            DropColumn("dbo.Clinics", "Latitude");
            DropColumn("dbo.Clinics", "Price");
            DropColumn("dbo.Clinics", "MobileNumber");
            DropColumn("dbo.Clinics", "EndTime");
            DropColumn("dbo.Clinics", "StartTime");
            DropColumn("dbo.Clinics", "Address");
            DropColumn("dbo.Clinics", "AppointmentLength");
            DropTable("dbo.PatientStates");
            DropTable("dbo.TimeSlotLists");
            DropTable("dbo.DayLists");
            DropTable("dbo.StatusHistories");
            DropTable("dbo.AppointmentStatus");
            DropTable("dbo.Appointments");
            CreateIndex("dbo.Clinics", "UserId");
            CreateIndex("dbo.ApplyForClinics", "UserId");
            CreateIndex("dbo.ApplyForClinics", "ClinicId");
            AddForeignKey("dbo.ApplyForClinics", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ApplyForClinics", "ClinicId", "dbo.Clinics", "Id", cascadeDelete: true);
        }
    }
}
