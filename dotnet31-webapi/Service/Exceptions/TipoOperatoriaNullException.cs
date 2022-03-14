using System;

namespace cmes_webapi.Services
{
    public class TipoOperatoriaNullException : TipoOperatoriaException
    {
        public TipoOperatoriaNullException() : base("TipoOperatoria is Null") { }
    }
}
