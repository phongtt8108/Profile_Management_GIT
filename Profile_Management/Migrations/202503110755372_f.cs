namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.User_TBL", "Nation_ID");
            AddForeignKey("dbo.User_TBL", "Nation_ID", "dbo.Nationality", "Nation_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_TBL", "Nation_ID", "dbo.Nationality");
            DropIndex("dbo.User_TBL", new[] { "Nation_ID" });
        }
    }
}
