

namespace Infrastructure.Message
{
    public class OutboxMessage  
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public string Payload { get; set; } = null!;

        public DateTime OccurredOn { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public string? Erorr {  get; set; }
    }
}
