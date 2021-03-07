namespace ManagementSystem.Data.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}