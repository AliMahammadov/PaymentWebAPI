namespace PaymentWebEntity.DTOs
{
    public class UserUpdateDto
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string PhoneNumber { get; set; }
    }
}
