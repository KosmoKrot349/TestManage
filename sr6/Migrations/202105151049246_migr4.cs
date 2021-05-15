namespace sr6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actions", "action", c => c.String(nullable: false));
            AlterColumn("dbo.Scenarios", "title", c => c.String(nullable: false));
            AlterColumn("dbo.Scenarios", "result", c => c.String(nullable: false));
            AlterColumn("dbo.Scenarios", "nameOfFixer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scenarios", "nameOfFixer", c => c.String());
            AlterColumn("dbo.Scenarios", "result", c => c.String());
            AlterColumn("dbo.Scenarios", "title", c => c.String());
            AlterColumn("dbo.Actions", "action", c => c.String());
        }
    }
}
