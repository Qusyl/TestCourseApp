

namespace Application.Dto
{
    public record OutboxMessageDto(
        Guid id,
        string Type,
        string Payload, 
        DateTime OccurredOn,
        DateTime? ProcessedOn,
        string? Error
        );
}
