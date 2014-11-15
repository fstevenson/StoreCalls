namespace StoreCalls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Call",
                c => new
                    {
                        CallId = c.Long(nullable: false, identity: true),
                        CategoryId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        Program = c.String(),
                        Comments = c.String(),
                        Positive = c.Boolean(nullable: false),
                        Person_PersonId = c.Long(),
                    })
                .PrimaryKey(t => t.CallId)
                .ForeignKey("dbo.Person", t => t.Person_PersonId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.Person_PersonId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Long(nullable: false, identity: true),
                        PhoneNumber = c.Int(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Call", new[] { "CategoryId" });
            DropIndex("dbo.Call", new[] { "EmployeeId" });
            DropIndex("dbo.Call", new[] { "Person_PersonId" });
            DropForeignKey("dbo.Call", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Call", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Call", "Person_PersonId", "dbo.Person");
            DropTable("dbo.Category");
            DropTable("dbo.Employee");
            DropTable("dbo.Person");
            DropTable("dbo.Call");
        }
    }
}
