namespace ShoppingAPI.Entity.Base
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public Guid GUID { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
