using System.ComponentModel.DataAnnotations;

namespace CodeFood.Web.Models;
public class RegistrationViewModel
{
    [Required]
    [Display(Name = "Username")] 
    public string? UserName { get; set; }

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmedPassword { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")] 
    public string? Email { get; set; }
}
