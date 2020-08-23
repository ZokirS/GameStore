using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using GameStore.WebUI.Infrastrucuture.Abstract;

namespace GameStore.WebUI.Infrastrucuture.Concrete
{
    public class FormAuthProvider: IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}