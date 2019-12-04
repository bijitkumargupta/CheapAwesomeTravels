using System.Threading.Tasks;

namespace WebBedsAPI_Adapter
{
    public interface IWebBedsAPI
    {
        Task<FindBargainResponseMessage> FindBargain(string apiMethodName, int destinationId, int nights, string authCode);
    }
}