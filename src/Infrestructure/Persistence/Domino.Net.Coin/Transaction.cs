using LiteDB;

namespace Domino.Net.Coin;

public class Transaction
{
    public Transaction(string sender, string recipient, double amount)
    {
        this.Sender = sender;
        this.Recipient = recipient;
        this.Amount = amount;
        this.TimeStamp = DateTime.Now.Ticks;
    }

    public long TimeStamp { get; private set; }
    public string Sender { get; private set; }
    public string Recipient { get; private set; }
    public double Amount { get; private set; }
    public double Fee { get; set; }

    public static void AddToPool(Transaction transaction)
    {
        var trxPool = GetPool();
        trxPool.Insert(transaction);
    }

    public static void Add(Transaction transaction)
    {
        var transactions = GetAll();
        transactions.Insert(transaction);
    }

    public static ILiteCollection<Transaction> GetPool()
    {
        var coll = DbContext.DB.GetCollection<Transaction>(DbContext.TBL_TRANSACTION_POOL);
        return coll;
    }

    public static ILiteCollection<Transaction> GetAll()
    {
        var coll = DbContext.DB.GetCollection<Transaction>(DbContext.TBL_TRANSACTIONS);
        return coll;
    }

    public static IEnumerable<Transaction> GetHistory(string name)
    {
        var coll = DbContext.DB.GetCollection<Transaction>(DbContext.TBL_TRANSACTIONS);
        var transactions = coll.Find(x => x.Sender == name || x.Recipient == name);
        return transactions;
    }

    public static double GetBalance(string name)
    {
        double balance = 0;
        double spending = 0;
        double income = 0;

        var collection = GetAll();
        var transactions = collection.Find(x => x.Sender == name || x.Recipient == name);

        foreach (Transaction trx in transactions)
        {
            var sender = trx.Sender;
            var recipient = trx.Recipient;

            if (name.ToLower().Equals(sender.ToLower()))
            {
                spending += trx.Amount + trx.Fee;
            }

            if (name.ToLower().Equals(recipient.ToLower()))
            {
                income += trx.Amount;
            }

            balance = income - spending;
        }

        return balance;
    }
}
