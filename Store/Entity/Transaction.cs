namespace Store.Entity
{
    public class Transaction : BaseEntity
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public Wallet Wallet { get; set; }
        public int WalletId { get; set; }
    }
}
