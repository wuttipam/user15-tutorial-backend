using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tutorial.Api.Models;

namespace Tutorial.Api.Services
{
    public interface ITutorialService
    {

        public Task<List<Models.Tutorial>> GetAsync();
        public Task<List<Models.Tutorial>> GetAyncByTitle(string title);
        public Task<Models.Tutorial?> GetAsync(string id);
        public Task CreateAsync(Models.Tutorial newTutorial);
        public Task UpdateAsync(string id, Models.Tutorial updatedTutorial);
        public Task RemoveAsync(string id);
        public Task RemoveAsync();
    }


}
