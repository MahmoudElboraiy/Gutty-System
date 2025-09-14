using System.Text.Json.Serialization;
using MediatR;

namespace Application.PaymobCommands.PaymobTransactionResponse;

public record PaymobTransactionResponseCommand(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("obj")] ObjectInPaymobTransaction Transaction,
    [property: JsonPropertyName("hmac")] string? Hmac
) : IRequest;

public record PaymobTransactionResponseCommandIntention(
    [property: JsonPropertyName("id")] string Id
);

public record ObjectInPaymobTransaction(
    [property: JsonPropertyName("amount_cents")] int AmountCents,
    [property: JsonPropertyName("created_at")] string CreatedAt,
    [property: JsonPropertyName("currency")] string Currency,
    [property: JsonPropertyName("error_occured")] bool ErrorOccured,
    [property: JsonPropertyName("has_parent_transaction")] bool HasParentTransaction,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("integration_id")] int IntegrationId,
    [property: JsonPropertyName("is_3d_secure")] bool Is3DSecure,
    [property: JsonPropertyName("is_auth")] bool IsAuth,
    [property: JsonPropertyName("is_capture")] bool IsCapture,
    [property: JsonPropertyName("is_refunded")] bool IsRefunded,
    [property: JsonPropertyName("is_standalone_payment")] bool IsStandalonePayment,
    [property: JsonPropertyName("is_voided")] bool IsVoided,
    [property: JsonPropertyName("order")] OrderInObject Order,
    [property: JsonPropertyName("owner")] int Owner,
    [property: JsonPropertyName("pending")] bool Pending,
    [property: JsonPropertyName("source_data")] SourceDataInObject SourceData,
    [property: JsonPropertyName("success")] bool Success
);

public record OrderInObject([property: JsonPropertyName("id")] int Id);

public record SourceDataInObject(
    [property: JsonPropertyName("pan")] string Pan,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("sub_type")] string SubType
);
