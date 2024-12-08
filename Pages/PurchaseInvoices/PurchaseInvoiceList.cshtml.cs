using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;
using System;

namespace store_managament_oop.Pages.PurchaseInvoices
{
    public class PurchaseInvoiceListModel : PageModel
    {
        public List<PurchaseInvoice> purchaseInvoices { get; set; }
        private IPurchaseInvoiceService _purchaseInvoiceService;
		[BindProperty]
		public string searchKeyword { get; set; }
		public string message = string.Empty;
		public PurchaseInvoiceListModel()
        {
            _purchaseInvoiceService = ObjectCreator.CreatePurchaseInvoiceServiceObject();
        }
		public void OnGet()
        {
            //get list
            purchaseInvoices = _purchaseInvoiceService.ReadPurchaseInvoiceList();
        }
        public void OnPost()
        {
			try
			{
				purchaseInvoices = _purchaseInvoiceService.ReadPurchaseInvoiceList(searchKeyword);
				if (purchaseInvoices.Count == 0)
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
