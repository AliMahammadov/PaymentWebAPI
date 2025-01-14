namespace PaymentWebCore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime Data { get; set; } = DateTime.UtcNow;

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
