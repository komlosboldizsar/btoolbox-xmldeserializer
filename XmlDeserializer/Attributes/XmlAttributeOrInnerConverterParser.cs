using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public sealed class XmlAttributeOrInnerConverterParser<TOutput> : XmlAttributeOrInnerParser<TOutput, XmlAttributeOrInnerConverterParser<TOutput>.Data>
{

    public XmlAttributeOrInnerConverterParser(XmlNode node, string attributeName, Data data, DeserializationContext context, IAttributeOrInnerConverter<TOutput> converter)
        : base(node, attributeName, data, context)
        => this.converter = converter;

    private readonly IAttributeOrInnerConverter<TOutput> converter;

    protected override TOutput getFromString(string stringValue)
    {
        try
        {
            return converter.Convert(stringValue);
        }
        catch (ArgumentException ex)
        {
            throw new AttributeOrInnerValueInvalidException(ex.Message, attribute);
        }
    }

    public class Builder : XmlAttributeOrInnerParserBuilder<Builder, TOutput, Data>
    {

        public Builder(XmlNode node, string attributeName, DeserializationContext context, IAttributeOrInnerConverter<TOutput> converter)
            : base(node, attributeName, context)
            => this.converter = converter;

        private readonly IAttributeOrInnerConverter<TOutput> converter;

        public override XmlAttributeOrInnerConverterParser<TOutput> Build()
            => new(node, attributeName, data, context, converter);

    }

    public class Data : XmlAttributeOrInnerParserData<TOutput>
    { }

}

public static class XmlAttributeConverterParserHelpers
{

    public static XmlAttributeOrInnerConverterParser<TOutput>.Builder AttributeAs<TOutput>(this XmlNode node, string attributeName, DeserializationContext context, IAttributeOrInnerConverter<TOutput> converter)
       => new(node, attributeName, context, converter);

    public static XmlAttributeOrInnerConverterParser<TOutput>.Builder InnerAs<TOutput>(this XmlNode node, DeserializationContext context, IAttributeOrInnerConverter<TOutput> converter)
       => new(node, null, context, converter);

}