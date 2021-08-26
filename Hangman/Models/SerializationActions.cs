using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hangman.ViewModels;

namespace Hangman.Models
{
    class SerializationActions
    {
        public void SerializeObject(string xmlFileName, LoginVM entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(LoginVM));
            FileStream fileStr = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }

        public LoginVM DeserializeObject(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(LoginVM));
            FileStream file = new FileStream(xmlFileName, FileMode.Open);
            var entity = xmlser.Deserialize(file);
            file.Dispose();
            return entity as LoginVM;
        }
    }
}
