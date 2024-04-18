using NLog;
using Newtonsoft.Json;
using Repository.Context;
using Repository.Interface;
using StackExchange.Redis;
using Dapper;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer.Request_Body;
using System.Reflection.Emit;

namespace Repository.Service
{
    public class LabelServiceRepoLogic : ILabelRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger _logger;
        private readonly IDatabase _cache;

        public LabelServiceRepoLogic(DapperContext context, ILogger logger, ConnectionMultiplexer redisConnection)
        {
            _context = context;
            _logger = logger;
            _cache = redisConnection.GetDatabase();
        }

       //start
        public async Task<int> Addlabel(LabelBody labl, int userid)
        {
            var query = @"
                INSERT INTO Label (LabelName, userid, NoteId)
                VALUES (@labelname, @userid, @noteid)";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new { labelname= labl.LabelName, noteid=labl.NoteId, userid= userid });
                    _logger.Info($"Added {affectedRows} label(s)");
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding label");
                throw;
            }
        }

        public async Task<int> EditbyLabelId(int labelid, LabelBody labl, int userid)
        {
            var query = @"UPDATE Label 
                          SET LabelName = @labelname,NoteId=@noteid
                          WHERE LabelId = @labelId and userid=@Userid";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new { labelname= labl.LabelName, noteid=labl.NoteId, labelId= labelid, Userid=userid });
                    _logger.Info($"Edited {affectedRows} label(s)");
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while editing label");
                throw;
            }
        }

        public async Task<int> DeletebyLabelId(int LabelId, int userid)
        {
            var query = "DELETE FROM Label WHERE LabelId = @Labelid and userid=@Userid";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new { Labelid = LabelId, Userid= userid });
                    _logger.Info($"Deleted {affectedRows} label(s)");
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting label");
                throw;
            }
        }

        //get labels under userid
        public async Task<object> GetbylabelIdAndNotesId(int userid)
        {
            var cacheKey = $"labels_user_{userid}";
            var cachedData = await _cache.StringGetAsync(cacheKey);

            if (!cachedData.IsNullOrEmpty)
            {
                _logger.Info($"Retrieved cached labels for user with ID {userid}");
                return JsonConvert.DeserializeObject<List<label>>(cachedData);
            }

            var query = "SELECT Label.* FROM Label WHERE Label.userid = @userid ";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<label>(query, new { userid= userid });

                    _logger.Info($"Retrieved {result.Count()} label(s) for user with ID {userid}");

                    // Cache the data for future use
                    await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(10));

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occurred while retrieving labels for user with ID {userid}");
                throw;
            }
        }
    }
}
