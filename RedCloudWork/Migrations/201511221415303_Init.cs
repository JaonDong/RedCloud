namespace RedCloudWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChargeSource = c.String(),
                        ServiceRequestNo = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TradingTime = c.DateTime(nullable: false),
                        CompletionTime = c.DateTime(),
                        ProductExpense = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompleteState = c.Boolean(nullable: false),
                        Merchant_Id = c.Int(),
                        Product_Id = c.Int(),
                        Salesman_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchants", t => t.Merchant_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Salesmen", t => t.Salesman_Id)
                .Index(t => t.Merchant_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Salesman_Id);
            
            CreateTable(
                "dbo.Merchants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MerchantNo = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salesmen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "Salesman_Id", "dbo.Salesmen");
            DropForeignKey("dbo.Bills", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Bills", "Merchant_Id", "dbo.Merchants");
            DropIndex("dbo.Bills", new[] { "Salesman_Id" });
            DropIndex("dbo.Bills", new[] { "Product_Id" });
            DropIndex("dbo.Bills", new[] { "Merchant_Id" });
            DropTable("dbo.Salesmen");
            DropTable("dbo.Products");
            DropTable("dbo.Merchants");
            DropTable("dbo.Bills");
        }
    }
}
