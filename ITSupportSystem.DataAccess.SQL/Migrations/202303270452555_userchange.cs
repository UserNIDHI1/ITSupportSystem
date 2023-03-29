namespace ITSupportSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserName", c => c.String());
            AddColumn("dbo.Users", "Gender", c => c.String());
            AddColumn("dbo.Users", "MobileNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "MobileNo");
            DropColumn("dbo.Users", "Gender");
            DropColumn("dbo.Users", "UserName");
        }
    }
}
