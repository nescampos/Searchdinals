namespace Searchdinals.Models
{
    public class SearchResult
    {
        public string status { get; set; }
        public string count { get; set; }
        public List<SearchResultData> results { get; set; }
    }

    public class SearchResultData
    {
        public string txid { get; set; }
        public string inputindex { get; set; }
        public string inscriptionid { get; set; }
        public string inscriptionnumber { get; set; }
        public string blockheight { get; set; }
        public string contentstr { get; set; }
        public string contenttypestr { get; set; }
        public string contenthash { get; set; }
        public string contentlength { get; set; }
        public DateTime createdat { get; set; }
    }
}
