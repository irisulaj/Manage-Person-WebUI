using Microsoft.VisualBasic;

namespace ManagePersonWebUI.Models
{
    public class PersonViewModel
    {
        public int IdPerson { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public DateTime? DateofBirth { get; set; }

        public string? Phone { get; set; }

        public string Gender { get; set; }

        public bool IsEmployed { get; set; }

        public int? IdMaritalstatus { get; set; }

        public string? PlaceofBirth { get; set; }
    }
}
