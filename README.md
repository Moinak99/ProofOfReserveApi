# ProofOfReserveApi

Proof of Reserve API -This is an ASP.NET Core Web API that calculates Merkle Roots and generates Merkle Proofs for user balances. I paired it with a custom `MerkleTreeLib` to make it secure.

## What It Does
This API lets you:
- Get the Merkle Root for a set of user balances via `/merkle-root`.
- Fetch a proof path for a specific user’s balance via `/merkle-proof/{userId}`.
I hardcoded 8 users (IDs 1-8 with balances 1111-8888 JPY) to test it out.

## How to Set It Up
1. Clone the repo: `git clone https://github.com/<your-username>/ProofOfReserveApi`
2. Grab the .NET 9.0 SDK - I’m using that, so it should match!
3. CD into `ProofOfReserveApi_v1` and run `dotnet restore`.
4. Build it with `dotnet build` - fingers crossed it works!
5. Start it with `dotnet run` - should fire up on `http://localhost:5087`.

## Usage
### Endpoints
- **GET /api/proofofreserve/merkle-root**
  - Returns the Merkle Root as a hex string (e.g., `2b93f77e...`).
  - Example: `curl http://localhost:5087/api/proofofreserve/merkle-root`

- **GET /api/proofofreserve/merkle-proof/{userId}**
  - Returns `{ "Balance": int, "Proof": [{ "Hash": string, "Side": int }] }` for valid IDs (1-8).
  - Returns 404 for invalid IDs (e.g., 9).
  - Example: `curl http://localhost:5087/api/proofofreserve/merkle-proof/1`
  - Sample response:
    ```json
    {
      "Balance": 1111,
      "Proof": [
        { "Hash": "hex1", "Side": 0 },
        { "Hash": "hex2", "Side": 1 }
      ]
    }
