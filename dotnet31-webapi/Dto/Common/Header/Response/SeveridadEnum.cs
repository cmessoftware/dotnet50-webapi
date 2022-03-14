namespace cmes_webapi.Api.Dto
{
    public enum SeveridadEnum
    {
        OK,
        /// <summary>
        /// Informativo se usa para mostrar situaciones de funcionamiento normal
        /// </summary>
        INFO,
        /// <summary>
        /// Adevertencia se usa cuando el proceso pudo completarse aunque hubo ciertas anomalías 
        /// (ej. asumir defaults por falta de configuración, o exceso en tiempo de proceso)
        /// </summary>
        AVISO,
        /// <summary>
        /// Error se usa cuando un proceso individual no pudo completarse pero la aplicación continúa funcionando
        /// (ej. argumentos en un pedido, o error de acceso a la base de datos)
        /// </summary>
        ERROR,
        /// <summary>
        /// Fatal se usa cuando la aplicación no puede continuar procesando requerimientos
        /// (ej. error permanente de conexión a base de datos, o falta de configuración necesaria para iniciar)
        /// </summary>
        FATAL,
        DEBUG
    }

}