using GestionApiTareas.Servicios.Interfaces;

namespace GestionApiTareas.Servicios;


public class WebHostServices : IExcelTemplateInterface
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string Wwwroot;
    public WebHostServices(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        Wwwroot = _webHostEnvironment.WebRootPath;
    }
    public string BuilRutadAsync() => Path.Combine(Wwwroot, $"ExcelTemplate\\{DateTime.Now.ToString("yyyyMMddHHmmss")}_sellos.xlsx");
    public string FindPath(string dir, string fileName) => Path.Combine(Wwwroot, dir, $"{fileName}.xlsx");
    public string FindPath(string dir) => Path.Combine(Wwwroot, dir);
}