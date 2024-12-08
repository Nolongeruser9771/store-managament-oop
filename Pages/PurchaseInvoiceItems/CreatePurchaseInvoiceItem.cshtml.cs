using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.PurchaseInvoiceItems
{
    public class CreatePurchaseInvoiceItemModel : PageModel
    {
		public List<Product> productList { get; set; }
		private IProductService _productService;
		private IPurchaseInvoiceService _purchaseInvoiceService;

		[BindProperty(SupportsGet = true)]
		public int purchaseInvoiceId { get; set; }
		[BindProperty]
		public int productId { get; set; }
		[BindProperty]
		public int quantity { get; set; }
		[BindProperty]
		public int price { get; set; }
		public PurchaseInvoice purchaseInvoice { get; set; }
		public string message = string.Empty;
		public CreatePurchaseInvoiceItemModel() : base()
		{
			_purchaseInvoiceService = ObjectCreator.CreatePurchaseInvoiceServiceObject();
			_productService = ObjectCreator.CreateProductServiceObject();
		}
		public void OnGet()
		{
			productList = _productService.ReadProductList();
		}

		public void OnPost()
		{
			try
			{
				//find product
				Product product = _productService.FindProductById(productId);
				if (product == null)
				{
					throw new Exception("Product id not found");
				}
				PurchaseInvoiceItem purchaseInvoiceItem = new PurchaseInvoiceItem(productId, product.productName, product.price, quantity, purchaseInvoiceId);
				_purchaseInvoiceService.CreatePurchaseInvoiceItem(purchaseInvoiceItem);

				//update stock
				product.stock += purchaseInvoiceItem.quantity;
				_productService.EditProduct(product);

				//update purchase invoice
				PurchaseInvoice purchaseInvoice = _purchaseInvoiceService.FindPurchaseInvoiceById(purchaseInvoiceItem.purchaseInvoiceId);
				if (purchaseInvoice == null)
				{
					throw new Exception("Purchase invoice id not found");
				}
				purchaseInvoice.totalAmount = purchaseInvoice.totalAmount + purchaseInvoiceItem.totalPrice;
				_purchaseInvoiceService.EditPurchaseInvoice(purchaseInvoice);

				message = "Create new item successfully!";
				Response.Redirect("/purchase_invoices/edit?purchaseInvoiceId=" + purchaseInvoiceId);
			}
			catch (Exception ex)
			{
				message = "An error occurred: " + ex.Message;
				productList = _productService.ReadProductList();
			}
		}
	}
}
