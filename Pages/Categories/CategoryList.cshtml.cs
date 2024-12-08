using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using store_managament_oop.Utils;

namespace store_managament_oop.Pages.Categories
{
    public class CategoryListModel : PageModel
    {
        public List<Category> CategoryList { get; set; }
        private ICategoryService _categoryService;
        [BindProperty]
        public string searchKeyword { get; set; }
        [BindProperty(SupportsGet =true)]
        public int page { get; set; }
        [BindProperty(SupportsGet =true)]
        public int pageSize { get; set; }
        public string message = string.Empty;

		public CategoryListModel()
        {
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
				//CategoryList = _categoryService.ReadCategoryListByPage(page, pageSize);
				CategoryList = _categoryService.ReadCategoryList();
                //in case of empty list
                if(CategoryList.Count == 0)
                {
                    message = "No categories";
                }
			} catch(Exception ex)
            {
                message = ex.Message;
            }
        }
        public void OnPost()
        {
            try
            {
				CategoryList = _categoryService.ReadCategoryList(searchKeyword);
				if (CategoryList.Count == 0 || CategoryList == null)
				{
					message = "No categories";
				}
			} catch(Exception ex)
            {
                message = ex.Message;
            }

		}
    }
}
