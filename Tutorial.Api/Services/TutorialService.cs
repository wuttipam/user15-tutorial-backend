using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Tutorial.Api.Models;

namespace Tutorial.Api.Services
{
    public class TutorialService : ITutorialService
    {
        #region Property
        private readonly IMongoCollection<Models.Tutorial> _tutorialCollection;
        #endregion


        #region Constructor
        public TutorialService(IOptions<TutorialDatabaseSettings> tutorialDatabaseSettings)
        {
            var mongoClient = new MongoClient(tutorialDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(tutorialDatabaseSettings.Value.DatabaseName);

            _tutorialCollection = mongoDatabase.GetCollection<Models.Tutorial>(tutorialDatabaseSettings.Value.TutorialCollectionName);
        }
        #endregion

        public async Task<List<Models.Tutorial>> GetAsync() =>
            await _tutorialCollection.Find(_ => true).ToListAsync();

        public async Task<List<Models.Tutorial>> GetAyncByTitle(string title)
        {
            return await _tutorialCollection.Find(x => x.Title == title).ToListAsync();
        }

        public async Task<Models.Tutorial?> GetAsync(string id) =>
            await _tutorialCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Models.Tutorial newTutorial)
        {
            await _tutorialCollection.InsertOneAsync(newTutorial);
        }
            
        public async Task UpdateAsync(string id, Models.Tutorial updatedTutorial) =>
            await _tutorialCollection.ReplaceOneAsync(x => x.Id == id, updatedTutorial);

        public async Task RemoveAsync(string id) =>
            await _tutorialCollection.DeleteOneAsync(x => x.Id == id);

        public async Task RemoveAsync() =>
           await _tutorialCollection.DeleteManyAsync(x => x.Id !=null);
    }


}
