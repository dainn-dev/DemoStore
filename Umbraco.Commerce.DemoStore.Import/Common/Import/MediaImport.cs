using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Commerce.DemoStore.Import.Common.Config;
using Umbraco.Commerce.DemoStore.Import.Common.Extensions;
using Umbraco.Commerce.DemoStore.Import.Common.Model.DTO;
using Umbraco.Commerce.DemoStore.Import.Core.Extensions;
using Umbraco.Commerce.DemoStore.Import.Core.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Models.Import;
using Umbraco.Commerce.DemoStore.Import.Core.Providers;

namespace Umbraco.Commerce.DemoStore.Import.Common.Import
{
    public class MediaImport : IMediaImport
    {
        private readonly IScopeProvider _scopeProvider;

        private readonly ILogger<MediaImport> _logging;

        private readonly IMediaService _mediaService;

        private readonly ContentSettings _contentSettings;

        private readonly CmsImportConfig _config;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Helpers.Definition.IIOHelper _ioHelper;

        private readonly MediaFileManager _mediaFileManager;

        public MediaImport(IScopeProvider scopeProvider, 
            ILogger<MediaImport> logger, 
            IMediaService mediaService, 
            IOptions<ContentSettings> contentSettings, 
            IHttpContextAccessor httpContextAccessor, 
            IOptions<CmsImportConfig> config, 
            Helpers.Definition.IIOHelper ioHelper, 
            MediaFileManager mediaFileManager)
        {
            _scopeProvider = scopeProvider;
            _logging = logger;
            _mediaService = mediaService;
            _contentSettings = contentSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _config = config.GetValueWithDefaults();
            _ioHelper = ioHelper;
            _mediaFileManager = mediaFileManager;
        }

        public IEnumerable<Guid> Import(string relativeUrl, Guid imageParentId, Guid fileParentId, int importUserId, bool breakWhenFileNotExists, IProviderOptions dataProviderOptions = null)
        {
            List<Guid> list = new List<Guid>();
            foreach (string item in ParseUrls(relativeUrl))
            {
                string extension = GetExtension(item);
                string text = _ioHelper.MapPathWebRoot(_config.MediaConfig.MediaImportLocation + "/" + item);
                MediaRelationInfo relatedMediaItem = GetRelatedMediaItem(item);
                if (System.IO.File.Exists(text))
                {
                    long length = new FileInfo(text).Length;
                    if (relatedMediaItem == null)
                    {
                        string mediaType = (_contentSettings.Imaging.ImageFileTypes.Contains(extension) ? _config.MediaConfig.MediaImportImageTypeAlias : _config.MediaConfig.MediaImportFileTypeAlias);
                        Guid? guid = ImportFile(item, fileParentId, importUserId, text, length, mediaType);
                        if (guid.HasValue)
                        {
                            list.Add(guid.Value);
                        }
                    }
                    else
                    {
                        if (relatedMediaItem.ByteSize != length)
                        {
                            UpdateFile(item, fileParentId, importUserId, text, length, relatedMediaItem.UmbracoId);
                        }
                        list.Add(relatedMediaItem.UmbracoId);
                    }
                }
                else
                {
                    if (breakWhenFileNotExists)
                    {
                        throw new FileNotFoundException("file '" + text + "' does not exists on disk");
                    }
                    _logging.LogDebug("file '" + text + "' does not exists on disk");
                }
            }
            return list;
        }

        public MediaRelationInfo GetRelatedMediaItem(string relativeUrl)
        {
            MediaRelationInfo result = null;
            using IScope scope = _scopeProvider.CreateScope();
            CMSImportMediaRelationDTO cMSImportMediaRelationDTO = scope.Database.FirstOrDefault<CMSImportMediaRelationDTO>("Select * From CMSImportMediaRelation where SourceUrl = @0", new object[1] { relativeUrl });
            if (cMSImportMediaRelationDTO != null)
            {
                result = new MediaRelationInfo
                {
                    ByteSize = cMSImportMediaRelationDTO.ByteSize,
                    SourceUrl = cMSImportMediaRelationDTO.SourceUrl,
                    UmbracoId = cMSImportMediaRelationDTO.UmbracoMediaId
                };
            }
            scope.Complete();
            return result;
        }

        public void DeleteRelation(Guid umbracoId)
        {
            _logging.LogDebug($"Deleting Media Relation for umbraco Id {umbracoId}");
            using IScope scope = _scopeProvider.CreateScope();
            scope.Database.Execute("Delete From CMSImportMediaRelation where UmbracoMediaId = @0", umbracoId);
            scope.Complete();
        }

