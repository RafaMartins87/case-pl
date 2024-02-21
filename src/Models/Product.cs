namespace Catalog.API.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string CodigoBarras { get; set; }
        public string NomeProduto { get; set; } 
        public string CategoriaProduto { get; set; } 
        public string Valor { get; set; }


    }
}
