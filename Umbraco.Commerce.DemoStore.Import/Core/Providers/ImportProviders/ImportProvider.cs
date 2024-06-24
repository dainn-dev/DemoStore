using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;
using Umbraco.Commerce.DemoStore.Import.Core.Helpers;
using Umbraco.Commerce.DemoStore.Import.Core.Notifications;
using Umbraco.Commerce.DemoStore.Import.Core.State;

namespace Umbraco.Commerce.DemoStore.Import.Core.Providers.ImportProviders
{
    public abstract class ImportProvider
    {
        private ILogger _logger;

        private IEventAggregator _eventAggregator;

        public string StatusMessage { get; protected set; }

        public string Alias { get; protected set; }

        public string Name { get; protected set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public int SortOrder { get; private set; }

        public string ContentType { get; private set; }

        public bool ChildImportOnly { get; private set; }

        public string SupportedChild { get; private set; }

        public string Icon { get; private set; }

        public bool DeleteOldRecords { get; set; }

        public virtual bool IsSupported => true;

        public bool CancelImport { get; set; }

        protected ImportProvider()
        {
            ImportProviderAttribute importProviderAttribute = AttributeHelper.GetAttributes<ImportProviderAttribute>(this, attributeRequired: true).FirstOrDefault();
            if (importProviderAttribute != null)
            {
                Alias = importProviderAttribute.Alias;
                Name = importProviderAttribute.Name;
                Icon = importProviderAttribute.Icon;
                SortOrder = importProviderAttribute.SortOrder;
                ContentType = importProviderAttribute.ContentType;
                ChildImportOnly = importProviderAttribute.ChildImportOnly;
                SupportedChild = importProviderAttribute.SupportedChild;
                Title = importProviderAttribute.Title;
                Intro = importProviderAttribute.Intro;
            }
        }

        protected virtual void Initialize(ILogger logger, IEventAggregator eventAggregator)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        public abstract ImportResult Import(ImportState state);

        public abstract ImportDefinition GetDefinition(IProviderOptions options);

        public virtual IProviderOptions CopyParentOptions(IProviderOptions parentProvider)
        {
            return GetImportProviderOptions();
        }

        protected void FireImporting(ImportState state)
        {
            try
            {
                _eventAggregator.Publish(new ImportingNotification(state));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
        }

        protected void FireImported(ImportState state)
        {
            try
            {
                _eventAggregator.Publish(new ImportedNotification(state));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
        }

        public void FireBulkImporting(ImportState state)
        {
            try
            {
                _eventAggregator.Publish(new BulkImportingNotification(state));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
        }

        public void FireBulkImported(ImportState state)
        {
            try
            {
                _eventAggregator.Publish(new BulkImportingNotification(state));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
        }

        protected void FireRecordSkipped<T>(T item, ImportState state, object primaryKeyValue, Hashtable items) where T : IContentBase
        {
            try
            {
                _eventAggregator.Publish(new RecordSkippedNotification<T>(item, state, primaryKeyValue, items));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
        }

        protected void FireRecordImported<T>(T item, ImportState state, object primaryKeyValue, Hashtable items) where T : IContentBase
        {
            try
            {
                _eventAggregator.Publish(new RecordImportedNotification<T>(item, state, primaryKeyValue, items));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
        }

        protected RecordImportingEventResult FireRecordImporting<T>(T sender, ImportState state, object primaryKeyValue, Hashtable items) where T : IContentBase
        {
            RecordImportingEventResult recordImportingEventResult = new RecordImportingEventResult();
            try
            {
                EventMessages eventMessages = new EventMessages();
                RecordImportingNotification<T> notification = new RecordImportingNotification<T>(sender, state, primaryKeyValue, items, eventMessages);
                recordImportingEventResult.Cancel = _eventAggregator.PublishCancelable(notification);
                recordImportingEventResult.CancelMessage = (from s in eventMessages.GetAll()
                                                            select s.Message).ToList().ToCsv();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "import for Import provider {0}: Error in third party event handler", state.SelectedDataProviderOptions.Alias);
            }
            return recordImportingEventResult;
        }

        protected Hashtable GetOriginalDataForEventArgs(IDataReader reader)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                hashtable.Add(reader.GetName(i), reader.GetValue(i));
            }
            return hashtable;
        }

        public abstract IProviderOptions GetImportProviderOptions();

        public abstract IEnumerable<string> ValidateProviderOptions(IProviderOptions importProvideroptions);

        public virtual IEnumerable<string> ValidateMappingOptions(IEnumerable<ImportMapping> mapping)
        {
            return new List<string>();
        }

        public virtual void SetDataProviderOptions(IProviderOptions importProvideroptions, IProviderOptions dataproviderOptions)
        {
        }

        public virtual bool DefinitionChanged(IProviderOptions importProvideroptions, string definitionAlias)
        {
            return false;
        }

        public virtual IProviderOptions UpdateProviderOptions(IProviderOptions importProvideroptions)
        {
            return importProvideroptions;
        }
    }
}
