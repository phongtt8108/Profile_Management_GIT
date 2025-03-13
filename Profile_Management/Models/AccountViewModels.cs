using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Profile_Management.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Eメールは必須です。")]
        [Display(Name = "Eメール")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }
        [Required]
        [Display(Name = "コード")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "パスワードを保存する？")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Eメールは必須です。")]
        [Display(Name = "Eメール")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage ="Eメールは必須です。")]
        [Display(Name = "Eメール")]
        [EmailAddress(ErrorMessage = "有効なメールをご入力ください。。")]
        public string Email { get; set; }
        [Required(ErrorMessage ="パスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        
        [EmailAddress]
        [Required(ErrorMessage = "Eメールは必須です。")]
        [Display(Name = "Eメール")]
        public string Email { get; set; }

        [Required(ErrorMessage = "パスワードは必須です。")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認パスワード")]
        [Compare("Password", ErrorMessage = "パスワードと確認パスワードは一致しません。")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Eメールは必須です。")]
        [Display(Name = "Eメール")]
       
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認パスワード")]
        [Compare("Password", ErrorMessage = "パスワードと確認パスワードは一致しません。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
   
        [EmailAddress]
        [Required(ErrorMessage = "Eメールは必須です。")]
        [Display(Name = "Eメール")]
        public string Email { get; set; }
    }
}
