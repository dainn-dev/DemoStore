using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Commerce.DemoStore.Import.Core.Import;

namespace Umbraco.Commerce.DemoStore.Import.Common.Events
{
    public class UmbracoCMSActionsNotificationHandler : INotificationHandler<ContentDeletingNotification>, INotificationHandler, INotificationHandler<ContentMovedToRecycleBinNotification>, INotificationHandler<MediaDeletingNotification>, INotificationHandler<MediaMovedToRecycleBinNotification>, INotificationHandler<MemberDeletingNotification>
    {
        private readonly IImportRelationRepository _importRelationRepository;

        private readonly IMediaImport _mediaImport;

        private readonly ILogger<UmbracoCMSActionsNotificationHandler> _logger;

        public UmbracoCMSActionsNotificationHandler(IImportRelationRepository importRelationRepository, IMediaImport mediaImport, ILogger<UmbracoCMSActionsNotificationHandler> logger)
        {
            _importRelationRepository = importRelationRepository;
            _mediaImport = mediaImport;
            _logger = logger;
        }

        public void Handle(ContentDeletingNotification notification)
        {
            try
            {
                foreach (IContent deletedEntity in notification.DeletedEntities)
                {
                    _logger.LogDebug($"Content item deleted, delete reference for content item {deletedEntity.Id}");
                    _importRelationRepository.Delete(deletedEntity.Key);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Deleting Content reference");
            }
        }

        public void Handle(ContentMovedToRecycleBinNotification notification)
        {
            try
            {
                foreach (MoveEventInfo<IContent> item in notification.MoveInfoCollection)
                {
                    _logger.LogDebug($"Content item thrashed, delete reference for content item {item.Entity.Id}");
                    _importRelationRepository.Delete(item.Entity.Key);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Deleting Content reference");
            }
        }

        public void Handle(MediaDeletingNotification notification)
        {
            try
            {
                foreach (IMedia deletedEntity in notification.DeletedEntities)
                {
                    _logger.LogDebug($"Media item is deleted, delete reference for content item {deletedEntity.Id}");
                    _mediaImport.DeleteRelation(deletedEntity.Key);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Deleting Media reference");
            }
        }

        public void Handle(MediaMovedToRecycleBinNotification notification)
        {
            try
            {
                foreach (MoveEventInfo<IMedia> item in notification.MoveInfoCollection)
                {
                    _logger.LogDebug($"Media item is thrashed, delete reference for content item {item.Entity.Id}");
                    _mediaImport.DeleteRelation(item.Entity.Key);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Deleting Media reference");
            }
        }

        public void Handle(MemberDeletingNotification notification)
        {
            try
            {
                foreach (IMember deletedEntity in notification.DeletedEntities)
                {
                    _logger.LogDebug($"member item is thrashed, delete reference for  item {deletedEntity.Id}");
                    _importRelationRepository.Delete(deletedEntity.Key);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Deleting Member reference");
            }
        }
    }
}
