namespace SwGlobalData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OperatorId = c.Long(nullable: false),
                        OfferImageUrl = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Operators",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OperatorName = c.String(),
                        OperatorUrl = c.String(),
                        OperatorImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OperatorId = c.Long(nullable: false),
                        ValueAddedServiceId = c.Long(nullable: false),
                        Amount = c.Int(nullable: false),
                        SiteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ValueAddedServices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ValueAddedServices");
            DropTable("dbo.Plans");
            DropTable("dbo.Operators");
            DropTable("dbo.Offers");
        }
    }
}
