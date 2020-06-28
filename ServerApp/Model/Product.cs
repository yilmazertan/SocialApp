namespace ServerApp.Model
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public bool isactive { get; set; }
        public string secret { get; set; }
    }
}