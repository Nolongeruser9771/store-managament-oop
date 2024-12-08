using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Products
{
	public class EditProductModel : PageModel
	{
		private IProductService _productService;

		public Product product { get; set; }

		[BindProperty(SupportsGet = true)] // cho phep lay tham so tu URL
		public int productId { get; set; }
		[BindProperty]
		public string productName { get; set; }
		[BindProperty]
		public float price { get; set; }
		[BindProperty]
		public DateTime expiredDate { get; set; }
		[BindProperty]
		public string manufacturer { get; set; }
		[BindProperty]
		public string manufacturingYear { get; set; }
		[BindProperty]
		public int stock { get; set; }
		[BindProperty]
		public int categoryId { get; set; }
		public string message { get; set; } = string.Empty;

		public List<Category> CategoryList { get; set; }
		private ICategoryService _categoryService;

		public EditProductModel()
		{
			_productService = ObjectCreator.CreateProductServiceObject();
			_categoryService = ObjectCreator.CreateCategoryServiceObject();
		}
		public void OnGet()
		{
			//get id
			if (productId == 0)
			{
				message = "Invalid productId";
				return;
			}
			product = _productService.FindProductById(productId);
			if (product == null)
			{
				message = "Product not found";
			}

			LoadCategoryList();
		}

		public void OnPost()
		{
			try
			{
				if (productId == 0)
				{
					message = "Invalid productId";
					return;
				}
				product = _productService.FindProductById(productId);
				if (product == null)
				{
					message = "Product not found";
					return;
				}
				else
				{
					//edit category
					product.productName = productName;
					product.price = price;
					product.expiredDate = expiredDate;
					product.manufacturer = manufacturer;
					product.manufacturingYear = manufacturingYear;
					product.stock = stock;
					product.categoryId = categoryId;
					
					_productService.EditProduct(product);
					Response.Redirect("/");
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
				LoadCategoryList();
			}
		}

		private void LoadCategoryList()
		{
			CategoryList = _categoryService.ReadCategoryList();
			if (CategoryList.Count == 0)
			{
				message = "No category";
			}
		}
	}
}
