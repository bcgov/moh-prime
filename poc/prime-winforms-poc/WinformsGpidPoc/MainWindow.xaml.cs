using IdentityModel.OidcClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinformsGpidPoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // The application needs to catch the response from Keycloak at some known URL.
            // This could be done by adding a custom URL in the system registry, but the easier and less intrusive method is to listen on the computer's loopback adaptor.
            string redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";

            var redirectListener = new HttpListener();
            redirectListener.Prefixes.Add(redirectUri);
            redirectListener.Start();

            // This OIDC client comes from the IdentityModel.OidcClient library, which handles the login request building and access code exchange. It can be installed using Nuget by using the command:
            // Install-Package IdentityModel.OidcClient -Version 4.0.0-preview.3
            // It is not reccommended to write this code yourself.
            var options = new OidcClientOptions
            {
                Authority = "https://sso-dev.pathfinder.gov.bc.ca/auth/realms/v4mbqqas",
                ClientId = "prime-pos-gpid",
                Scope = "openid",
                RedirectUri = redirectUri,
                IdentityTokenValidator = new NoValidationIdentityTokenValidator()
            };
            var oidcClient = new OidcClient(options);

            // Specifying the idp_hint bypasses an unnecessary screen in keycloak and takes the user directly to the BC Services Card login page.
            var extraParameter = new Dictionary<string, string> { { "kc_idp_hint", "bcsc" } };
            var state = await oidcClient.PrepareLoginAsync(extraParameter);

            // Using Process.Start on a URL should open the computer's default browser.
            // An in-app browser could be used to serve the login, but this is not reccommended by the OIDC standards and is far harder to implement.
            Process.Start(state.StartUrl);

            // Catch the response
            var context = await redirectListener.GetContextAsync();
            var response = context.Request.Url.ToString();
            redirectListener.Stop();

            // The response from the login contains a code that has to be exchanged with Keycloak for a JWT token to access the application.
            var oidcResult = await oidcClient.ProcessResponseAsync(response, state);

            // Now that we have the JWT token, we can add it to the Authorization header of our response to grab the GPID.
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {oidcResult.AccessToken}");
            var httpResponse = await httpClient.GetAsync("https://develop.pharmanetenrolment.gov.bc.ca/api/v1/provisioner-access/gpid");
            var content = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(content);

            // This brings the application to the front for viewing.
            this.Activate();
            textBox.Text = result.result;
        }

        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private class ResponseModel
        {
            public string result { get; set; }
        }
    }
}
