using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.SalesInvoices
{
    public class EditSalesInvoiceModel : PageModel
    {
		[BindProperty(SupportsGet = true)]
		public int salesInvoiceId { get; set; }
		private ISalesInvoiceService _salesInvoiceService;
		[BindProperty]
		public SalesInvoice salesInvoice { get; set; }
		public string message = string.Empty;
		[BindProperty]
		public string customerName { get; set; }
		[BindProperty]
		public string staffName { get; set; }
		[BindProperty]
		public DateTime invoiceDate { get; set; }
		public List<SalesInvoiceItem> salesInvoiceItems = new List<SalesInvoiceItem>();
		public EditSalesInvoiceModel()
		{
			_salesInvoiceService = ObjectCreator.CreateSalesInvoiceServiceObject();
		}
		public void OnGet()
		{
			//find invoice id
			salesInvoice = _salesInvoiceService.FindSalesInvoiceById(salesInvoiceId);

			//get invoice item list
			salesInvoiceItems = _salesInvoiceService.ReadSalesInvoiceItemListByInvoiceId(salesInvoiceId);
		}

		public void OnPost()
		{
			try
			{
				//find invoice
				salesInvoice = _salesInvoiceService.FindSalesInvoiceById(salesInvoiceId);
				//update invoice
				if (salesInvoice == null)
				{
					message = "Invoice not found";
					return;
				}
				else
				{
					//edit invoice
					salesInvoice.customerName = customerName;
					salesInvoice.staffName = staffName;
					salesInvoice.invoiceDate = invoiceDate;

					_salesInvoiceService.EditSalesInvoice(salesInvoice);

					message = "Save changes successfully!";
				}

				//save update
				_salesInvoiceService.EditSalesInvoice(salesInvoice);

				//get invoice item list
				salesInvoiceItems = _salesInvoiceService.ReadSalesInvoiceItemListByInvoiceId(salesInvoiceId);
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
	}
}
