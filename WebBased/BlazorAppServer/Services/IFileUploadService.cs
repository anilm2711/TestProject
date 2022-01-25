using BlazorAppServer.Base;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public interface IFileUploadService
    {
        Task<HttpResponseMessage> UploadAsync(string uri, HttpContent entity);
    }
}
