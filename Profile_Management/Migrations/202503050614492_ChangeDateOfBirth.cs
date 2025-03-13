namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateOfBirth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User_TBL", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_TBL", "DateOfBirth", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
