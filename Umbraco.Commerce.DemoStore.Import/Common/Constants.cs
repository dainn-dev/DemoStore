namespace Umbraco.Commerce.DemoStore.Import.Common
{
    public class Constants
    {
        public class Icons
        {
            public const string WizardIcon = "icon-wand";

            public const string WizardMenuIcon = "wand";

            public const string ScheduleMenuIcon = "calendar";

            public const string ScheduleLogMenuIcon = "file-cabinet";

            public const string PlayMenuIcon = "play";

            public const string FolderIcon = "icon-folder";

            public const string CalendarIcon = "icon-calendar";

            public const string SettingsIcon = "icon-settings";

            public const string ImageIcon = "icon-picture";

            public const string NotepadIcon = "icon-notepad";

            public const string EmailIcon = "icon-message";
        }

        public class FileLocations
        {
            public const string CMSImportTempFolder = "~/umbraco/Data/TEMP/cmsimport/";
        }

        public static class FixedProperties
        {
            public static class Content
            {
                public const string Name = "@@#Import_name";

                public const string CreateDate = "@@#Import_createDate";

                public const string PublishAt = "@@#Import_publishAt";

                public const string UnpublishAt = "@@#Import_unpublishAt";
            }

            public static class Members
            {
                public const string Name = "@@#Import_name";

                public const string Login = "@@#Import_login";

                public const string Password = "@@#Import_password";

                public const string Email = "@@#Import_email";

                public const string CreateDate = "@@#Import_createDate";

                public const string IsApproved = "@@#Import_approved";
            }

            public const string Prefix = "@@#Import_";

            public const string DefaultPropertyAlias = "__DefaultPropertyAlias__";

            public const string TextPropertyAlias = "Umbraco.TextBox";

            public const string DateTimePropertyAlias = "Umbraco.DateTime";

            public const string MediaPropertyAlias = "Umbraco.MediaPicker";

            public const string NewMediaPickerPropertyAlias = "Umbraco.MediaPicker3";

            public const string MNTPPropertyAlias = "Umbraco.MultiNodeTreePicker";

            public const string RichTextPropertyAlias = "Umbraco.TinyMCE";

            public const string BooleanPropertyAlias = "Umbraco.Boolean";
        }

        public const int AmountOfRecordsInTrial = 500;
    }

}
