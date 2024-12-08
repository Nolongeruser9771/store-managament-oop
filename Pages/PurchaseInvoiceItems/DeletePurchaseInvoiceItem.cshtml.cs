using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.PurchaseInvoiceItems
{
	public class DeletePurchaseInvoiceItemModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public int purchaseInvoiceItemId { get; set; }
		[BindProperty]
		public PurchaseInvoiceItem purchaseInvoiceItem { get; set; }
		public string message = string.Empty;
		public Product product { get; set; }
		private IProductService _productService;
		private IPurchaseInvoiceService _purchaseInvoiceService;
		public int purchaseInvoiceId { get; set; }
		public DeletePurchaseInvoiceItemModel()
		{
			_purchaseInvoiceService = ObjectCreator.CreatePurchaseInvoiceServiceObject();
			_productService = ObjectCreator.CreateProductServiceObject();
		}
		public void OnGet()
		{
			message = purchaseInvoiceItemId.ToString();
			//find invoice
			PurchaseInvoiceItem purchaseInvoiceItem = _purchaseInvoiceService.FindPurchaseInvoiceItemById(purchaseInvoiceItemId);

			//find product
			if (purchaseInvoiceItem != null)
			{
				purchaseInvoiceId = purchaseInvoiceItem.purchaseInvoiceId;
			}
		}

		public void OnPost()
		{
			try
			{
				//delete purchaseInvoiceItem
				PurchaseInvoiceItem purchaseInvoiceItem = _purchaseInvoiceService.FindPurchaseInvoiceItemById(purchaseInvoiceItemId);
				if (purchaseInvoiceItem == null)
				{
					message = "Purchase Invoice Item not found";
					return;
				}
				else
				{
					product = _productService.FindProductById(purchaseInvoiceItem.productId);
					//delete
					_purchaseInvoiceService.DeletePurchaseInvoiceItem(purchaseInvoiceItemId);
					message = "Delete successfully!";
					//update stock
					product.stock -= purchaseInvoiceItem.quantity;
					_productService.EditProduct(product);


					Response.Redirect("/purchase_invoices/edit?purchaseInvoiceId=" + purchaseInvoiceItem.purchaseInvoiceId);
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
	}
}
