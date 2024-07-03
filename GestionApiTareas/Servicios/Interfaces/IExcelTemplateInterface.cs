namespace GestionApiTareas.Servicios.Interfaces
{
    public interface IExcelTemplateInterface
    {
        public string BuilRutadAsync();
        //public Task<string> BuilRutadAsync();
        public string FindPath(string dir, string fileName);
        string FindPath(string dir);
    }
}
