namespace Store.Entity
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
