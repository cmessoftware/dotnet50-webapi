namespace cmes_webapi.Api.Dto
{
    public class LogItemDto
    {
        public LogItemDto() { }
        public LogItemDto(string errorCode, string errorMessage, SeveridadEnum severity)
        {
            Code = errorCode;
            Description = errorMessage;
            DetailURL = string.Empty;
            Severity = severity;
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DetailURL { get; set; }
        public SeveridadEnum Severity { get; set; }
    }
}
