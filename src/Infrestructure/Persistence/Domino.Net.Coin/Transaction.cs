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
}
