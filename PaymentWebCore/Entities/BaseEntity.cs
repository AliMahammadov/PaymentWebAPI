namespace PaymentWebCore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreateDateFormatted => CreateDate.HasValue ? CreateDate.Value.ToString("yyyy-MM-dd 'Clock' HH:mm") : null;
        public string UpdateDateFormatted => UpdateDate.HasValue ? UpdateDate.Value.ToString("yyyy-MM-dd 'Clock' HH:mm") : null;

    }

}
