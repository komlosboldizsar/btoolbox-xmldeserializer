using BToolbox.XmlDeserializer.Context;
using BToolbox.XmlDeserializer.Relations;
using System.Xml;

namespace BToolbox.XmlDeserializer;

public abstract class SimpleCollectionDeserializerBase<TCollection, TElement, TEnvironment> :
    CollectionDeserializerBase<TCollection, TElement, TEnvironment>
{

    public override string ElementName { get; }

    private readonly IDeserializer<TElement, TEnvironment> elementDeserializer;

    public SimpleCollectionDeserializerBase(string elementName, IDeserializer<TElement, TEnvironment> elementDeserializer)
    {
        ElementName = elementName;
        this.elementDeserializer = elementDeserializer;
    }

    protected override bool parseChildNode(XmlNode xmlNode, DeserializationContext context, out TElement element, out IRelationBuilder<TEnvironment> relationBuilder, object parent)
    {
        element = elementDeserializer.Parse(xmlNode, context, out relationBuilder, parent);
        return true;
    }

}
