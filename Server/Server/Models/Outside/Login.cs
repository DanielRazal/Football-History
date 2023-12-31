namespace Server.Models.Outside
{
    public class Login
    {
        [Required(ErrorMessage = "The UserName field is required.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
