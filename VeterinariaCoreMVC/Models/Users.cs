namespace VeterinariaCoreMVC.Models
{
    public class Users
    {
        public int IdUser { get; set; }
        public string Nombres { get; set; } = String.Empty;
        public int IdTipoDoc { get; set; }
        public TipoDoc TipoDoc { get; set; }
        public string NroDocumento { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

    }
}
