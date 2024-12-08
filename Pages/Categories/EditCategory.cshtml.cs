using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Categories
{
    public class EditCategoryModel : PageModel
    {
		private ICategoryService _categoryService;

		public Category category { get; set; }
        
        [BindProperty(SupportsGet = true)] // cho phep lay tham so tu URL
        public int categoryId { get; set; }
        
        public string message = string.Empty;
        [BindProperty]
        public string categoryName { get; set; }
        [BindProperty]
        public string categoryDescription { get; set; }

		public EditCategoryModel()
        {
            _categoryService = ObjectCreator.CreateCategoryServiceObject();
        }
        public void OnGet()
        {
            //get id
            if (categoryId == 0){
                message = "Invalid categoryId";
                return;
            }
			category = _categoryService.FindCategoryById(categoryId);
            if(category == null)
            {
                message = "Category not found";
            }
        }

        public void OnPost()
        {
            try
            {
                if(categoryId == 0)
                {
                    message = "Invalid categoryId";
                    return;
                }
                category = _categoryService.FindCategoryById(categoryId);
                if(category == null)
                {
                    message = "Category not found";
                    return;
                } else
                {
                    //edit category
                    category.categoryName = categoryName;
                    category.categoryDescription = categoryDescription;
                    _categoryService.EditCategory(category);
                    Response.Redirect("/categories");
                }
            }catch (Exception ex)
            {
                message = ex.Message;
            }
        }
    }
}
