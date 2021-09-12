namespace Germes.Data
{
    public class BotResult
    {
        public string Text { get; set; }
        public bool NeedToAnswer { get; set; }

        public BotResult(string text, bool needToAnswer = true)
        {
            Text = text;
            NeedToAnswer = needToAnswer;
        }
    }
}
