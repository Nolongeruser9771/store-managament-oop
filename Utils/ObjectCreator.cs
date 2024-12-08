using Repository;
using Service;

namespace store_managament_oop.Utils
{
	public class ObjectCreator
	{
		public static ICategoryService CreateCategoryServiceObject()
		{
			ICategoryRepository categoryRepository = new CategoryRepository();
			return new CategoryService(categoryRepository);
		}

		public static IProductService CreateProductServiceObject()
		{
			IProductRepository productRepository = new ProductRepository();
			return new ProductService(productRepository);
		}

		public static IPurchaseInvoiceService CreatePurchaseInvoiceServiceObject()
		{
			IPurchaseInvoiceRepository purchaseInvoiceRepository = new PurchaseInvoiceRepository();
			IPurchaseInvoiceItemRepository purchaseInvoiceItemRepository = new PurchaseInvoiceItemRepository();
			return new PurchaseInvoiceService(purchaseInvoiceRepository, purchaseInvoiceItemRepository);
		}
		public static ISalesInvoiceService CreateSalesInvoiceServiceObject()
		{
			ISalesInvoiceRepository salesInvoiceRepository = new SalesInvoiceRepository();
			ISalesInvoiceItemRepository salesInvoiceItemRepository = new SalesInvoiceItemRepository();
			return new SalesInvoiceService(salesInvoiceRepository, salesInvoiceItemRepository);
		}
	}
}
