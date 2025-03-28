﻿namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intchange_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_TBL", "Nation_ID", "dbo.Nationality");
            DropIndex("dbo.User_TBL", new[] { "Nation_ID" });
            AlterColumn("dbo.User_TBL", "Nation_ID", c => c.Int());
            CreateIndex("dbo.User_TBL", "Nation_ID");
            AddForeignKey("dbo.User_TBL", "Nation_ID", "dbo.Nationality", "Nation_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_TBL", "Nation_ID", "dbo.Nationality");
            DropIndex("dbo.User_TBL", new[] { "Nation_ID" });
            AlterColumn("dbo.User_TBL", "Nation_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.User_TBL", "Nation_ID");
            AddForeignKey("dbo.User_TBL", "Nation_ID", "dbo.Nationality", "Nation_ID", cascadeDelete: true);
        }
    }
}
