namespace Application.Candidates.Queries.Get
{
    public class GetCandidateVm
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public long DateOfBirth { get; set; }

        public string Surname { get; set; }

        public string Address1 { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string PhoneHome { get; set; }

        public string PhoneMobile { get; set; }

        public string PhoneWork { get; set; }
    }
}