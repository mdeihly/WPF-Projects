namespace DemoLibrary.Models
{
    public class PersonModel
    {
        public int PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Available { get; set; }
        public string? Country { get; set; }
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}