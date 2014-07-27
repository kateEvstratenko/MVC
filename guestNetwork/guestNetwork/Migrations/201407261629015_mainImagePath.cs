namespace guestNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mainImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "mainImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "mainImagePath");
        }
    }
}
