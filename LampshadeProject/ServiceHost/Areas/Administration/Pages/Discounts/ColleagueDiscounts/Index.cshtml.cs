using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        public ColleagueDiscountSearchModel SearchModel;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public SelectList Products;

        #region constractor
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication  _colleagueDiscountsApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication collleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountsApplication = collleagueDiscountApplication;
        }
        #endregion

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            ColleagueDiscounts = _colleagueDiscountsApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountsApplication.Define(command);
            TempData["Notification.Message"] = result.Message;
            TempData["Notification.Type"] = result.Type.ToString();
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var colleagueDiscount = _colleagueDiscountsApplication.GetDetails(id);
            colleagueDiscount.Products = _productApplication.GetProducts();
            return Partial("./Edit", colleagueDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountsApplication.Edit(command);
            TempData["Notification.Message"] = result.Message;
            TempData["Notification.Type"] = result.Type.ToString();
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscountsApplication.Remove(id);
            TempData["Notification.Message"] = result.Message;
            TempData["Notification.Type"] = result.Type.ToString();
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueDiscountsApplication.Restore(id);
            TempData["Notification.Message"] = result.Message;
            TempData["Notification.Type"] = result.Type.ToString();
            return RedirectToPage("./Index");
        }
    
    }
}
