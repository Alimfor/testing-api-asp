using Dapper.FluentMap.Mapping;

namespace Exam.Utils.Mapper;

public class EntityStoreMapper : EntityMap<EntityStoreDate>
{
    public EntityStoreMapper()
    {
        Map(date => date.CreatedAt).ToColumn("created_at");
        Map(date => date.UpdatedAt).ToColumn("updated_at");
    }
}