namespace SwGlobalData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "IsAdmin");
            DropColumn("dbo.AspNetUsers", "DeviceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DeviceId", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}
