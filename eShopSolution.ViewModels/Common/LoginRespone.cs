using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Common
{
    public class LoginRespone<T> : ApiResult<T>
    {
        public object Result { get; set; }
        public LoginRespone(T resultObj, object result)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Result = result;
        }

        public LoginRespone()
        {
            IsSuccessed = true;
        }
    }
}
