namespace GestionApiTareas.ViewModel
{
    public class PermisosViewModel
    {
        public string nombrePantalla { get; set; }

        public string nombreSeccion { get; set; }

        public string urlSeccion { get; set; }

        public string iconoPantalla { get; set; }


    }

    public class DataDashboard
    {
        public int montajesRealizados { get; set; }

        public int vehiculosDisponibles { get; set; }

        public int llantasTotales { get; set; }

        public int pedidosRealizados { get; set; }
    }

    public class DiasDeSemanaData
    {
        public int numeroDia { get; set; }
        public int dataPorDia { get; set; }
    }

    public class DataIdDashboar
    {
        public long ids { get; set; }

        public DateTime fechaRegistro { get; set; }
    }

    public class DataChartPorDiaDashboard
    {

       
        public string codigo { get; set; }
        public string placa { get; set; }
        public string tipoVehiculo { get; set; }

        public string fechaMontaje { get; set; }
        public long? kmMontaje { get; set; }

        public List<NumerosTotalesDetalleMontaje> listaNumerosTotalesDetalleMontaje { get; set; }

    }

    public class NumerosTotalesDetalleMontaje
    {

        public string descripcion { get; set; }
        public int numeroGeneral { get; set; }


    }


    public class DataLlantaDashboard
    {
        public string termico { get; set; }
        public string placaAsignacion { get; set; }

        public string estadoLlanta { get; set; }

        public List<ListaDetalleLlantaInfo> listaDetalleLlantaDashboard { get; set; }
    }

    public class DetalleLlantaDashboard
    {
        public List<ListaDetalleLlantaInfo> listaListaDetalleLLantaInfo { get; set; }

    }

    public class ListaDetalleLlantaInfo
    {

        public string descripcion { get; set; }
        public string dataDescripcion { get; set; }
    }



    public class DataPedidoDashboard
    {
        public string codigoPedido { get; set; }
        public string enviadoPedido { get; set; }
        public string tipoSolicitud { get; set; }
        public string tipoPedido { get; set; }
        public string motivoPedido { get; set; }

        public List<DetallePedidoDashboard> listaDetallePedidoDashboard { get; set; }
    }

    public class DetallePedidoDashboard
    {
        public List<ListaDetallePedidoInfo> listaListaDetallePedidoInfo { get; set; }

    }

    public class ListaDetallePedidoInfo
    {

        public string descripcion { get; set; }
        public string dataDescripcion { get; set; }
    }



}
