using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SecondName { get; set; }
        [Required(ErrorMessage = "Login can't be null!")]
        [MaxLength(50, ErrorMessage ="Login can't be more 50 symbols!")]
        public string Login { get; set; } = null!;
        [Required(ErrorMessage = "Password can't be null!")]
        [MaxLength(50, ErrorMessage = "Password can't be more 50 symbols!")]
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string OfficeId { get; set; } = null!;

        [JsonIgnore]
        public string FIO => Id != null ? (LastName + " " + FirstName[0] + "." + (SecondName != null ? SecondName[0] + "." : "")) : " ";

        public virtual Office Office { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Specialization> Specializations { get; set; }
    }
}
