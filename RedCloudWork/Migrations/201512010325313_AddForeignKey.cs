namespace RedCloudWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "Merchant_Id", "dbo.Merchants");
            DropForeignKey("dbo.Bills", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Bills", "Salesman_Id", "dbo.Salesmen");
            DropIndex("dbo.Bills", new[] { "Merchant_Id" });
            DropIndex("dbo.Bills", new[] { "Product_Id" });
            DropIndex("dbo.Bills", new[] { "Salesman_Id" });
            RenameColumn(table: "dbo.Bills", name: "Merchant_Id", newName: "MerchantId");
            RenameColumn(table: "dbo.Bills", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.Bills", name: "Salesman_Id", newName: "SalesmanId");
            AlterColumn("dbo.Bills", "MerchantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bills", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bills", "SalesmanId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bills", "SalesmanId");
            CreateIndex("dbo.Bills", "ProductId");
            CreateIndex("dbo.Bills", "MerchantId");
            AddForeignKey("dbo.Bills", "MerchantId", "dbo.Merchants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "SalesmanId", "dbo.Salesmen", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "SalesmanId", "dbo.Salesmen");
            DropForeignKey("dbo.Bills", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Bills", "MerchantId", "dbo.Merchants");
            DropIndex("dbo.Bills", new[] { "MerchantId" });
            DropIndex("dbo.Bills", new[] { "ProductId" });
            DropIndex("dbo.Bills", new[] { "SalesmanId" });
            AlterColumn("dbo.Bills", "SalesmanId", c => c.Int());
            AlterColumn("dbo.Bills", "ProductId", c => c.Int());
            AlterColumn("dbo.Bills", "MerchantId", c => c.Int());
            RenameColumn(table: "dbo.Bills", name: "SalesmanId", newName: "Salesman_Id");
            RenameColumn(table: "dbo.Bills", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.Bills", name: "MerchantId", newName: "Merchant_Id");
            CreateIndex("dbo.Bills", "Salesman_Id");
            CreateIndex("dbo.Bills", "Product_Id");
            CreateIndex("dbo.Bills", "Merchant_Id");
            AddForeignKey("dbo.Bills", "Salesman_Id", "dbo.Salesmen", "Id");
            AddForeignKey("dbo.Bills", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.Bills", "Merchant_Id", "dbo.Merchants", "Id");
        }
    }
}
