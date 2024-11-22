namespace NFT.Core.Entities
{
    public class AuthRequest
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Signature { get; set; }
        public string Message { get; set; }
    }

}
