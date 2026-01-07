using Duende.IdentityModel.Client;
using MicroserviceCourse.Web.Options;
using MicroserviceCourse.Web.Services;
using System.Net;

namespace MicroserviceCourse.Web.Pages.Auth.SignUp;

public record KeycloakErrorResponse(string ErrorMessage);

public class SignUpService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
{
    public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
    {
        var token = await GetClientCredentialTokenAsAdmin();

        var address = $"{identityOption.BaseAddress}/admin/realms/UdemyTenant/users/";
        client.SetBearerToken(token);

        var userCreateRequest = CreateUserCreateRequest(model);
        
        var response = await client.PostAsJsonAsync(address, userCreateRequest);

        if (!response.IsSuccessStatusCode)
        {
            if(response.StatusCode != HttpStatusCode.InternalServerError)
            {
                var keycloakResponseError = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                return ServiceResult.Error(keycloakResponseError!.ErrorMessage);
            }

            var error = await response.Content.ReadAsStringAsync();
            logger.LogError(error);

            return ServiceResult.Error("System Error occurred. Please try again later.");
        }

        return ServiceResult.Success();
    }

    private async Task<string> GetClientCredentialTokenAsAdmin()
    {
        var discoveryRequest = new DiscoveryDocumentRequest()
        {
            Address = identityOption.Address,
            Policy = { RequireHttps = false },
        };

        client.BaseAddress = new Uri(identityOption.Address);
        var discoveryResponse = await client.GetDiscoveryDocumentAsync();

        if (discoveryResponse.IsError)
            throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret
            });

        if (tokenResponse.IsError)
            throw new Exception($"Token request failed: {tokenResponse.Error}");

        return tokenResponse.AccessToken!;
    }
    private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
    {
        return new UserCreateRequest(model.UserName, true, model.FirstName, model.LastName, model.Email,
            [new Credential("password", model.Password, false)]);
    }
}
