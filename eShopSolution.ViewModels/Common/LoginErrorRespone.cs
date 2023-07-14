using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Common
{
    public class LoginErrorRespone<T> : LoginRespone<T>
    {
        public string[] ValidationErrors { get; set; }

        public LoginErrorRespone()
        {
        }

        public LoginErrorRespone(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public LoginErrorRespone(string[] validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}
