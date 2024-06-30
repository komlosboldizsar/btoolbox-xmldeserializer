using BToolbox.XmlDeserializer.Exceptions;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public class AttributeOrInnerValueInvalidException : DeserializationException
{

    public AttributeOrInnerValueInvalidException(string message, XmlNode xmlNodeOrAttribute)
        : base(((xmlNodeOrAttribute is XmlAttribute) ? "Attribute" : "Node inner") + $" value invalid: [{message}]", xmlNodeOrAttribute) { }


    public AttributeOrInnerValueInvalidException(string message, XmlNode xmlNode, XmlAttribute xmlAttribute)
        : base(((xmlAttribute != null) ? "Attribute" : "Node inner") + $" value invalid: [{message}]", (xmlAttribute ?? xmlNode)) { }

    public AttributeOrInnerValueInvalidException(string message)
        : base($"Attribute/node inner value invalid: [{message}]", (XmlNode)null) { }

}
