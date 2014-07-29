namespace guestNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Identityabstractclass : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Responses", new[] { "AdvertisementId" });
            RenameColumn(table: "dbo.Responses", name: "AdvertisementId", newName: "Id");
            DropPrimaryKey("dbo.Responses");
            AlterColumn("dbo.Responses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Responses", "Id");
            CreateIndex("dbo.Responses", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Responses", new[] { "Id" });
            DropPrimaryKey("dbo.Responses");
            AlterColumn("dbo.Responses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Responses", "AdvertisementId");
            RenameColumn(table: "dbo.Responses", name: "Id", newName: "AdvertisementId");
            CreateIndex("dbo.Responses", "AdvertisementId");
        }
    }
}
