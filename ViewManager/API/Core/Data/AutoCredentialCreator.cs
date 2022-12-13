namespace API.Core.Data
{
    public class AutoCredentialCreator
    {
        private static Random s_random = new Random();

        public List<string> RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            List<string> credential = new();

            for (int i = 0; i < 2; i++)
            {
                credential.Add(new string(Enumerable.Repeat(chars, length)
               .Select(s => s[s_random.Next(s.Length)]).ToArray()));
            }

            return credential;
        }
    }
}
