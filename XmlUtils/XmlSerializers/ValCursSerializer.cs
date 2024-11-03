using SolidTA.XmlUtils.XmlModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolidTA.XmlUtils.XmlSerializers
{
    public class ValCursSerializer
    {
        private ValCursSerializer() { }
        public static ValCurs Deserialize(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
            ValCurs valCurs = new ValCurs();
            using (StringReader reader = new StringReader(xmlString))
            {
                valCurs = (ValCurs) serializer.Deserialize(reader);
            }
            return valCurs;
        }
       
    }
}
