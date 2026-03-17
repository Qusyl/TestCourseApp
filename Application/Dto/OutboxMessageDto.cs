using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
