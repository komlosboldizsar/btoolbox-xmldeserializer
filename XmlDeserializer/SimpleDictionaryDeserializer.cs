using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer;

public class SimpleDictionaryDeserializer<TKey, TElement, TEnvironment> : SimpleCollectionDeserializerBase<Dictionary<TKey, TElement>, TElement, TEnvironment>
{

    private readonly Func<TElement, XmlNode, DeserializationContext, TKey> getKey;

    public SimpleDictionaryDeserializer(string elementName, IDeserializer<TElement, TEnvironment> elementDeserializer, Func<TElement, XmlNode, DeserializationContext, TKey> getKey)
        : base(elementName, elementDeserializer)
        => this.getKey = getKey;

    protected override Dictionary<TKey, TElement> createCollection(XmlNode xmlNode, DeserializationContext context, object parent)
        => new();

    protected override void addElementToCollection(Dictionary<TKey, TElement> collection, TElement element, XmlNode elementNode, DeserializationContext context)
        => collection.Add(getKey(element, elementNode, context), element);

}
