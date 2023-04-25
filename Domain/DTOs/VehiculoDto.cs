namespace Domain.DTOs
{
    public class VehiculoDto 
    {
        public int intIdVehiculo { get; set; }
        public int intTipoPatrullaje { get; set; }
        public string strMatricula { get; set; }
        public int? intRegionSSF { get; set; }
        public int intTipoVehiculo { get; set; }
        public string? strNumeroEconomico { get; set; }
        public int intHabilitado { get; set; }
        public string strDescripcionTipoPatrullaje { get; set; }
        public string strDescripcionTipoVehiculo { get; set; }
    }

    /// <summary>
    /// Vehículo (estructura para actualización
    /// </summary>
    public class VehiculoDtoForUpdate
    {
        /// <summary>
        /// Identificador del vehículo
        /// </summary>
        public int intIdVehiculo { get; set; }
        /// <summary>
        /// Bandera indicadora del estado del vehículo (1- Habilitado, 0 - Deshabilitado)
        /// </summary>
        public int intHabilitado { get; set; }
        /// <summary>
        /// Identificador del tipo de patrullaje
        /// </summary>
        public int intTipoPatrullaje { get; set; }
        /// <summary>
        /// Identificador del tipo de vehículo
        /// </summary>
        public int intTipoVehiculo { get; set; }
        /// <summary>
        /// Número enconómico del vehículo
        /// </summary>
        public string strNumeroEconomico { get; set; }
        /// <summary>
        /// Matrícula del vehículo
        /// </summary>
        public string strMatricula { get; set; }
        /// <summary>
        /// Nombre del usuario (alias o usuario_nom) que realiza la actualización
        /// </summary>
        public string strUsuario { get; set; }
    }

    /// <summary>
    /// Vehículo - estructura para creación
    /// </summary>
    public class VehiculoDtoForCreate
    {
        /// <summary>
        /// Bandera indicadora del estado del vehículo (1- Habilitado, 0 - Deshabilitado)
        /// </summary>
        public int intHabilitado { get; set; }
        /// <summary>
        /// Identificador del tipo de patrullaje
        /// </summary>
        public int intTipoPatrullaje { get; set; }
        /// <summary>
        /// Identificador del tipo de vehículo
        /// </summary>
        public int intTipoVehiculo { get; set; }
        /// <summary>
        /// Identificador de la comandancia regional
        /// </summary>
        public int intRegionSSF { get; set; }
        /// <summary>
        /// Número enconómico del vehículo
        /// </summary>
        public string strNumeroEconomico { get; set; }
        /// <summary>
        /// Matrícula del vehículo
        /// </summary>
        public string strMatricula { get; set; }
        /// <summary>
        /// Nombre del usuario (alias o usuario_nom) que realiza la creación
        /// </summary>
        public string strUsuario { get; set; }
    }
}
