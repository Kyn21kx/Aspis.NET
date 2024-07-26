namespace AspisNet.Security {
    public interface IJwtFactory {

        string MakeWithReflectionPayload<T>(T payload);

        string MakeWithDictionaryPayload(IDictionary<string, object> payload);

    }
}
