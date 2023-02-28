namespace BudgetTracker.WebApi.TransferModels;

public class GenericResponse
{
    public bool HasError { get; set; }
    public List<string> ErrorMsg { get; set; } = new();
}
