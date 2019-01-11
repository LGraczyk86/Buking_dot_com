namespace Buking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewParametr1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GuestHouses", "Path", c => c.String());
            DropColumn("dbo.GuestHouses", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GuestHouses", "Image", c => c.Binary(nullable: false));
            DropColumn("dbo.GuestHouses", "Path");
        }
    }
}
