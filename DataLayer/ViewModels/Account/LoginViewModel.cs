using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Эл. почта не указана")]
        [EmailAddress(ErrorMessage = "Некорректный адрес эл. почты")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не указан")]
        [StringLength(24, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 24 символов")]
        public string Password { get; set; }
    }
}
