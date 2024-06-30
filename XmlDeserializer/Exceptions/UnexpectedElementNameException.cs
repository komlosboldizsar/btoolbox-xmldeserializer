using System.Xml;

namespace BToolbox.XmlDeserializer.Exceptions;

public class UnexpectedElementNameException : DeserializationException
{

    public string[] ExpectedElementNames { get; }
    public string[] IgnoredElementNames { get; }

    public UnexpectedElementNameException(XmlNode invalidNode, params string[] expectedElementNames)
        : base($"Unexpected element with name [{invalidNode.LocalName}], expected: [{string.Join(';', expectedElementNames)}]", invalidNode)
        => ExpectedElementNames = expectedElementNames;

    public UnexpectedElementNameException(XmlNode invalidNode, string[] expectedElementNames, string[] ignoredElementNames)
        : base($"Unexpected element with name [{invalidNode.LocalName}], expected: [{string.Join(';', expectedElementNames)}], ignored: [{string.Join(';', ignoredElementNames)}]", invalidNode)
    {
        ExpectedElementNames = expectedElementNames;
        IgnoredElementNames = ignoredElementNames;
    }

}
