namespace Students.Domain.Business
{
    public class StudentPaymentDto
    {
        public string StudentName { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
