using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public sealed class XmlAttributeOrInnerStringParser : XmlAttributeOrInnerParser<string, XmlAttributeOrInnerStringParser.Data>
{

    public XmlAttributeOrInnerStringParser(XmlNode node, string attributeName, Data data, DeserializationContext context)
        : base(node, attributeName, data, context) { }

    protected override string getFromString(string stringValue)
    {
        if (data.notEmpty && string.IsNullOrWhiteSpace(stringValue))
            throwValueInvalidException("Value must be not empty");
        return stringValue;
    }

    public class Builder : XmlAttributeOrInnerParserBuilder<Builder, string, Data>
    {

        public Builder(XmlNode node, string attributeName, DeserializationContext context)
            : base(node, attributeName, context) { }

        public override XmlAttributeOrInnerStringParser Build()
            => new(node, attributeName, data, context);

        public Builder NotEmpty()
        {
            data.notEmpty = true;
            return this;
        }

    }

    public class Data : XmlAttributeOrInnerParserData<string>
    {
        public bool notEmpty;
    }

}

public static class XmlAttributeStringParserHelpers
{

    public static XmlAttributeOrInnerStringParser.Builder AttributeAsString(this XmlNode node, string attributeName, DeserializationContext context)
       => new(node, attributeName, context);

    public static XmlAttributeOrInnerStringParser.Builder InnerAsString(this XmlNode node, DeserializationContext context)
       => new(node, null, context);

}

