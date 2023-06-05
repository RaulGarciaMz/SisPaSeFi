namespace Domain.DTOs
{
/// <summary>
/// Uso Vehiculo para agregar (create)
/// </summary>
public class UsoVehiculoDtoForCreateOrUpdate
    {
        /// <summary>
        /// Identificador del programa de patrullaje
        /// </summary>
        public int intIdPrograma { get; set; }
        /// <summary>
        /// Identificador del vehículo
        /// </summary>
        public int intIdVehiculo { get; set; }
        /// <summary>
        /// Identificador del usuario del vehículo
        /// </summary>
        public int intIdUsuarioVehiculo { get; set; }
        /// <summary>
        /// Cantidad de kilómetros iniciales del vehículo
        /// </summary>
        public int intKmInicio { get; set; }
        /// <summary>
        /// Cantidad de kilómetros del vehículo al final del patrullaje
        /// </summary>
        public int intKmFin { get; set; }
        /// <summary>
        /// Cantidad de combustible consumido en el patrullaje
        /// </summary>
        public int intConsumoCombustible { get; set; }
        /// <summary>
        /// Descripción del estado del vehículo
        /// </summary>
        public string? strEstadoVehiculo { get; set; }
        /// <summary>
        /// Nombre del usuario (alias o usuario_nom) que realiza la operación
        /// </summary>
        public string Usuario { get; set; }
    }

    public class UsoVehiculoDtoGet
    {
        public int intIdUsoVehiculo { get; set; }
        public int intIdPrograma { get; set; }
        public int intIdVehiculo { get; set; }
        public int intIdUsuarioVehiculo { get; set; }
        public int intKmInicio { get; set; }
        public int intKmFin { get; set; }
        public int intConsumoCombustible { get; set; }
        public string? strEstadoVehiculo { get; set; }
        public string? strNumeroEconomico { get; set; }
        public string strMatricula { get; set; }
    }
}
