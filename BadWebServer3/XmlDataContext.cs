using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer
{
    public class XmlDataContext
    {

        private readonly string filename = "data.xml";

        public List<string> Items { get; private set; }


        public XmlDataContext()
        {
            if (System.IO.File.Exists(filename))
            {

                using (var stream = System.IO.File.OpenRead(filename))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    this.Items = (List<string>)serializer.Deserialize(stream);

                }
            }
            else
            {
                this.Items = new List<string>();
            }
        }

        public void SaveChanges()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
            using (var stream = System.IO.File.Exists(filename) ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
            {
                stream.SetLength(0);
                stream.Position = 0;
                serializer.Serialize(stream, Items);
            }
        }
    }
}
