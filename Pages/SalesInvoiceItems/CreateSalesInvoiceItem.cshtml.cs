using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.SalesInvoiceItems
{
    public class CreateSalesInvoiceItemModel : PageModel
    {
		public List<Product> productList { get; set; }
		private IProductService _productService;
		private ISalesInvoiceService _salesInvoiceService;

		[BindProperty(SupportsGet = true)]
		public int salesInvoiceId { get; set; }
		[BindProperty]
		public int productId { get; set; }
		[BindProperty]
		public int quantity { get; set; }
		[BindProperty]
		public int price { get; set; }
		public SalesInvoice salesInvoice { get; set; }
		public string message = string.Empty;
		public CreateSalesInvoiceItemModel() : base()
		{
			_salesInvoiceService = ObjectCreator.CreateSalesInvoiceServiceObject();
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
				} else if (product.stock < quantity)
				{
					throw new Exception("Stock not enough. Only " + product.stock + " items available.");
				}
				SalesInvoiceItem salesInvoiceItem = new SalesInvoiceItem(productId, product.productName, product.price, quantity, salesInvoiceId);
				_salesInvoiceService.CreateSalesInvoiceItem(salesInvoiceItem);

				//update stock
				product.stock -= salesInvoiceItem.quantity;
				_productService.EditProduct(product);

				//update purchase invoice
				SalesInvoice salesInvoice = _salesInvoiceService.FindSalesInvoiceById(salesInvoiceItem.salesInvoiceId);
				if (salesInvoice == null)
				{
					throw new Exception("Sales invoice id not found");
				}
				salesInvoice.totalAmount += salesInvoiceItem.totalPrice;
				_salesInvoiceService.EditSalesInvoice(salesInvoice);

				message = "Create new item successfully!";
				Response.Redirect("/sales_invoices/edit?salesInvoiceId=" + salesInvoiceId);
			}
			catch (Exception ex)
			{
				message = ex.Message;
				productList = _productService.ReadProductList();
			}
		}
	}
}
