namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActionLog_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActionLog", "ActionLogAccountLog", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActionLog", "ActionLogAccountLog", c => c.Int(nullable: false));
        }
    }
}
