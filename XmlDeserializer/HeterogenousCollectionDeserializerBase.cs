using BToolbox.XmlDeserializer.Context;
using BToolbox.XmlDeserializer.Exceptions;
using BToolbox.XmlDeserializer.Relations;
using System.Xml;

namespace BToolbox.XmlDeserializer;

public abstract class HeterogenousCollectionDeserializerBase<TCollection, TElementBase, TEnvironment> :
    CollectionDeserializerBase<TCollection, TElementBase, TEnvironment>
{

    public override string ElementName { get; }

    public HeterogenousCollectionDeserializerBase(string elementName)
        => ElementName = elementName;

    protected override bool parseChildNode(XmlNode xmlNode, DeserializationContext context, out TElementBase element, out IRelationBuilder<TEnvironment> relationBuilder, object parent)
    {
        if (ignoredElements.Contains(xmlNode.LocalName))
        {
            relationBuilder = null;
            element = default;
            return false;
        }
        if (!registrations.TryGetValue(xmlNode.LocalName, out IDeserializer<TElementBase, TEnvironment> deserializer))
            throw new UnexpectedElementNameException(xmlNode, getExpectedElementNames());
        element = deserializer.Parse(xmlNode, context, out relationBuilder, parent);
        return true;
    }

    private readonly Dictionary<string, IDeserializer<TElementBase, TEnvironment>> registrations = new();
    private readonly List<string> ignoredElements = new();

    public void Register(IDeserializer<TElementBase, TEnvironment> deserializer)
        => registrations.Add(deserializer.ElementName, deserializer);

    public void AddIgnoredElement(string ignoredElement)
        => ignoredElements.Add(ignoredElement);

    private string[] getExpectedElementNames() => registrations.Select(r => r.Value.ElementName).ToArray();

}
