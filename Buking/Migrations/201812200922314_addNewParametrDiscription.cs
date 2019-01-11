namespace Buking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewParametrDiscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GuestHouses", "Discription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GuestHouses", "Discription");
        }
    }
}
