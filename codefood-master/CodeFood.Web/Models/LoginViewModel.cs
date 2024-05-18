using System.ComponentModel.DataAnnotations;

namespace CodeFood.Web.Models;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Username")] 
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")] 
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Remember me?")] 
    public bool RememberMe { get; set; }
}
