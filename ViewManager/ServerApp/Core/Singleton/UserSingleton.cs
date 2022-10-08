using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class UserSingleton
    {
        public static User User { get; set; } = null;
    }
}
