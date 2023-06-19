using System.Text.Json;

namespace BudgetTracker.WebApi.TransferModels;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
}
