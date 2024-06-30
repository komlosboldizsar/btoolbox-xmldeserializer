using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public abstract class XmlAttributeOrInnerParser<TValue, TData>
    where TData : XmlAttributeOrInnerParserData<TValue>, new()
{

    protected readonly XmlNode node;
    protected readonly string attributeName;
    protected readonly XmlAttribute attribute;
    protected readonly TData data = new();
    protected readonly DeserializationContext context;

    public XmlAttributeOrInnerParser(XmlNode node, string attributeName, TData data, DeserializationContext context)
    {
        this.node = node;
        this.attributeName = attributeName;
        if (attributeName != null)
            attribute = node.Attributes[attributeName];
        this.data = data;
        this.context = context;
    }

    public XmlAttributeOrInnerData<TValue> Get()
    {
        if (attributeName != null)
        {
            if (attribute == null)
            {
                if (data.mandatory)
                    throw new MandatoryAttributeNotFoundException(node, attributeName);
                return new(node, attribute, data.defaultValue);
            }
            return new(node, attribute, getFromString(attribute.Value));
        }
        else
        {
            return new(node, null, getFromString(node.InnerText));
        }
    }

    protected abstract TValue getFromString(string stringValue);

    protected void throwValueInvalidException(string message)
        => throw new AttributeOrInnerValueInvalidException(message, node, attribute);

}
