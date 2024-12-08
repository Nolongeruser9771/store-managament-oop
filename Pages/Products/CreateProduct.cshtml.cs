using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Products
{
	public class CreateProductModel : PageModel
	{
		//Binding to get values
		[BindProperty]
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
		private IProductService _productService;
		//category
		public List<Category> CategoryList { get; set; }
		private ICategoryService _categoryService;

		//constructor
		public CreateProductModel() : base()
		{
			_productService = ObjectCreator.CreateProductServiceObject();
			_categoryService = ObjectCreator.CreateCategoryServiceObject();
		}
		public void OnGet()
		{
			message = "Please enter product information";
			//category
			LoadCategoryList();
		}
		public void OnPost()
		{
			try
			{
				Product product = new Product(productName, price, expiredDate, manufacturer, manufacturingYear, stock, categoryId);
				_productService.CreateProduct(product);

				message = "Create new product successfully!";
				Response.Redirect("/");
			}
			catch (Exception ex)
			{
				message = ex.Message;

				//re-get category
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
