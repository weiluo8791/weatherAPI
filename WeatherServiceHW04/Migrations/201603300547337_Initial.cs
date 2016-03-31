namespace WeatherServiceHW04.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Humidities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RecorDateTime = c.DateTime(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pressures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Millibar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RecorDateTime = c.DateTime(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Temperatures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Degree = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RecorDateTime = c.DateTime(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Temperatures");
            DropTable("dbo.Pressures");
            DropTable("dbo.Humidities");
        }
    }
}
