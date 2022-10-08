using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class AuthUser
    {
        public object Id { get; set; } = null;
        public object RoleValue { get; set; } = null;
        public object Token { get; set; } = null;
    }
}
