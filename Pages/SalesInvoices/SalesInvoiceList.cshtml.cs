using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.SalesInvoices
{
	public class SalesInvoiceListModel : PageModel
	{
		public List<SalesInvoice> salesInvoices { get; set; }
		private ISalesInvoiceService _salesInvoiceService;
		[BindProperty]
		public string searchKeyword { get; set; }
		public string message = string.Empty;
		public SalesInvoiceListModel()
		{
			_salesInvoiceService = ObjectCreator.CreateSalesInvoiceServiceObject();
		}
		public void OnGet()
		{
			//get list
			salesInvoices = _salesInvoiceService.ReadSalesInvoiceList();
		}
		public void OnPost()
		{
			try
			{
				salesInvoices = _salesInvoiceService.ReadSalesInvoiceList(searchKeyword);
				if (salesInvoices.Count == 0)
				{
					message = "No invoice found.";
				}
			}

			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
	}
}
