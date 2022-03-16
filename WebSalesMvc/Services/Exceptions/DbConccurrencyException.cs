using System;

namespace WebSalesMvc.Services.Exceptions
{
    public class DbConccurrencyException  : ApplicationException
    {
        public DbConccurrencyException (string message) : base(message)
        {

        }
    }
}
