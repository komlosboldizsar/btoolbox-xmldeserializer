using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public sealed class XmlAttributeOrInnerIntParser : XmlAttributeOrInnerParser<int?, XmlAttributeOrInnerIntParser.Data>
{

    public XmlAttributeOrInnerIntParser(XmlNode node, string attributeName, Data data, DeserializationContext context)
        : base(node, attributeName, data, context) { }

    protected override int? getFromString(string stringValue)
    {
        if (!int.TryParse(stringValue, out int intValue))
            throwValueInvalidException("Not an integer.");
        data.min?.Check(intValue, attribute);
        data.max?.Check(intValue, attribute);
        return intValue;
    }

    public class Builder : XmlAttributeOrInnerParserBuilder<Builder, int?, Data>
    {

        public Builder(XmlNode node, string attributeName, DeserializationContext context)
            : base(node, attributeName, context) { }

        public override XmlAttributeOrInnerIntParser Build()
            => new(node, attributeName, data, context);

        public Builder Min(int boundary, bool inclusive = true)
        {
            data.min = new MinBoundary(boundary, inclusive);
            return this;
        }

        public Builder Max(int boundary, bool inclusive = true)
        {
            data.max = new MaxBoundary(boundary, inclusive);
            return this;
        }

    }

    public class Data : XmlAttributeOrInnerParserData<int?>
    {
        public MinBoundary min;
        public MaxBoundary max;
    }

    public abstract class Boundary
    {

        protected readonly int boundary;
        protected readonly bool inclusive;

        public Boundary(int boundary, bool inclusive)
        {
            this.boundary = boundary;
            this.inclusive = inclusive;
        }

        public abstract void Check(int value, XmlAttribute attribute);
        protected abstract string ErrorMessage { get; }
        protected void throwErrorMessage(XmlAttribute attribute)
            => throw new AttributeOrInnerValueInvalidException(ErrorMessage, attribute);

    }

    public class MinBoundary : Boundary
    {

        public MinBoundary(int boundary, bool inclusive)
            : base(boundary, inclusive) { }

        public override void Check(int value, XmlAttribute attribute)
        {
            if (value < boundary)
                throwErrorMessage(attribute);
            if (!inclusive && value == boundary)
                throwErrorMessage(attribute);
        }

        protected override string ErrorMessage => $"Value must be greater than or equal to {boundary}.";

    }

    public class MaxBoundary : Boundary
    {

        public MaxBoundary(int boundary, bool inclusive)
            : base(boundary, inclusive) { }

        public override void Check(int value, XmlAttribute attribute)
        {
            if (value > boundary)
                throwErrorMessage(attribute);
            if (!inclusive && value == boundary)
                throwErrorMessage(attribute);
        }

        protected override string ErrorMessage => $"Value must be less than or equal to {boundary}.";

    }

}

public static class XmlAttributeIntParserHelpers
{

    public static XmlAttributeOrInnerIntParser.Builder AttributeAsInt(this XmlNode node, string attributeName, DeserializationContext context)
       => new(node, attributeName, context);

    public static XmlAttributeOrInnerIntParser.Builder InnerAsInt(this XmlNode node, DeserializationContext context)
       => new(node, null, context);

}

