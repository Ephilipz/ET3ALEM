namespace BusinessEntities.Models
{
    public class OrderedChoice
    {
        public int Id { get; private set; }
        public OrderQuestion OrderQuestion { get; set; }
        public int OrderQuestionId { get; set; }
        public string Text { get; set; }
    }
}