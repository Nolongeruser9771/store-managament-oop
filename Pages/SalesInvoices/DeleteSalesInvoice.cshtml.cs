using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.SalesInvoices
{
    public class DeleteSalesInvoiceModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public int salesInvoiceId { get; set; }
		[BindProperty]
		public SalesInvoice salesInvoice { get; set; }
		private ISalesInvoiceService _salesInvoiceService;
		private IProductService _productService;
		public string message { get; set; }
		List<SalesInvoiceItem> salesInvoiceItemList { get; set; }
		Dictionary<int, int> productQuantities { get; set; } = new Dictionary<int, int>();
		public DeleteSalesInvoiceModel()
		{
			_salesInvoiceService = ObjectCreator.CreateSalesInvoiceServiceObject();
			_productService = ObjectCreator.CreateProductServiceObject();
		}
		public void OnGet()
		{
			//find invoice
			salesInvoice = _salesInvoiceService.FindSalesInvoiceById(salesInvoiceId);
		}
		public void OnPost()
		{
			try
			{
				//find invoice
				salesInvoice = _salesInvoiceService.FindSalesInvoiceById(salesInvoiceId);

				//delete
				if (salesInvoice == null)
				{
					message = "Invoice not found";
				}
				else
				{
					//find product list
					salesInvoiceItemList = _salesInvoiceService.ReadSalesInvoiceItemListByInvoiceId(salesInvoiceId);
					foreach (var invoiceItem in salesInvoiceItemList)
					{
						if (productQuantities.ContainsKey(invoiceItem.productId))
						{
							productQuantities[invoiceItem.productId] += invoiceItem.quantity; // Accumulate quantity
						}
						else
						{
							productQuantities[invoiceItem.productId] = invoiceItem.quantity; // Add new entry
						}
					}

					_salesInvoiceService.DeleteSalesInvoice(salesInvoiceId);

					//update stock
					if (salesInvoiceItemList.Count != 0)
					{
						foreach (var productId in productQuantities.Keys)
						{
							Product product = _productService.FindProductById(productId);
							if (product != null)
							{
								product.stock += productQuantities[productId];
								_productService.EditProduct(product);
							}
						}
					}
					Response.Redirect("/sales_invoices");
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
	}
}
