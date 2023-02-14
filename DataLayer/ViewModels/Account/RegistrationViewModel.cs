using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messenger2.DataLayer.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Имя пользователя не указано")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Имя пользователя должено содержать от 4 до 16 символов")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Эл. почта не указана")]
        [EmailAddress(ErrorMessage = "Некорректный адрес эл. почты")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не указан")]
        [StringLength(24, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 24 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не подтвержден")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
