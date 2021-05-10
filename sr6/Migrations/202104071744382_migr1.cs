namespace sr6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
        }
    }
}
