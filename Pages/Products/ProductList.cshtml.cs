using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;
using System;

namespace store_managament_oop.Pages.Products
{
	public class ProductListModel : PageModel
	{
		public List<Product> ProductList { get; set; }
		private IProductService _productService;
		[BindProperty]
		public string searchKeyword { get; set; }
		[BindProperty(SupportsGet = true)]
		public int page { get; set; }
		[BindProperty(SupportsGet = true)]
		public int pageSize { get; set; }
		[BindProperty]
		public int categoryId { get; set; }
		public string message = string.Empty;

		//category
		public List<Category> CategoryList { get; set; }
		private ICategoryService _categoryService;
		public ProductListModel() : base()
		{
			_productService = ObjectCreator.CreateProductServiceObject();
			_categoryService = ObjectCreator.CreateCategoryServiceObject();
		}
		public void OnGet()
		{
			try
			{
				//            if(page == 0)
				//            {
				//                page = 1;
				//            }

				//            if(pageSize == 0)
				//            {
				//                pageSize = 3;
				//            }
				//ProductList = _productService.ReadProductListByPage(page, pageSize);
				LoadProducts();
				LoadCategories();
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
		public void OnPost(string action)
		{
			try
			{
				if (action == "expired")
				{
					ProductList = _productService.ReadExpiredProductList();
					LoadCategories();
					return;
				}
				// If both categoryId and searchKeyword are specified, filter by both.
				if (categoryId != 0 && !string.IsNullOrEmpty(searchKeyword))
				{
					ProductList = _productService.ReadProductListByCategoryId(categoryId)
						.Where(p => p.categoryId == categoryId && p.productName.Contains(searchKeyword))
						.ToList();
				}
				// If only categoryId is specified, filter by category.
				else if (categoryId != 0)
				{
					ProductList = _productService.ReadProductListByCategoryId(categoryId);
				}
				// If only searchKeyword is specified, filter by search.
				else if (!string.IsNullOrEmpty(searchKeyword))
				{
					ProductList = _productService.ReadProductList(searchKeyword);
				}
				// Otherwise, load all products.
				else
				{
					ProductList = _productService.ReadProductList();
				}

				if (ProductList.Count == 0)
				{
					message = "No products found.";
				}

				LoadCategories();
			}

			catch (Exception ex)
			{
				message = ex.Message;
			}

		}

		private void LoadProducts()
		{
			ProductList = _productService.ReadProductList();
			if (ProductList.Count == 0)
			{
				message = "No products available.";
			}
		}

		private void LoadCategories()
		{
			CategoryList = _categoryService.ReadCategoryList();
			if (CategoryList.Count == 0)
			{
				message = "No categories available.";
			}
		}
	}
}
