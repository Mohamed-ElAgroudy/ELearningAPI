namespace ELearningAPI.DTOS
{
    public class ShoppingCartItemDto
    {
        public string UserEmail { get; set; } = null!;

        public int CourseId { get; set; }

        public int Quantity { get; set; }
    }
}
