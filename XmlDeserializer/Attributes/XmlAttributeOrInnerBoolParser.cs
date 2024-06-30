using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public sealed class XmlAttributeOrInnerBoolParser : XmlAttributeOrInnerParser<bool, XmlAttributeOrInnerBoolParser.Data>
{

    public XmlAttributeOrInnerBoolParser(XmlNode node, string attributeName, Data data, DeserializationContext context)
        : base(node, attributeName, data, context) { }

    protected override bool getFromString(string stringValue)
        => stringValue == STR_TRUE;

    private const string STR_TRUE = "true";

    public class Builder : XmlAttributeOrInnerParserBuilder<Builder, bool, Data>
    {

        public Builder(XmlNode node, string attributeName, DeserializationContext context)
            : base(node, attributeName, context) { }

        public override XmlAttributeOrInnerBoolParser Build()
            => new(node, attributeName, data, context);

    }

    public class Data : XmlAttributeOrInnerParserData<bool> { }

}

public static class XmlAttributeOrInnerBoolParserHelpers
{

    public static XmlAttributeOrInnerBoolParser.Builder AttributeAsBool(this XmlNode node, string attributeName, DeserializationContext context)
       => new(node, attributeName, context);

    public static XmlAttributeOrInnerBoolParser.Builder InnerAsBool(this XmlNode node, DeserializationContext context)
       => new(node, null, context);

}

