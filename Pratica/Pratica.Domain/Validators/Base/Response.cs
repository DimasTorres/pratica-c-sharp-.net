namespace Pratica.Domain.Validators.Base
{
    public class Response
    {
        public Response()
        {
            ReportErrors = new List<ReportError>();
        }

        public Response(List<ReportError> reportErrors)
        {
            ReportErrors = reportErrors;
        }

        public Response(ReportError reportError) : this(new List<ReportError> { reportError })
        {

        }

        public List<ReportError> ReportErrors { get; }
        public static Response<T> OK<T>(T data) => new Response<T>(data);
        public static Response OK() => new Response();
        public static Response Unprocessable(List<ReportError> reportErrors) => new Response(reportErrors);
        public static Response Unprocessable(ReportError reportError) => new Response(reportError);
    }

    public class Response<T> : Response
    {
        public Response() { }
        public Response(T data, List<ReportError> reportErrors = null) : base(reportErrors)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
