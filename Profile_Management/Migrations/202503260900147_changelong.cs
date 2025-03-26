namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changelong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActionLog", "ActionLogUser", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActionLog", "ActionLogUser", c => c.Int(nullable: false));
        }
    }
}
