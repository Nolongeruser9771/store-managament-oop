using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        //Binding to get values
        [BindProperty]
        public string categoryName { get; set; }
		[BindProperty]
		public string categoryDescription { get; set; }
        public string message { get; set; } = string.Empty;
        private ICategoryService _categoryService;
        
        //constructor
        public CreateCategoryModel():base()
        {
            _categoryService = ObjectCreator.CreateCategoryServiceObject();
        }
        public void OnGet()
        {
            message = "Please enter category information";
        }
        public void OnPost() 
        {
            try
            {
                Category category = new Category(categoryName, categoryDescription);
                _categoryService.CreateCategory(category);

                message = "Create new category successfully!";
                Response.Redirect("/categories");
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }
    }
}
