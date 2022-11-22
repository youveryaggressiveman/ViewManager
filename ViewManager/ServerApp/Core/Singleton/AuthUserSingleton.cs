using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class AuthUserSingleton
    {
        public static AuthUser AuthUser { get; set; } = new AuthUser();
    }
}
