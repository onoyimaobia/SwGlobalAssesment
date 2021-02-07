namespace SwGlobalData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complaints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplaintType = c.Int(nullable: false),
                        UserId = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Complaints");
        }
    }
}
