﻿using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer;

public class SimpleListDeserializer<TElement, TEnvironment> :
    SimpleCollectionDeserializerBase<List<TElement>, TElement, TEnvironment>
{

    public SimpleListDeserializer(string elementName, IDeserializer<TElement, TEnvironment> elementDeserializer)
        : base(elementName, elementDeserializer) { }

    protected override List<TElement> createCollection(XmlNode xmlNode, DeserializationContext context, object parent)
        => new();

    protected override void addElementToCollection(List<TElement> collection, TElement element, XmlNode elementNode, DeserializationContext context)
        => collection.Add(element);

}
