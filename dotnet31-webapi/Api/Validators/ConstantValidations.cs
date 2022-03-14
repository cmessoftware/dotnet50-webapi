using cmes_webapi.Common.Helpers;

namespace cmes_webapi.Common
{

    public static class ConstantValidations
    {
        public const string ProviderName = "paas_inpo_calypsops_template";
        public const int SuccessfulOperation = (int)SeveridadEnum.OK;
        public const SeveridadEnum SeverityError = SeveridadEnum.ERROR;
        public const SeveridadEnum SeverityInfo = SeveridadEnum.INFO;
        public const string CurrencyARS = "ARS";
        public const string CurrencyUSD = "USD";
        public const string SuccessfulOperationInfo = "INFO";

        public const string NumberOrderNew = "BGBAINOMN";
        public const string NumberOrderCancel = "BGBAINOMC";

        /// <summary>
        /// Constante de error general.
        /// </summary>
        public const string InternalServicesError = "No se pudo concretar la operación.";


    }
}
