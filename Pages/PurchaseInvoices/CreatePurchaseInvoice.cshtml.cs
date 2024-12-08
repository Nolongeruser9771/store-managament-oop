using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.PurchaseInvoices
{
    public class CreatePurchaseInvoiceModel : PageModel
    {
		private IPurchaseInvoiceService _purchaseInvoiceService;
		[BindProperty]
		public string providerName { get; set; }
		[BindProperty]
		public string staffName { get; set; }
		[BindProperty]
		public DateTime invoiceDate { get; set; }
		public string message = string.Empty;
		public CreatePurchaseInvoiceModel() : base()
		{
			_purchaseInvoiceService = ObjectCreator.CreatePurchaseInvoiceServiceObject(); 
		}
		public void OnGet()
        {
			//TODO
        }

		public void OnPost()
		{
			try
			{
				PurchaseInvoice purchaseInvoice = new PurchaseInvoice(providerName, staffName, invoiceDate);
				_purchaseInvoiceService.CreatePurchaseInvoice(purchaseInvoice);

				message = "Create invoice succesffully";
				Response.Redirect("/purchase_invoices/edit?purchaseInvoiceId=" + purchaseInvoice.id);
			}
			catch (Exception ex)
			{
				message = "An error occurred: " + ex.Message;
			}
		}
    }
}
