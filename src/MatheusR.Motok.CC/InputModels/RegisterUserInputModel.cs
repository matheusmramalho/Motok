using System.ComponentModel.DataAnnotations;

namespace MatheusR.Motok.CC.InputModels;
public class RegisterUserInputModel
{
    public RegisterUserInputModel(string name, string username, string password, string rolename)
    {
        Name = name;
        Username = username;
        Password = password;
        Rolename = rolename;
    }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    public string Username { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "A senha deve ter no mínimo 10 caracteres")]
    public string Password { get; set; }

    [Required(ErrorMessage = "A role do usuário é obrigatória")]
    public string Rolename { get; set; }
}
