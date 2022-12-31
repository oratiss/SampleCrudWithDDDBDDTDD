
namespace Infrastructure.Persistence.MSSQL.BaseModels
{
    public class PersistingEntity<TKey> where TKey:struct
    {
        public TKey Id { get; set; }
    }
}
