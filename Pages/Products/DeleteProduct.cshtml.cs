using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Products
{
	public class DeleteProductModel : PageModel
	{
		private IProductService _productService;

		public Product product { get; set; }

		[BindProperty(SupportsGet = true)] // cho phep lay tham so tu URL
		public int productId { get; set; }

		public string message = string.Empty;

		public DeleteProductModel()
		{
			_productService = ObjectCreator.CreateProductServiceObject();
		}
		public void OnGet()
		{
			product = _productService.FindProductById(productId);
			if (product == null)
			{
				message = "Product not found";
				return;
			}
		}
		public void OnPost(int productId)
		{
			try
			{
				product = _productService.FindProductById(productId);
				if (product == null)
				{
					message = "Product not found";
					return;
				}
				else
				{
					//delete
					_productService.DeleteProduct(productId);
					Response.Redirect("/");
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
	}
}
