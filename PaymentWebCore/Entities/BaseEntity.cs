namespace PaymentWebCore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? PayDate { get; set; } = default(DateTime?);    

    }
}
