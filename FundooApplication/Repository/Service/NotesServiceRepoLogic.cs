using Repository.Context;
using Repository.Interface;
using System;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using StackExchange.Redis;
using ModelLayer.Entity;

namespace Repository.Service
{
    public class NotesServiceRepoLogic : INotesRepository
    {
        private readonly DapperContext _context;
        private readonly IDatabase _cache;

        public NotesServiceRepoLogic(DapperContext context, ConnectionMultiplexer redisConnection)
        {
            _context = context;
            _cache = redisConnection.GetDatabase();
        }

        public async Task<int> Addnotes(int userid, Notes notes)
        {
            var query = @"
                INSERT INTO Notes (NoteId, Title, Description, BgColor, ImagePath, Remainder, IsArchive, IsPinned, IsTrash, CreatedAt, ModifiedAt, LabelName, Userid)
                VALUES (@NoteId, @Title, @Description, @BgColor, @ImagePath, @Remainder, @IsArchive, @IsPinned, @IsTrash, @CreatedAt, @ModifiedAt, @LabelName, @Userid)";

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { notes.NoteId, notes.Title, notes.Description, notes.BgColor, notes.ImagePath, notes.Remainder, notes.IsArchive, notes.Ispinned, notes.IsTrash, notes.CreatedAt, notes.ModifiedAt, notes.LabelName, notes.userid });

                // Clear cache after adding a new note
                var cacheKey = $"notes_{notes.userid}";
                await _cache.KeyDeleteAsync(cacheKey);

                return affectedRows;
            }
        }

        public async Task<IEnumerable<Notes>> GetNotesById(int UserId)
        {
            var cacheKey = $"notes_{UserId}";
            var cachedData = await _cache.StringGetAsync(cacheKey);

            if (!cachedData.IsNullOrEmpty)
            {
                // Deserialize the cached data into a collection of Notes
                return JsonConvert.DeserializeObject<List<Notes>>(cachedData);
            }
            else
            {
                var query = "SELECT * FROM Notes WHERE UserId = @UserId";

                using (var connection = _context.CreateConnection())
                {
                    // Execute the query to get notes for the specified UserId
                    var notes = await connection.QueryAsync<Notes>(query, new { UserId });

                    // Cache the data for future use
                    await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(notes), TimeSpan.FromMinutes(10));

                    // Return the notes retrieved from the database
                    return notes;
                }
            }
        }


        public async Task<int> DeletenotesbyId(int userid)
        {
            var query = $"DELETE FROM Notes WHERE userid = @userid";

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { userid });

                // Clear cache after deleting a note
                var cacheKey = $"notes_{userid}";
                await _cache.KeyDeleteAsync(cacheKey);

                return affectedRows;
            }
        }

        public async Task<int> EditbynoteId(int Userid, int Noteid, Notes note)
        {
            var query = @"
                UPDATE Notes 
                SET Title = @title, 
                    Description = @description, 
                    BgColor = @bgcolor, 
                    ImagePath = @imagepath, 
                    Remainder = @remainder, 
                    IsArchive = @isarchive, 
                    Ispinned = @ispinned,
                    IsTrash = @istrash, 
                    CreatedAt = @createat, 
                    ModifiedAt = @modifiedat, 
                    LabelName = @labelname, 
                    userid = @userid 
                WHERE NoteId = @noteid";

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new
                {
                    noteid = Noteid,
                    title = note.Title,
                    description = note.Description,
                    bgcolor = note.BgColor,
                    imagepath = note.ImagePath,
                    remainder = note.Remainder,
                    isarchive = note.IsArchive,
                    ispinned = note.Ispinned,
                    istrash = note.IsTrash,
                    createat = note.CreatedAt,
                    modifiedat = note.ModifiedAt,
                    labelname = note.LabelName,
                    userid = Userid
                });

                // Clear cache after updating a note
                var cacheKey = $"notes_{Noteid}";
                await _cache.KeyDeleteAsync(cacheKey);

                return affectedRows;
            }
        }
    }
}
