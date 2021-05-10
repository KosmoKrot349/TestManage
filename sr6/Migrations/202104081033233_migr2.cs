namespace sr6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        result = c.String(),
                        dateOfExecution = c.DateTime(nullable: false),
                        typeOfError = c.String(),
                        dateOfBugFix = c.DateTime(nullable: false),
                        nameOfFixer = c.String(),
                        Projectid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Projects", t => t.Projectid, cascadeDelete: true)
                .Index(t => t.Projectid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scenarios", "Projectid", "dbo.Projects");
            DropIndex("dbo.Scenarios", new[] { "Projectid" });
            DropTable("dbo.Scenarios");
        }
    }
}
