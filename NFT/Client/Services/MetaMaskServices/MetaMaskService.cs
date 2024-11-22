using Microsoft.JSInterop;
using NFT.Shared.DataTransferObjects.AuthRequests;
using System.Net.Http.Json;

namespace NFT.Client.Services.MetaMaskServices
{
    public class MetaMaskService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public MetaMaskService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> RequestAccountAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("ethereumInterop.requestAccount");
        }

        public async Task<string> SignMessageAsync(string message)
        {
            return await _jsRuntime.InvokeAsync<string>("ethereumInterop.signMessage", message);
        }

        public async Task<AuthResponseDto> AuthenticateAsync(string account, string signature, string message)
        {
            var authRequest = new AuthRequestDto
            {
                Account = account,
                Signature = signature,
                Message = message
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/verify", authRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            }

            throw new Exception($"Authentication failed. Server returned status code: {response.StatusCode}");
        }
    }
}
