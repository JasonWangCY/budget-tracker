namespace BudgetTracker.WebApi.TransferModels;

public class GenericResponse
{
    public bool HasError { get; init; }
    public List<string> ErrorMsg { get; init; } = new();
}
