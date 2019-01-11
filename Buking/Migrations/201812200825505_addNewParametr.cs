namespace Buking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewParametr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GuestHouses", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GuestHouses", "Price");
        }
    }
}
