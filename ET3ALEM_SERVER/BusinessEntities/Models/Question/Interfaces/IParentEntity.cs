using System.Collections.Generic;

namespace BusinessEntities.Models.Interfaces
{
    public interface IParentEntity
    {
        IEnumerable<IEnumerable<object>> GetChildEntitiesToDelete();
        IEnumerable<IEnumerable<object>> GetChildEntitiesToAdd();
        IEnumerable<IEnumerable<object>> GetChildEntitiesToUpdate();
        IEnumerable<IEnumerable<object>> GetAllChildEntities();
        void SetChildEntitiesToNull();
        IEnumerable<IEnumerable<object>> RemoveDeletedEntitiesFromChildren();
    }
}