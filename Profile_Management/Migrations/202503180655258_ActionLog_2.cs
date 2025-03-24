namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActionLog_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionLog",
                c => new
                    {
                        ActionLogID = c.Int(nullable: false, identity: true),
                        ActionLogType = c.String(maxLength: 50),
                        ActionLogDescription = c.String(),
                        ActionLogDate = c.DateTime(nullable: false),
                        ActionLogUser = c.Int(nullable: false),
                        ActionLogAccountLog = c.Int(nullable: false),
                        ActionLogIP = c.String(),
                        ActionLogDevice = c.String(),
                    })
                .PrimaryKey(t => t.ActionLogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionLog");
        }
    }
}
