namespace WebApplication1Varuosad.Controllers
{
    public class QueryParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 30;
        public string Sort { get; set; } = "";
        public string Test { get; set; } = "";
    }
}