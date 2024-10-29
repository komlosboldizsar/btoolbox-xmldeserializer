using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer;

public abstract class SimpleCollectionDeserializer<TCollection, TElement, TEnvironment> :
    SimpleCollectionDeserializerBase<TCollection, TElement, TEnvironment>
    where TCollection : ICollection<TElement>
{

    public SimpleCollectionDeserializer(string elementName, IDeserializer<TElement, TEnvironment> elementDeserializer)
        : base(elementName, elementDeserializer) { }

    protected override void addElementToCollection(TCollection collection, TElement element, XmlNode elementNode, DeserializationContext context)
        => collection.Add(element);

}
