namespace VoorraadBeheer.Authentication
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        public string Role { get; set; }
    }
}
