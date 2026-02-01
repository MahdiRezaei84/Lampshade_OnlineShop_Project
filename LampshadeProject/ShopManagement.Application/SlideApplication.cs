using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        #region constractor
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }
        #endregion

        #region create slide
        public OperationResult CreateSlide(CreateSlide command)
        {
            var operation = new OperationResult();
            var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }
        #endregion

        #region edit slide
        public OperationResult EditSlide(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetById(command.Id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText);
            _slideRepository.SaveChanges();
            return operation.Succedded();

        }
        #endregion

        #region get slide details
        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }
        #endregion

        #region get slide list
        public List<SlideViewModel> GetSlideList()
        {
            return _slideRepository.GetSlideList();
        }
        #endregion

        #region remove slide
        public OperationResult RemoveSlide(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetById(id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }
        #endregion

        #region restore slide
        public OperationResult RestoreSlide(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.GetById(id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }
        #endregion

    }
}
