using System.Text.Json;

namespace BudgetTracker.WebApi.TransferModels;

public class ErrorResponse
{
    public int StatusCode { get; init; }
    public string Message { get; init; } = null!;
}
