using System;
using System.Runtime.Serialization;

namespace Interfaces
{
    [DataContract]
    public struct SPoint
    {
        [DataMember]
        public Int32 X { get; set; }
        [DataMember]
        public Int32 Y { get; set; }

        public SPoint(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public override String ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}