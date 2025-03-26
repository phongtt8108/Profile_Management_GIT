namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class index_1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.User_TBL", "UserID", name: "IX_UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User_TBL", "IX_UserID");
        }
    }
}
