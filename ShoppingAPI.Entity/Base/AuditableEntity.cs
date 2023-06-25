namespace ShoppingAPI.Entity.Base
{
    public class AuditableEntity:BaseEntity
    {
        public DateTime AddedTime { get; set; }
        public int AddedUser { get; set; }
        public string AddedIPV4Adress { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedUser { get; set; }
        public string UpdatedIPV4Adress { get; set; }

    }
}
