using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<SlideViewModel> Slides;

        #region constractor
        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }
        #endregion

        #region Get Index
        public void OnGet()
        {
            Slides = _slideApplication.GetSlideList();
        }

        #endregion

        #region Get createSlide
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }
        #endregion

        #region createSlide
        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.CreateSlide(command);
            return new JsonResult(result);
        }
        #endregion

        #region Get editSlide
        public IActionResult OnGetEdit(long id)
        {
            var slide = _slideApplication.GetDetails(id);
            return Partial("./Edit", slide);
        }
        #endregion

        #region editSlide
        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.EditSlide(command);
            return new JsonResult(result);
        }
        #endregion

        #region removeSlide
        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.RemoveSlide(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index"); 
        }
        #endregion

        #region restoreSlide
        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.RestoreSlide(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion

    }
}
