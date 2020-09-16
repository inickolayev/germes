using System;
using System.Threading.Tasks;
using Flurl.Http;
using Ngrok.Adapter.Data;

namespace Ngrok.Adapter.Service
{
    public interface INgrokService
    {
        /// <summary>
        ///     Получить адрес https тунеля
        /// </summary>
        Task<string> GetHttpsTunnelUrl();
    }
}
