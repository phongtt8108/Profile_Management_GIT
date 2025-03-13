namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_TBL", "Nation_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_TBL", "Nation_ID");
        }
    }
}
