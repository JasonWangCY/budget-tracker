namespace BudgetTracker.Domain.Constants;

public static class TransactionConstants
{
    public static readonly List<string> DefaultCategoryTypes = new()
    {
        DefaultCategoryType.TRANSPORT,
        DefaultCategoryType.FOOD,
        DefaultCategoryType.SALARY,
        DefaultCategoryType.ALLOWANCE,
        DefaultCategoryType.GROCERY,
        DefaultCategoryType.ENTERTAINMENT
    };

    public static readonly List<string> DefaultTransactionTypes = new()
    {
        DefaultTransactionType.INCOME,
        DefaultTransactionType.EXPENSE
    };

    public static class DefaultCategoryType
    {
        public const string TRANSPORT = "TRANSPORT";
        public const string FOOD = "FOOD";
        public const string SALARY = "SALARY";
        public const string ALLOWANCE = "ALLOWANCE";
        public const string GROCERY = "GROCERY";
        public const string ENTERTAINMENT = "ENTERTAINMENT";
    }

    public static class DefaultTransactionType
    {
        public const string EXPENSE = "EXPENSE";
        public const string INCOME = "INCOME";
    }
}
