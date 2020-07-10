using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "First name cannot be empty")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name cannot be empty")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "BirthDate cannot be empty and over 18 years old")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Email cannot be empty")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username cannot be empty")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again")]
        public string ConfirmPassword { get; set; }
        public User()
        {
            BirthDate = DateTime.Now;
        }
    }
}
