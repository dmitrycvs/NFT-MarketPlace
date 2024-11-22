

namespace NFT.Shared.DataTransferObjects.AuthRequests
{
    public class AuthRequestDto
    {
        public string Account { get; set; }
        public string Signature { get; set; }
        public string Message { get; set; }
    }

    public class AuthResponseDto
    {
        public bool Success { get; set; }
    }
}