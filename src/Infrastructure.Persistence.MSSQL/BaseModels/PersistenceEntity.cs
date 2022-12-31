
namespace Infrastructure.Persistence.MSSQL.BaseModels
{
    public class PersistenceEntity<TKey> where TKey:struct
    {
        public TKey Id { get; set; }
    }
}
