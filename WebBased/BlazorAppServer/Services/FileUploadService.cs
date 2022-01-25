using BlazorAppServer.Base;
using EBazarModels.Models;
using System.Linq.Expressions;

namespace BlazorAppServer.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly HttpClient _context;
        public FileUploadService(HttpClient context)
        {
            _context = context;
        }

        public async Task<HttpResponseMessage> UploadAsync(string uri, HttpContent entity)
        {
            return await _context.PostAsync(uri, entity);
        }
    }

}
