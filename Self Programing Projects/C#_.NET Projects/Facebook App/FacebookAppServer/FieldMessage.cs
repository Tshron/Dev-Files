using System;
using System.Xml.Serialization;

namespace FacebookAppServer
{
     [Serializable]
     [XmlType(TypeName = "FieldMessage")]
     public struct FieldMessage<K, V>
     {
        public FieldMessage(K v1, V v2) : this()
        {
            Key = v1;
            Value = v2;
        }

        public K Key { get; set; }

        public V Value { get; set; }
     }
}
