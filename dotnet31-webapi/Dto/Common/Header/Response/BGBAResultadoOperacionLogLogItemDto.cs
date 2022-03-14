using System;

namespace cmes_webapi.Api.Dto
{
    [Serializable]
	public class BGBAResultadoOperacionLogLogItemDto
    {
        /// <comentarios/>
        public severidad2 Severidad { get; set; }
     
        /// <comentarios/>
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string URLDetalle { get; set; }
        
    }

    public enum severidad2
    {

        /// <comentarios/>
        INFO,

        /// <comentarios/>
        WARNING,

        /// <comentarios/>
        ERROR,
    }

}
