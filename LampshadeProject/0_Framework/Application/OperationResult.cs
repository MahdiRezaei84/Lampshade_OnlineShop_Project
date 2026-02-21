using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSuccedded { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }

        public OperationResult()
        {
            IsSuccedded = false;
        }

        #region succeedded
        public OperationResult Succedded(string message = "عملیات با موفقیت انجام شد",
            NotificationType type = NotificationType.Success)
        {
            IsSuccedded = true;
            Message = message;
            Type = type;
            return this;
        }
        #endregion

        #region failed
        public OperationResult Failed(string message, NotificationType type = NotificationType.Error)
        {
            IsSuccedded = false;
            Message = message;
            Type = type;
            return this;
        }
        #endregion
    }

    public enum NotificationType
    {
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4

    }
}
