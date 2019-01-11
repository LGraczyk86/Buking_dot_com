namespace Buking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewParametr2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GuestHouses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.GuestHouses", "Discription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GuestHouses", "Discription", c => c.String(nullable: false));
            AlterColumn("dbo.GuestHouses", "Name", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
