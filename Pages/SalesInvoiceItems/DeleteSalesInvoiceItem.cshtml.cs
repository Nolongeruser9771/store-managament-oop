using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.SalesInvoiceItems
{
    public class DeleteSalesInvoiceItemModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public int salesInvoiceItemId { get; set; }
		[BindProperty]
		public SalesInvoiceItem salesInvoiceItem { get; set; }
		public string message = string.Empty;
		public Product product { get; set; }
		private IProductService _productService;
		private ISalesInvoiceService _salesInvoiceService;
		public int salesInvoiceId { get; set; }
		public DeleteSalesInvoiceItemModel()
		{
			_salesInvoiceService = ObjectCreator.CreateSalesInvoiceServiceObject();
			_productService = ObjectCreator.CreateProductServiceObject();
		}
		public void OnGet()
		{
			//find invoice
			SalesInvoiceItem salesInvoiceItem = _salesInvoiceService.FindSalesInvoiceItemById(salesInvoiceItemId);

			//find product
			if (salesInvoiceItem != null)
			{
				salesInvoiceId = salesInvoiceItem.salesInvoiceId;
			}
		}

		public void OnPost()
		{
			try
			{
				//delete salesInvoiceItem
				SalesInvoiceItem salesInvoiceItem = _salesInvoiceService.FindSalesInvoiceItemById(salesInvoiceItemId);
				if (salesInvoiceItem == null)
				{
					message = "Sales Invoice Item not found";
					return;
				}
				else
				{
					product = _productService.FindProductById(salesInvoiceItem.productId);
					//delete
					_salesInvoiceService.DeleteSalesInvoiceItem(salesInvoiceItemId);
					message = "Delete successfully!";
					//update stock
					product.stock += salesInvoiceItem.quantity;
					_productService.EditProduct(product);


					Response.Redirect("/sales_invoices/edit?salesInvoiceId=" + salesInvoiceItem.salesInvoiceId);
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
	}
}
