using System;
using Microsoft.Extensions.DependencyInjection;
using IdentityModel.Client;

using Prime.HttpClients;

namespace Prime.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddHttpClientWithBaseAddress<TClient, TImplementation>(this IServiceCollection services, string baseAddress)
            where TClient : class
            where TImplementation : class, TClient
        {
            return services.AddHttpClient<TClient, TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseAddress.EnsureTrailingSlash());
            });
        }

        public static IHttpClientBuilder WithBearerToken<T>(this IHttpClientBuilder builder, T credentials) where T : ClientCredentialsTokenRequest
        {
            builder.Services.AddSingleton(credentials)
                .AddTransient<BearerTokenHandler<T>>();

            builder.AddHttpMessageHandler<BearerTokenHandler<T>>();

            return builder;
        }
    }
}
