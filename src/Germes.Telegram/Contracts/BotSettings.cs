namespace Germes.Domain.Data
{
    public class BotSettings
    {
        public string Token { get; set; }
        public bool UseNgrok { get; set; }
        public string NgrokHost { get; set; }
    }
}
