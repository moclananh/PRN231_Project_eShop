using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Common
{
    public class LoginRespone<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }
        public Guid Id { get; set; }
        public LoginRespone(T resultObj, Guid id)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Id = id;
        }

        public LoginRespone()
        {
            IsSuccessed = true;
        }
    }
}
