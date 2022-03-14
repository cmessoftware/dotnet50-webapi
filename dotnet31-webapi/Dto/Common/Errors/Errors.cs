// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

using System.Collections.Generic;

namespace cmes_webapi.Api.Dto
{
    public class Meta
    {
        public string method { get; set; }
        public string operation { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string status_code { get; set; }
        public object detail { get; set; }
        public string message { get; set; }
        public string lang { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string code_backend { get; set; }
        public string code_internal { get; set; }
        public string method_uri_path { get; set; }
        public string error_type { get; set; }
        public string data { get; set; }
        public string trace { get; set; }
    }

    //public class Root
    //{
    //    public Meta meta { get; set; }
    //    public Respuesta.Data data { get; set; }
    //    public List<Error> errors { get; set; }
    //}
}

