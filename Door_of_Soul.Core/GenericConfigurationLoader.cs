using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Door_of_Soul.Core
{
    public static class GenericConfigurationLoader<TConfiguration> where TConfiguration : class, new()
    {
        public static bool Load(string filePath, out TConfiguration configuration)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TConfiguration));
            if (File.Exists(filePath))
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        configuration = (TConfiguration)serializer.Deserialize(reader);
                        return true;
                    }
                    else
                    {
                        configuration = null;
                        return false;
                    }
                }
            }
            else
            {
                TConfiguration defaultConfiguration = new TConfiguration();
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    serializer.Serialize(writer, defaultConfiguration);
                }
                configuration = defaultConfiguration;
                return false;
            }
        }
    }
}
