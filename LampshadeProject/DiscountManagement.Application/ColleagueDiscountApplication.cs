using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.VisualBasic;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        #region constractor
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }
        #endregion

        #region define colleagueDiscount
        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DouplicatedRecord);

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();

            return operation.Succedded();
        }
        #endregion

        #region edit colleagueDiscount
        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetById(command.Id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound, NotificationType.Warning);
            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DouplicatedRecord);

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();

            return operation.Succedded();
        }
        #endregion

        #region get colleagueDiscount details
        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }
        #endregion

        #region remove colleagueDiscount
        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var collleagueDiscount = _colleagueDiscountRepository.GetById(id);
            if (collleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound, NotificationType.Warning);

            collleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();

            return operation.Succedded();
        }
        #endregion

        #region restore colleagueDiscount
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var collleagueDiscount = _colleagueDiscountRepository.GetById(id);
            if (collleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound, NotificationType.Warning);

            collleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();

            return operation.Succedded();
        }
        #endregion

        #region search
        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
        #endregion
    }
}
