namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User_TBL", "Nationality_1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_TBL", "Nationality_1", c => c.String());
        }
    }
}
