namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialCreate_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User_TBL", "MaritalStatus", c => c.String(maxLength: 20));
            AlterColumn("dbo.User_TBL", "Company", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_TBL", "Company", c => c.String(maxLength: 50));
            AlterColumn("dbo.User_TBL", "MaritalStatus", c => c.String());
        }
    }
}
