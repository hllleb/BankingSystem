namespace BankingSystem;

public class Transaction
{
    private static int transactionsCount = 0;
    private readonly decimal amount;
    
    public Transaction(TransactionType transactionType, decimal amount)
    {
        if (!Enum.IsDefined(transactionType))
        {
            throw new ArgumentException("Invalid transaction type", nameof(transactionType));
        }
        
        this.TransactionId = ++transactionsCount;
        this.TransactionType = transactionType;
        this.Amount = amount;
        this.Timestamp = DateTime.Now;
    }
    
    public int TransactionId { get; }

    public TransactionType TransactionType { get; }

    public DateTime Timestamp { get; }
    
    public decimal Amount
    {
        get => this.amount;
        private init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Amount is be negative");
            }

            this.amount = value;
        }
    }

}