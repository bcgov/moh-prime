using System.ComponentModel.DataAnnotations;
using Prime.ViewModels.Parties;

namespace Prime.ViewModels
{
    public class OrganizationUpdateModel
    {
        [Key]
        public int Id { get; set; }

        public string RegistrationId { get; set; }

        public string Name { get; set; }

        public string DoingBusinessAs { get; set; }
    }
}
