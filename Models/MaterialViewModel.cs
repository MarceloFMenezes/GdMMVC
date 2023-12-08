using GdMMVC.Models.Enum;

namespace GdMMVC.Models
{
    public class MaterialViewModel
    {
       public int Id {get; set;}
        public string Nome {get; set;}
        public string Marca {get; set;}
        public string? Cor {get; set;}
        public TipoEnum Tipo {get; set;}
        public int Quantidade {get; set;}
    }
}