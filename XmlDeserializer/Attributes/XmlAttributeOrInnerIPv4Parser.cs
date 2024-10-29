using BToolbox.XmlDeserializer.Context;
using System.Text.RegularExpressions;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public sealed class XmlAttributeOrInnerIPv4Parser : XmlAttributeOrInnerParser<string, XmlAttributeOrInnerIPv4Parser.Data>
{

    public XmlAttributeOrInnerIPv4Parser(XmlNode node, string attributeName, Data data, DeserializationContext context)
        : base(node, attributeName, data, context) { }

    protected override string getFromString(string stringValue)
    {
        if (!REGEXP_IP_ADDRESS.IsMatch(stringValue))
            throw new AttributeOrInnerValueInvalidException("Invalid IPv4 address");
        return stringValue;
    }

    private readonly static Regex REGEXP_IP_ADDRESS = new(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public class Builder : XmlAttributeOrInnerParserBuilder<Builder, string, Data>
    {

        public Builder(XmlNode node, string attributeName, DeserializationContext context)
            : base(node, attributeName, context) { }

        public override XmlAttributeOrInnerIPv4Parser Build()
            => new(node, attributeName, data, context);

    }

    public class Data : XmlAttributeOrInnerParserData<string>
    { }

}

public static class XmlAttributeIPv4ParserHelpers
{

    public static XmlAttributeOrInnerIPv4Parser.Builder AttributeAsIPv4(this XmlNode node, string attributeName, DeserializationContext context)
       => new(node, attributeName, context);

    public static XmlAttributeOrInnerIPv4Parser.Builder InnerAsIPv4(this XmlNode node, DeserializationContext context)
       => new(node, null, context);

}

