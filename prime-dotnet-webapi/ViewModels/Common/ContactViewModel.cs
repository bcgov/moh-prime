namespace Prime.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobRoleTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SMSPhone { get; set; }
        public string Fax { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }
}
