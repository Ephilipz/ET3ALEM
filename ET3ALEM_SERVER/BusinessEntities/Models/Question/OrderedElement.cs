namespace BusinessEntities.Models
{
    public class OrderedElement
    {
        public int Id { get; set; }
        // public OrderQuestion OrderQuestion { get; set; }
        public int OrderQuestionId { get; set; }
        public string Text { get; set; }
    }
}