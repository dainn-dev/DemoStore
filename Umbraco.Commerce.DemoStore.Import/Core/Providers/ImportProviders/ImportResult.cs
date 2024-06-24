namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders
{
    public class ImportResult
    {
        public DateTime Duration { get; set; }

        public bool Canceled { get; set; }

        public int RecordsInDataSource { get; set; }

        public int RecordsAdded { get; set; }

        public int RecordsUpdated { get; set; }

        public int RecordsSkipped { get; set; }

        public int RecordsDeleted { get; set; }

        public List<string> Errors { get; set; }

        public ImportResult()
        {
            Errors = new List<string>();
        }
    }
}
