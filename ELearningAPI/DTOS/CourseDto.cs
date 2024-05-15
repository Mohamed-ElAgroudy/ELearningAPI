namespace ELearningAPI.DTOS
{
    public class CourseDto
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}
