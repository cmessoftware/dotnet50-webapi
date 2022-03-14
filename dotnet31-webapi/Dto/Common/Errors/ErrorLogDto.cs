namespace cmes_webapi.Api.Dto
{
    public class ErrorLogDto
    {
        public int ErrorLogID { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorExceptionMessage { get; set; }

        public string ProcedureName { get; set; }
        public string ComponentTarget { get; set; }

        public string When { get; set; }

    }
}
