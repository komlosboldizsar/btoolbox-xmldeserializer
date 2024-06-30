using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public record XmlAttributeOrInnerData<TValue>(XmlNode node, XmlAttribute Attribute, TValue Value);
