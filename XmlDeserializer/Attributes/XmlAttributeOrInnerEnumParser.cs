using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public sealed class XmlAttributeOrInnerEnumParser<TEnum> : XmlAttributeOrInnerParser<TEnum, XmlAttributeOrInnerEnumParser<TEnum>.Data>
    where TEnum : Enum
{

    public XmlAttributeOrInnerEnumParser(XmlNode node, string attributeName, Data data, DeserializationContext context)
        : base(node, attributeName, data, context) { }

    protected override TEnum getFromString(string stringValue)
    {
        if (!data.enumValues.TryGetValue(stringValue, out TEnum enumValue))
        {
            if (data.defaultOnUnknown)
                return data.defaultValue;
            throw new Exception();
        }
        return enumValue;
    }

    public class Builder : XmlAttributeOrInnerParserBuilder<Builder, TEnum, Data>
    {

        public Builder(XmlNode node, string attributeName, DeserializationContext context)
            : base(node, attributeName, context) { }

        public override XmlAttributeOrInnerEnumParser<TEnum> Build()
            => new(node, attributeName, data, context);

        public Builder Translation(string stringValue, TEnum enumValue)
        {
            data.enumValues.Add(stringValue, enumValue);
            return this;
        }

        public Builder DefaultOnUnknown(bool defaultOnUnknown)
        {
            data.defaultOnUnknown = defaultOnUnknown;
            return this;
        }

    }

    public class Data : XmlAttributeOrInnerParserData<TEnum>
    {
        public Dictionary<string, TEnum> enumValues = new();
        public bool defaultOnUnknown = false;
    }

}

public static class XmlAttributeEnumParserHelpers
{

    public static XmlAttributeOrInnerEnumParser<TEnum>.Builder AttributeAsEnum<TEnum>(this XmlNode node, string attributeName, DeserializationContext context)
        where TEnum : Enum
       => new(node, attributeName, context);

    public static XmlAttributeOrInnerEnumParser<TEnum>.Builder InnerAsEnum<TEnum>(this XmlNode node, DeserializationContext context)
        where TEnum : Enum
       => new(node, null, context);

}

