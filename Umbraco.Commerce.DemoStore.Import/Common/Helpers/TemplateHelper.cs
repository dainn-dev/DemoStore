using System.Text;

namespace Umbraco.Commerce.DemoStore.Import.Common.Helpers
{
    public class TemplateHelper
    {
        public string ReadTemplate(string fileLocation)
        {
            return File.ReadAllText(fileLocation);
        }

        public void SaveTemplate(string fileLocation, string content)
        {
            File.WriteAllText(fileLocation, content, Encoding.UTF8);
        }
    }
}
