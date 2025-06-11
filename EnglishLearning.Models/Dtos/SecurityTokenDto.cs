namespace Topic.Models.Dtos
{
    public class SecurityTokenDto
    {
        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}