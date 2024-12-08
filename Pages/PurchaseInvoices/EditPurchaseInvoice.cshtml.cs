using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.PurchaseInvoices
{
    public class EditPurchaseInvoiceModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public int purchaseInvoiceId { get; set; }
		private IPurchaseInvoiceService _purchaseInvoiceService;
		[BindProperty]
        public PurchaseInvoice purchaseInvoice { get; set; }
		public string message = string.Empty;
		[BindProperty]
		public string providerName { get; set; }
		[BindProperty]
		public string staffName { get; set; }
		[BindProperty]
		public DateTime invoiceDate { get; set; }
		public List<PurchaseInvoiceItem> purchaseInvoiceItems = new List<PurchaseInvoiceItem>();
		public EditPurchaseInvoiceModel ()
		{
			_purchaseInvoiceService = ObjectCreator.CreatePurchaseInvoiceServiceObject();
		}
		public void OnGet()
        {
			//find invoice id
			purchaseInvoice = _purchaseInvoiceService.FindPurchaseInvoiceById(purchaseInvoiceId);

			//get invoice item list
			purchaseInvoiceItems = _purchaseInvoiceService.ReadPurchaseInvoiceItemListByInvoiceId(purchaseInvoiceId);
		}

		public void OnPost()
        {
			try
			{
				//find invoice
				purchaseInvoice = _purchaseInvoiceService.FindPurchaseInvoiceById(purchaseInvoiceId);
				//update invoice
				if (purchaseInvoice == null)
				{
					message = "Invoice not found";
					return;
				}
				else
				{
					//edit invoice
					purchaseInvoice.providerName = providerName;
					purchaseInvoice.staffName = staffName;
					purchaseInvoice.invoiceDate = invoiceDate;

					_purchaseInvoiceService.EditPurchaseInvoice(purchaseInvoice);

					message = "Save changes successfully!";
				}

				//save update
				_purchaseInvoiceService.EditPurchaseInvoice(purchaseInvoice);

				//get invoice item list
				purchaseInvoiceItems = _purchaseInvoiceService.ReadPurchaseInvoiceItemListByInvoiceId(purchaseInvoiceId);
			}
			catch(Exception ex)
			{
				message = ex.Message;
			}
        }
    }
}
