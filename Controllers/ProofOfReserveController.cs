using Microsoft.AspNetCore.Mvc;
using ProofOfReserveApi.Models;
using MerkleTreeLib;
using System.Linq;

namespace ProofOfReserveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProofOfReserveController : ControllerBase
    {
        private readonly UserDatabase _database = new UserDatabase();

        [HttpGet("merkle-root")]
        public ActionResult<string> GetMerkleRoot()
        {
            var inputs = _database.GetSerializedUsers();
            // Using “ProofOfReserve_Leaf“ and “ProofOfReserve_Branch“ for hash tag
            var (merkleRoot, _) = MerkleTree.CalculateMerkleRootWithProofs(inputs, "ProofOfReserve_Leaf", "ProofOfReserve_Branch");
            return Ok(merkleRoot);
        }

        [HttpGet("merkle-proof/{userId}")]
        public ActionResult<object> GetMerkleProof(int userId)
        {
            var user = _database.GetUser(userId);
            if (!user.HasValue)
                return NotFound($"User ID {userId} not found");

            var inputs = _database.GetSerializedUsers();
            // Using “ProofOfReserve_Leaf“ and “ProofOfReserve_Branch“ for hash tag
            var (merkleRoot, proofs) = MerkleTree.CalculateMerkleRootWithProofs(inputs, "ProofOfReserve_Leaf", "ProofOfReserve_Branch");
            var serializedUser = $"({user.Value.UserId},{user.Value.Balance})";
            var (leafHash, path) = MerkleTree.GetMerkleProof(serializedUser, "ProofOfReserve_Leaf", proofs);
            return Ok(new
            {
                Balance = user.Value.Balance,
                Proof = path.Select(p => new { Hash = p.Hash, Side = p.Side }).ToList()
            });
        }
    }
}