namespace ITSupportSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeticketcomment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketComments", "TicketId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketComments", "TicketId", c => c.String());
        }
    }
}
