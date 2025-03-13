namespace Profile_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialCreae_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_TBL",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        DateOfBirth = c.String(nullable: false, maxLength: 30),
                        Gender = c.String(nullable: false, maxLength: 20),
                        NationalID = c.Int(nullable: false),
                        Nationality = c.String(nullable: false, maxLength: 50),
                        MaritalStatus = c.String(),
                        PhoneNumber = c.String(maxLength: 50),
                        Address = c.String(maxLength: 500),
                        Job = c.String(maxLength: 50),
                        Company = c.String(maxLength: 50),
                        Position = c.String(maxLength: 50),
                        ProfilePicture = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_TBL");
        }
    }
}
