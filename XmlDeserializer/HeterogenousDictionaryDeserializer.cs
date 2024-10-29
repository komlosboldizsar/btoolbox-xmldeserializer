using BToolbox.XmlDeserializer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BToolbox.XmlDeserializer
{
    public class HeterogenousDictionaryDeserializer<TKey, TElementBase, TEnvironment>
        : HeterogenousCollectionDeserializerBase<Dictionary<TKey, TElementBase>, TElementBase, TEnvironment>
    {

        private readonly Func<TElementBase, XmlNode, DeserializationContext, TKey> getKey;

        public HeterogenousDictionaryDeserializer(string elementName, Func<TElementBase, XmlNode, DeserializationContext, TKey> getKey)
            : base(elementName)
            => this.getKey = getKey;

        protected override Dictionary<TKey, TElementBase> createCollection(XmlNode xmlNode, DeserializationContext context, object parent)
            => new();

        protected override void addElementToCollection(Dictionary<TKey, TElementBase> collection, TElementBase element, XmlNode elementNode, DeserializationContext context)
            => collection.Add(getKey(element, elementNode, context), element);

    }
}
