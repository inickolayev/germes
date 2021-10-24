using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Germes.Implementations.Services
{
    public class TelegramSourceAdapter : ISourceAdapter
    {
        private readonly TelegramBotClient _client;
        public static string SourceId { get; } = "Telegram";

        public TelegramSourceAdapter(TelegramBotClient client)
        {
            _client = client;
        }

        public bool Check(string sourceId)
            => sourceId == SourceId;

        public async Task<string> GetName(string chatId, CancellationToken cancellationToken)
        {
            var chat = await _client.GetChatAsync(new ChatId(long.Parse(chatId)), cancellationToken);
            return $"{chat.FirstName} {chat.LastName}";
        }
    }
}