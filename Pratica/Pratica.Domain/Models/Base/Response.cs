namespace Pratica.Domain.Models.Base
{
    public class Response
    {
        public Response()
        {
            ReportErrors = new List<ReportError>();
        }

        public Response(List<ReportError> reportErrors)
        {
            ReportErrors = reportErrors ?? new List<ReportError>();
        }

        public Response(ReportError reportError) : this(new List<ReportError> { reportError })
        {

        }

        public List<ReportError> ReportErrors { get; }
        public static Response<T> OK<T>(T data) => new Response<T>(data);
        public static Response OK() => new Response();
        public static Response Unprocessable(List<ReportError> reportErrors) => new Response(reportErrors);
        public static Response Unprocessable(ReportError reportError) => new Response(reportError);

        public static Response<T> Unprocessable<T>(List<ReportError> reportErrors)
        {
            return new Response<T>(reportErrors);
        }

        public static Response<T> Unprocessable<T>(ReportError reportError)
        {
            return new Response<T>(new List<ReportError>() { reportError });
        }
    }

    public class Response<T> : Response
    {
        public Response() { }
        public Response(List<ReportError> reportErrors) : base(reportErrors) { }
        public Response(T data, List<ReportError> reportErrors = null) : base(reportErrors)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
