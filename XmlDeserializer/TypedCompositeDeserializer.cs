using BToolbox.XmlDeserializer.Context;
using System.Xml;

namespace BToolbox.XmlDeserializer;

public class TypedCompositeDeserializer<TResult, TEnvironment> : CompositeDeserializer<TResult, TEnvironment>
{

    public TypedCompositeDeserializer(string elementName, Func<TResult> resultCreator)
        : base(elementName)
        => this.resultCreator = (x, c, o) => resultCreator();

    public TypedCompositeDeserializer(string elementName, Func<XmlNode, DeserializationContext, object, TResult> resultCreator)
        : base(elementName)
        => this.resultCreator = resultCreator;

    private readonly Func<XmlNode, DeserializationContext, object, TResult> resultCreator;
    protected override TResult createResult(XmlNode xmlNode, DeserializationContext context, object parent)
        => resultCreator(xmlNode, context, parent);

    public class Registration<TElement> : DeserializerRegistrationBase<TResult, TElement, TEnvironment>
    {

        private readonly Action<TResult, TElement> resultHandler;

        public Registration(IDeserializer<TElement, TEnvironment> deserializer, Action<TResult, TElement> resultHandler)
            : base(deserializer)
            => this.resultHandler = resultHandler;

        protected override void handleResult(TResult result, TElement element) => resultHandler(result, element);

    }

    public Registration<TElement> Register<TElement>(IDeserializer<TElement, TEnvironment> deserializer, Action<TResult, TElement> resultHandler)
    {
        Registration<TElement> registration = new(deserializer, resultHandler);
        addRegistration(deserializer.ElementName, registration);
        return registration;
    }

}
