using Microsoft.Extensions.DependencyInjection;
using IdentityModel.Client;

using Prime.HttpClients;

namespace Prime.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder WithBearerToken<T>(this IHttpClientBuilder builder, T credentials) where T : ClientCredentialsTokenRequest
        {
            builder.Services.AddSingleton(credentials)
                .AddTransient<BearerTokenHandler<T>>();

            builder.AddHttpMessageHandler<BearerTokenHandler<T>>();

            return builder;
        }
    }
}
