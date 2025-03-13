namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createnation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nationality",
                c => new
                    {
                        Nation_ID = c.Int(nullable: false, identity: true),
                        Nation_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Nation_ID);
            
            AddColumn("dbo.User_TBL", "Nationality_1", c => c.String());
            DropColumn("dbo.User_TBL", "Nationality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_TBL", "Nationality", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.User_TBL", "Nationality_1");
            DropTable("dbo.Nationality");
        }
    }
}
