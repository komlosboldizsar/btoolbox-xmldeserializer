﻿using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer.Attributes;

public abstract class XmlAttributeOrInnerParserBuilder<TBuilder, TValue, TData>
    where TBuilder : XmlAttributeOrInnerParserBuilder<TBuilder, TValue, TData>
    where TData : XmlAttributeOrInnerParserData<TValue>, new()
{

    protected readonly XmlNode node;
    protected readonly string attributeName;
    protected readonly TData data = new();
    protected readonly DeserializationContext context;

    public XmlAttributeOrInnerParserBuilder(XmlNode node, string attributeName, DeserializationContext context)
    {
        this.node = node;
        this.attributeName = attributeName;
        this.context = context;
    }

    public abstract XmlAttributeOrInnerParser<TValue, TData> Build();

    public XmlAttributeOrInnerData<TValue> Get()
        => Build().Get();

    public TBuilder Mandatory(bool isMandatory = true)
    {
        data.mandatory = isMandatory;
        return (TBuilder)this;
    }

    public TBuilder Default(TValue defaultValue)
    {
        data.defaultValue = defaultValue;
        return (TBuilder)this;
    }

}
