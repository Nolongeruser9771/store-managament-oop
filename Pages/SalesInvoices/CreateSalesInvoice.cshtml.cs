using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.SalesInvoices
{
    public class CreateSalesInvoiceModel : PageModel
    {
		private ISalesInvoiceService _salesInvoiceService;
		[BindProperty]
		public string customerName { get; set; }
		[BindProperty]
		public string staffName { get; set; }
		[BindProperty]
		public DateTime invoiceDate { get; set; }
		public string message = string.Empty;
		public CreateSalesInvoiceModel() : base()
		{
			_salesInvoiceService = ObjectCreator.CreateSalesInvoiceServiceObject();
		}
		public void OnGet()
		{
			//TODO
		}

		public void OnPost()
		{
			try
			{
				SalesInvoice salesInvoice = new SalesInvoice(customerName, staffName, invoiceDate);
				_salesInvoiceService.CreateSalesInvoice(salesInvoice);

				message = "Create invoice succesffully";
				Response.Redirect("/sales_invoices/edit?salesInvoiceId=" + salesInvoice.id);
			}
			catch (Exception ex)
			{
				message = "An error occurred: " + ex.Message;
			}
		}
	}
}