        private Guid? ImportFile(string relativeUrl, Guid parentId, int importUserId, string filePath, long byteSize, string mediaType)
        {
            IMedia byId = _mediaService.GetById(parentId);
            _logging.LogDebug($"Importing media item  '{relativeUrl}' on parent '{parentId}' with media type '{mediaType}'");
            Guid? guid = (_config.MediaConfig.MediaImportKeepFolderStructure ? EnsureFolderStructure(relativeUrl, byId, importUserId) : byId?.Key);
            if (guid.HasValue)
            {
                string fileName = Path.GetFileName(filePath);
                IMedia media = _mediaService.CreateMedia(fileName, guid.Value, mediaType, importUserId);
                media.SetFile(_mediaFileManager, filePath);
                _mediaService.Save(media);
                SaveMediaRelation(new MediaRelationInfo
                {
                    UmbracoId = media.Key,
                    SourceUrl = relativeUrl,
                    ByteSize = byteSize
                });
                return media.Key;
            }
            return null;
        }

        public void UpdateFile(string relativeUrl, Guid parentId, int importUserId, string filePath, long byteSize, Guid umbracoId)
        {
            string fileName = Path.GetFileName(filePath);
            IMedia byId = _mediaService.GetById(umbracoId);
            byId.SetFile(_mediaFileManager, filePath, fileName);
            _mediaService.Save(byId);
            SaveMediaRelation(new MediaRelationInfo
            {
                UmbracoId = byId.Key,
                SourceUrl = relativeUrl,
                ByteSize = byteSize
            });
        }

        private Guid? EnsureFolderStructure(string relativeUrl, IMedia parent, int importUserId)
        {
            IMedia media = parent;
            List<string> list = relativeUrl.ToList('/');
            foreach (string item in list)
            {
                if (!list.IsLast(item))
                {
                    media = GetOrCreateFolder(item, media, importUserId);
                }
            }
            return media?.Key;
        }

        private IMedia GetOrCreateFolder(string folderName, IMedia parent, int importUserId)
        {
            long totalRecords;
            IMedia media = _mediaService.GetPagedChildren(parent.Id, 0L, 10000, out totalRecords).FirstOrDefault((IMedia c) => c.Name.Equals(folderName, StringComparison.InvariantCultureIgnoreCase));
            if (media == null)
            {
                media = _mediaService.CreateMedia(folderName, parent.Key, _config.MediaConfig.MediaImportFolderTypeAlias, importUserId);
                _mediaService.Save(media, importUserId);
            }
            return media;
        }

        public string FormatRelativeUrl(string url)
        {
            url = ReplaceMediaFolder(url);
            if (url.Contains("?"))
            {
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!url.StartsWith("/"))
            {
                url = $"/{url}";
            }
            _ = _httpContextAccessor.HttpContext.Request;
            return url;
        }

        private string ReplaceMediaFolder(string url)
        {
            if (_config.MediaConfig.MediaImportLocation != "/" && _config.MediaConfig.MediaImportLocation != "~/")
            {
                url = url.Replace(_config.MediaConfig.MediaImportLocation, string.Empty);
            }
            url = RemoveDoubleSlash(url);
            if (!url.StartsWith("/"))
            {
                return $"/{url}";
            }
            return url;
        }

        private string RemoveDoubleSlash(string url)
        {
            return url.Replace("//", "/");
        }

        private string GetExtension(string url)
        {
            try
            {
                return Path.GetExtension(url)?.Remove(0, 1).ToLower();
            }
            catch (Exception innerException)
            {
                throw new ArgumentException("Getting extension for media import  from url '" + url + "' failed.", innerException);
            }
        }

        private void SaveMediaRelation(MediaRelationInfo mediaRelationInfo)
        {
            using IScope scope = _scopeProvider.CreateScope();
            CMSImportMediaRelationDTO cMSImportMediaRelationDTO = scope.Database.ExecuteScalar<CMSImportMediaRelationDTO>("Select * From CMSImportMediaRelation where SourceUrl = @0", new object[1] { mediaRelationInfo.SourceUrl }) ?? new CMSImportMediaRelationDTO();
            cMSImportMediaRelationDTO.ByteSize = mediaRelationInfo.ByteSize;
            cMSImportMediaRelationDTO.SourceUrl = mediaRelationInfo.SourceUrl;
            cMSImportMediaRelationDTO.UmbracoMediaId = mediaRelationInfo.UmbracoId;
            if (cMSImportMediaRelationDTO.Id > 0)
            {
                scope.Database.Update(cMSImportMediaRelationDTO);
            }
            else
            {
                scope.Database.Insert(cMSImportMediaRelationDTO);
            }
            scope.Complete();
        }

        private IEnumerable<string> ParseUrls(string relativeUrls)
        {
            List<string> list = new List<string>();
            foreach (string item in relativeUrls.ToList())
            {
                string text = FormatRelativeUrl(item);
                string text2 = _ioHelper.MapPath(text);
                try
                {
                    if ((System.IO.File.GetAttributes(text2) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        list.AddRange(ParseFolder(text2, text));
                    }
                    else
                    {
                        list.Add(text);
                    }
                }
                catch
                {
                    list.Add(text);
                }
            }
            return list;
        }

        private IEnumerable<string> ParseFolder(string folderpath, string relativeUrl)
        {
            List<string> list = new List<string>();
            string[] files = Directory.GetFiles(folderpath);
            foreach (string path in files)
            {
                list.Add(relativeUrl + "/" + Path.GetFileName(path));
            }
            return list;
        }
    }
}
