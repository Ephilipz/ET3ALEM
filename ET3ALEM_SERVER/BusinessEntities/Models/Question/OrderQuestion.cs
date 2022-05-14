using System.Collections.Generic;
using System.Linq;
using BusinessEntities.Enumerators;
using BusinessEntities.Models.Interfaces;
using Helpers.Extensions;

namespace BusinessEntities.Models
{
    public class OrderQuestion : Question, IAfterSaveAction, IParentEntity
    {
        public OrderQuestion()
        {
            QuestionType = QuestionType.Order;
        }
        public virtual IEnumerable<OrderedElement> OrderedElements { get; set; }
        public string CorrectOrderIds { get; set; }

        public void PreformAfterSaveAction()
        {
            CorrectOrderIds = string.Join(",", OrderedElements.Select(element => element.Id));
        }

        public IEnumerable<IEnumerable<object>> GetChildEntitiesToDelete()
        {
            return new List<IEnumerable<object>>() {OrderedElements.GetDeletedElements()};
        }

        public IEnumerable<IEnumerable<object>> GetChildEntitiesToAdd()
        {
            return new List<IEnumerable<object>>() {OrderedElements.GetAddedElements()};
        }

        public IEnumerable<IEnumerable<object>> GetChildEntitiesToUpdate()
        {
            return new List<IEnumerable<object>>() {OrderedElements.GetUpdatedElements()};
        }

        public IEnumerable<IEnumerable<object>> GetAllChildEntities()
        {
            return new List<IEnumerable<object>> {OrderedElements};
        }
    }
}