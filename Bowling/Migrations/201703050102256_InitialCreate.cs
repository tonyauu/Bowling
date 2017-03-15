namespace Bowling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lane",
                c => new
                    {
                        LaneID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumbOfPeople = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LaneID);
            
            CreateTable(
                "dbo.Reserve",
                c => new
                    {
                        ReserveID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        ReserveStatus = c.Int(),
                        Lane_LaneID = c.Int(),
                    })
                .PrimaryKey(t => t.ReserveID)
                .ForeignKey("dbo.Lane", t => t.Lane_LaneID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.Lane_LaneID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserve", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Reserve", "Lane_LaneID", "dbo.Lane");
            DropIndex("dbo.Reserve", new[] { "Lane_LaneID" });
            DropIndex("dbo.Reserve", new[] { "PersonID" });
            DropTable("dbo.Person");
            DropTable("dbo.Reserve");
            DropTable("dbo.Lane");
        }
    }
}
