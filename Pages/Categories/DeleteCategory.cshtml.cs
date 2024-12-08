using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Categories
{
    public class DeleteCategoryModel : PageModel
    {
		private ICategoryService _categoryService;

		public Category category { get; set; }

		[BindProperty(SupportsGet = true)] // cho phep lay tham so tu URL
		public int categoryId { get; set; }

		public string message = string.Empty;

		public DeleteCategoryModel()
		{
			_categoryService = ObjectCreator.CreateCategoryServiceObject();
		}
		public void OnGet()
        {
			//TODO
		}
        public void OnPost(int categoryId)
        {
            try
            {
				category = _categoryService.FindCategoryById(categoryId);
				if (category == null)
				{
					message = "Category not found";
					return;
				}
				else
				{
					//delete
					_categoryService.DeleteCategory(categoryId);
					Response.Redirect("/categories");
				}
			} catch (Exception ex)
			{
				message = ex.Message;
			}
        }
    }
}
