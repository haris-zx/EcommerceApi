namespace DummyProject.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        ICollection<Product> products { get; set; }
    }
}
