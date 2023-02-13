using System.ComponentModel.DataAnnotations;

namespace DatingZone.Entities.Dtos
{
    public class MemberEditDto
    {
        public string LookingFor { get; set; }
        public string Introduction { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
