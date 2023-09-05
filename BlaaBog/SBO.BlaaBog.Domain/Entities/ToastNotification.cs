using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class ToastNotification
    {
        private ToastColor _status;
        public ToastColor Status { get { return _toastStatus; } set { _toastStatus = value; } }

        private string _message;
        public string Message { get { return _message; } set { _message = value; } }
    }
}
