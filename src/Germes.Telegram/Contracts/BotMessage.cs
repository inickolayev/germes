namespace Germes.Domain.Data
{
    public class BotMessage
    {
        /// <summary>
        ///     Текст сообщения
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        ///     Идентификатор чата
        /// </summary>
        public string ChatId { get; set; }
        /// <summary>
        ///     Идентификатор источника
        /// </summary>
        public string SourceId { get; set; }
    }
}
