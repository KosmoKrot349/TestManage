namespace sr6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        action = c.String(),
                        Scenarioid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Scenarios", t => t.Scenarioid, cascadeDelete: true)
                .Index(t => t.Scenarioid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actions", "Scenarioid", "dbo.Scenarios");
            DropIndex("dbo.Actions", new[] { "Scenarioid" });
            DropTable("dbo.Actions");
        }
    }
}
