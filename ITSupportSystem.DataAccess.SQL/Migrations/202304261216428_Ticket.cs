namespace ITSupportSystem.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ticket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        AssignTo = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        PriorityId = c.Guid(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketAttachments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketId = c.String(),
                        FileName = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketId = c.String(),
                        Comment = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketStatusHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketId = c.Guid(nullable: false),
                        OldStatus = c.String(),
                        NewStatus = c.String(),
                        CreatedBy = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Guid(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TicketStatusHistories");
            DropTable("dbo.TicketComments");
            DropTable("dbo.TicketAttachments");
            DropTable("dbo.Tickets");
        }
    }
}
