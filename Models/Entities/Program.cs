namespace Models.Entities;
public class Program
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = null!;
    // public ICollection<Class> Classes { get; set; }
}