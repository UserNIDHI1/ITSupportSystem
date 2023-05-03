namespace ITSupportSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketattachment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketAttachments", "TicketId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketAttachments", "TicketId", c => c.String());
        }
    }
}
