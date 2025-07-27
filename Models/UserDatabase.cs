namespace ProofOfReserveApi.Models
{
    public class UserDatabase
    {
        public List<(int UserId, int Balance)> Users { get; } = new List<(int UserId, int Balance)>
        {   // Creating a list of users foe verification.
            (1, 1111), (2, 2222), (3, 3333), (4, 4444),
            (5, 5555), (6, 6666), (7, 7777), (8, 8888)
        };

        public string[] GetSerializedUsers()
        {
            return Users.Select(u => $"({u.UserId},{u.Balance})").ToArray();
        }

        public (int UserId, int Balance)? GetUser(int userId)
        {
            //Retrieves the user data (User ID and balance) for a specific User ID.
            return Users.Find(u => u.UserId == userId);
        }
    }
}