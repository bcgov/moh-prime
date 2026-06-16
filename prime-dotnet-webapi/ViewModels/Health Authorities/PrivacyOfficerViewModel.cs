namespace Prime.ViewModels.HealthAuthorities
{
    public class PrivacyOfficerViewModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneExtension { get; set; }
        public string SMSPhone { get; set; }

        public static implicit operator ContactViewModel(PrivacyOfficerViewModel vm)
        {
            return new ContactViewModel
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Phone = vm.Phone,
                PhoneExtension = vm.PhoneExtension,
                SMSPhone = vm.SMSPhone,
            };
        }
    }
}
