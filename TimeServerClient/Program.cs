using System.Security.Cryptography.X509Certificates;
using Grpc.Net.Client;
using TimeServer;

string certificatePath = "";
string timeServerUrl = "";

var certificate = new X509Certificate2(certificatePath);
HttpClientHandler handler = new()
{
    SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
    ClientCertificateOptions = ClientCertificateOption.Manual
};

handler.ClientCertificates.Add(certificate);

using var channel = GrpcChannel.ForAddress(timeServerUrl, new()
{
    HttpHandler = handler,
    DisposeHttpClient = true
});

var client = new TimeService.TimeServiceClient(channel);
var response = await client.GetCurrentTimeAsync(new TimeRequest());

Console.WriteLine(response.Message);
Console.ReadKey();
