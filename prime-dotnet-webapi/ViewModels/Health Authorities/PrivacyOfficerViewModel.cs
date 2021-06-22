namespace Prime.ViewModels.HealthAuthorities
{
    public class PrivacyOfficerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SMSPhone { get; set; }

        public static implicit operator ContactViewModel(PrivacyOfficerViewModel vm)
        {
            return new ContactViewModel
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Phone = vm.Phone,
                SMSPhone = vm.SMSPhone,
            };
        }
    }
}
