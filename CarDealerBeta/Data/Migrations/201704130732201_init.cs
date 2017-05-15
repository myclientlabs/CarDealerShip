namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Color",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vin = c.String(nullable: false, maxLength: 17, unicode: false),
                        ModelId = c.Int(nullable: false),
                        Year = c.Short(nullable: false),
                        New = c.Binary(nullable: false, maxLength: 1, fixedLength: true),
                        StyleId = c.Int(nullable: false),
                        TransmissionId = c.Int(nullable: false),
                        InteriorId = c.Int(nullable: false),
                        ExteriorId = c.Int(nullable: false),
                        Mileage = c.Int(nullable: false),
                        MSRP = c.Decimal(nullable: false, storeType: "money"),
                        Description = c.String(nullable: false, maxLength: 500),
                        ImageFile = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Model", t => t.ModelId)
                .ForeignKey("dbo.Style", t => t.StyleId)
                .ForeignKey("dbo.Transmission", t => t.TransmissionId)
                .ForeignKey("dbo.Color", t => t.ExteriorId)
                .ForeignKey("dbo.Color", t => t.InteriorId)
                .Index(t => t.ModelId)
                .Index(t => t.StyleId)
                .Index(t => t.TransmissionId)
                .Index(t => t.InteriorId)
                .Index(t => t.ExteriorId);
            
            CreateTable(
                "dbo.Inquiry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 2000),
                        VehicleID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactId)
                .ForeignKey("dbo.Vehicle", t => t.VehicleID)
                .Index(t => t.ContactId)
                .Index(t => t.VehicleID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 70),
                        LastName = c.String(nullable: false, maxLength: 70),
                        Phone = c.String(nullable: false, maxLength: 15, unicode: false),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Model",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MakeId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Make", t => t.MakeId)
                .Index(t => t.MakeId);
            
            CreateTable(
                "dbo.Make",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SalespersonId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PurchasePrice = c.Decimal(nullable: false, storeType: "money"),
                        PurchaseTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.PurchaseType", t => t.PurchaseTypeId)
                .ForeignKey("dbo.Salesperson", t => t.SalespersonId)
                .ForeignKey("dbo.Vehicle", t => t.VehicleId)
                .Index(t => t.CustomerId)
                .Index(t => t.SalespersonId)
                .Index(t => t.VehicleId)
                .Index(t => t.PurchaseTypeId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 70),
                        LastName = c.String(nullable: false, maxLength: 70),
                        Phone = c.String(nullable: false, maxLength: 15, unicode: false),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        Street1 = c.String(nullable: false, maxLength: 100, unicode: false),
                        Street2 = c.String(nullable: false, maxLength: 100, unicode: false),
                        City = c.String(nullable: false, maxLength: 30, unicode: false),
                        StateId = c.Int(),
                        ZipCode = c.String(nullable: false, maxLength: 5, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30, unicode: false),
                        Code = c.String(nullable: false, maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salesperson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 70),
                        LastName = c.String(nullable: false, maxLength: 70),
                        Email = c.String(nullable: false, maxLength: 120, unicode: false),
                        Role = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        FloorPrice = c.Decimal(nullable: false, storeType: "money"),
                        Available = c.Binary(nullable: false, maxLength: 1, fixedLength: true),
                        Feature = c.Binary(nullable: false, maxLength: 1, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicle", t => t.VehicleId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Style",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transmission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Special",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicle", "InteriorId", "dbo.Color");
            DropForeignKey("dbo.Vehicle", "ExteriorId", "dbo.Color");
            DropForeignKey("dbo.Vehicle", "TransmissionId", "dbo.Transmission");
            DropForeignKey("dbo.Vehicle", "StyleId", "dbo.Style");
            DropForeignKey("dbo.Stock", "VehicleId", "dbo.Vehicle");
            DropForeignKey("dbo.Sales", "VehicleId", "dbo.Vehicle");
            DropForeignKey("dbo.Salesperson", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sales", "SalespersonId", "dbo.Salesperson");
            DropForeignKey("dbo.Sales", "PurchaseTypeId", "dbo.PurchaseType");
            DropForeignKey("dbo.Customer", "StateId", "dbo.State");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Vehicle", "ModelId", "dbo.Model");
            DropForeignKey("dbo.Model", "MakeId", "dbo.Make");
            DropForeignKey("dbo.Inquiry", "VehicleID", "dbo.Vehicle");
            DropForeignKey("dbo.Inquiry", "ContactId", "dbo.Contact");
            DropIndex("dbo.Stock", new[] { "VehicleId" });
            DropIndex("dbo.Salesperson", new[] { "UserId" });
            DropIndex("dbo.Customer", new[] { "StateId" });
            DropIndex("dbo.Sales", new[] { "PurchaseTypeId" });
            DropIndex("dbo.Sales", new[] { "VehicleId" });
            DropIndex("dbo.Sales", new[] { "SalespersonId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Model", new[] { "MakeId" });
            DropIndex("dbo.Inquiry", new[] { "VehicleID" });
            DropIndex("dbo.Inquiry", new[] { "ContactId" });
            DropIndex("dbo.Vehicle", new[] { "ExteriorId" });
            DropIndex("dbo.Vehicle", new[] { "InteriorId" });
            DropIndex("dbo.Vehicle", new[] { "TransmissionId" });
            DropIndex("dbo.Vehicle", new[] { "StyleId" });
            DropIndex("dbo.Vehicle", new[] { "ModelId" });
            DropTable("dbo.Special");
            DropTable("dbo.Transmission");
            DropTable("dbo.Style");
            DropTable("dbo.Stock");
            DropTable("dbo.Users");
            DropTable("dbo.Salesperson");
            DropTable("dbo.PurchaseType");
            DropTable("dbo.State");
            DropTable("dbo.Customer");
            DropTable("dbo.Sales");
            DropTable("dbo.Make");
            DropTable("dbo.Model");
            DropTable("dbo.Contact");
            DropTable("dbo.Inquiry");
            DropTable("dbo.Vehicle");
            DropTable("dbo.Color");
        }
    }
}
