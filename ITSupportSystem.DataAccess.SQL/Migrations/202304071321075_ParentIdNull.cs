namespace ITSupportSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentIdNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "ParentFormId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "ParentFormId", c => c.Guid(nullable: false));
        }
    }
}
