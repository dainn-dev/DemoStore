using Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class ImportResultExtensions
    {
        public static ImportResult Add(this ImportResult current, ImportResult newResult)
        {
            if (newResult.Canceled)
            {
                current.Canceled = true;
            }
            current.Duration.Add(newResult.Duration.TimeOfDay);
            current.Errors.AddRange(newResult.Errors);
            current.RecordsAdded += newResult.RecordsAdded;
            current.RecordsDeleted += newResult.RecordsDeleted;
            current.RecordsInDataSource += newResult.RecordsInDataSource;
            current.RecordsSkipped += newResult.RecordsSkipped;
            current.RecordsUpdated += newResult.RecordsUpdated;
            return current;
        }
    }
}
