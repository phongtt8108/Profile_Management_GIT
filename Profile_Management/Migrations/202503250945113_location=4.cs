namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User_TBL", "NationalID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_TBL", "NationalID", c => c.Int(nullable: false));
        }
    }
}
