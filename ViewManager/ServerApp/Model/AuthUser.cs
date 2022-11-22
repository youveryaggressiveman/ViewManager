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
        public string Id { get; set; } = null;
        public string RoleValue { get; set; } = null;
        public string Token { get; set; } = null;
    }
}
