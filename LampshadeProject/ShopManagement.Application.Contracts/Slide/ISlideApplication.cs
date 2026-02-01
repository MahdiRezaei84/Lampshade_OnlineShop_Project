using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        OperationResult CreateSlide(CreateSlide command);
        OperationResult EditSlide(EditSlide command);
        OperationResult RemoveSlide(long id);
        OperationResult RestoreSlide(long id);
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetSlideList();
    }
}
