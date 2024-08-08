namespace Pratica.Domain.Validators.Base
{
    public class ReportError
    {
        public ReportError()
        {
        }

        public ReportError(string message)
        {
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }

        public static ReportError Create(string message) => new ReportError(message);
    }
}
