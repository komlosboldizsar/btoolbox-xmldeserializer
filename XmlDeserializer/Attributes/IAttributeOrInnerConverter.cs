namespace BToolbox.XmlDeserializer.Attributes;

public interface IAttributeOrInnerConverter<TOutput>
{
    public TOutput Convert(string stringValue);
}
