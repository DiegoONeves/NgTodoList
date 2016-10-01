namespace NgTodoList.Api.Models.Account
{
    public class ChangePasswordViewModel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}