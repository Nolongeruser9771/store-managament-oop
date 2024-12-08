using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.PurchaseInvoices
{
    public class DeletePurchaseInvoiceModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int purchaseInvoiceId { get; set; }
		[BindProperty]
        public PurchaseInvoice purchaseInvoice { get; set; }
        private IPurchaseInvoiceService _purchaseInvoiceService;
        private IProductService _productService;
		public string message { get; set; }
		List<PurchaseInvoiceItem> purchaseInvoiceItemList { get; set; }
		Dictionary<int, int> productQuantities { get; set; } = new Dictionary<int, int>();
		public DeletePurchaseInvoiceModel()
        {
            _purchaseInvoiceService = ObjectCreator.CreatePurchaseInvoiceServiceObject();
			_productService = ObjectCreator.CreateProductServiceObject();
		}
		public void OnGet()
        {
			//find invoice
			purchaseInvoice = _purchaseInvoiceService.FindPurchaseInvoiceById(purchaseInvoiceId);
		}
		public void OnPost()
		{
            try
            {
				//find invoice
				purchaseInvoice = _purchaseInvoiceService.FindPurchaseInvoiceById(purchaseInvoiceId);
				
				//delete
				if (purchaseInvoice == null)
				{
					message = "Invoice not found";
				}
				else
				{
					//find product list
					purchaseInvoiceItemList = _purchaseInvoiceService.ReadPurchaseInvoiceItemListByInvoiceId(purchaseInvoiceId);
					foreach (var invoiceItem in purchaseInvoiceItemList)
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

					_purchaseInvoiceService.DeletePurchaseInvoice(purchaseInvoiceId);
					
					//update stock
					if (purchaseInvoiceItemList.Count != 0)
					{
						foreach (var productId in productQuantities.Keys)
						{
							Product product = _productService.FindProductById(productId);
							if (product != null)
							{
								product.stock -= productQuantities[productId];
								_productService.EditProduct(product);
							}
						}
					}
					Response.Redirect("/purchase_invoices");
				}
			}
			catch(Exception ex)
            {
                message = ex.Message;
            }
		}
	}
}
