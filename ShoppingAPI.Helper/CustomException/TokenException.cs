using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Helper.CustomException
{
    public class TokenException:Exception
    {
        public TokenException(string message="Token Hatası Oluştu"):base(message)
        {

        }
    }
}
