﻿namespace BToolbox.XmlDeserializer.Attributes;

public interface IAttributeConverter<TOutput>
{
    public TOutput Convert(string stringValue);
}
