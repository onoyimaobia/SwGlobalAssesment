namespace SwGlobalData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RechargeTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recharges",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        OperatorId = c.Long(nullable: false),
                        RechargeTypeId = c.Long(nullable: false),
                        MobileNumber = c.String(),
                        Description = c.String(),
                        PlanId = c.Long(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recharges");
            DropTable("dbo.RechargeTypes");
        }
    }
}
