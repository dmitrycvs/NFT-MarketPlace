using Microsoft.AspNetCore.Mvc;
using Nethereum.Signer;
using NFT.Core.Entities;
using System.Net.Http;

namespace MetaMaskAuth.AppHost.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("verify")]
        public IActionResult Verify([FromBody] AuthRequest request)
        {
            var signer = new EthereumMessageSigner();
            var recoveredAddress = signer.EncodeUTF8AndEcRecover(request.Message, request.Signature);

            if (recoveredAddress == request.Account)
            {
                return Ok(new { success = true });
            }

            return Unauthorized();
        }
    }
}