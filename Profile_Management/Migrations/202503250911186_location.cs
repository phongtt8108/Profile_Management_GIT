namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActionLog", "ActionLogDevice_Name", c => c.String());
            AddColumn("dbo.ActionLog", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActionLog", "Location");
            DropColumn("dbo.ActionLog", "ActionLogDevice_Name");
        }
    }
}
