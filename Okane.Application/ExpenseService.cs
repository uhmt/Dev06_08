using Okane.Domain;

namespace Okane.Application;

public class ExpenseService : IExpenseService
{
    private readonly IExpensesRepository _expensesRepository;

    public ExpenseService(IExpensesRepository expensesRepository) => 
        _expensesRepository = expensesRepository;

    public ExpenseResponse RegisterExpense(CreateExpenseRequest request)
    {
        var expense = new Expense
        {
            Amount = request.Amount,
            Description = request.Description,
            Category = request.Category
        };
        
        _expensesRepository.Add(expense);
        
        return CreateExpenseResponse(expense);
    }

    public IEnumerable<ExpenseResponse> RetrieveAll() => 
        _expensesRepository
            .All()
            .Select(CreateExpenseResponse);

    public bool Delete(int id)
    {
        var expenseToDelete = _expensesRepository.ById(id);

        if (expenseToDelete == null)
            return false;
        
        _expensesRepository.Delete(id);
        return true;
    }

    private static ExpenseResponse CreateExpenseResponse(Expense expense) =>
        new()
        {
            Id = expense.Id,
            Category = expense.Category,
            Description = expense.Description,
            Amount = expense.Amount
        };
}