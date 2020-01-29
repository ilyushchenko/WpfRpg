using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TransferContracts
{
    public class DeserializableDataObject<TType>
    {
        public DeserializableDataObject()
        {
        }

        public DeserializableDataObject(TType type, object dataObject)
        {
            Type = type;
            Data = JsonConvert.SerializeObject(dataObject);
        }

        public TType Type { get; set; }
        public String Data { get; set; }
    }
}
