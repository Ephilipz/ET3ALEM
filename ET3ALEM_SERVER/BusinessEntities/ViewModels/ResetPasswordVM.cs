namespace BusinessEntities.ViewModels
{
    public class ResetPasswordVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResetPassword { get; set; }
        public string RecoveryToken { get; set; }
    }
}