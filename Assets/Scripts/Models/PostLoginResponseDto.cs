namespace Models
{
    public class PostLoginResponseDto
    {
        public string tokenType;
        public string accessToken;
        public int expiresIn;
        public string refreshToken;
    }
}