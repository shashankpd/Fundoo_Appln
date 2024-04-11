using Dapper;
using Microsoft.Extensions.Logging;
using ModelLayer.Entity;
using Repository.Context;
using Repository.Interface;
using NLog;
using System;
using System.Threading.Tasks;
using ILogger = NLog.ILogger;

namespace Repository.Service
{
    public class LabelServiceRepoLogic : ILabelRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<LabelServiceRepoLogic> _logger;  // Specify NLog.ILogger here

        public LabelServiceRepoLogic(DapperContext context, ILogger<LabelServiceRepoLogic> logger) // Specify NLog.ILogger here
        {
            _context = context;
            _logger = logger;
        }

        //start

        public async Task<int> Addlabel(label labl)
        {
            var query = @"
                INSERT INTO Label (LabelName, userid, NoteId)
                VALUES (@labelname, @userid, @noteid)";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new { labl.LabelName, labl.userid, labl.NoteId });
                    _logger.LogInformation($"Added {affectedRows} label(s)");
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding label");
                throw;
            }
        }

        public async Task<int> EditbyLabelId(int labelid, label labl)
        {
            var query = @"UPDATE Label 
                          SET LabelName = @labelname, userid = @userid, NoteId = @noteid 
                          WHERE LabelId = @labelid";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new { labelid, labl.LabelName, labl.userid, labl.NoteId });
                    _logger.LogInformation($"Edited {affectedRows} label(s)");
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing label");
                throw;
            }
        }

        public async Task<int> DeletebyLabelId(int lblid)
        {
            var query = "DELETE FROM Label WHERE LabelId = @lblid";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new { lblid });
                    _logger.LogInformation($"Deleted {affectedRows} label(s)");
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting label");
                throw;
            }
        }

        //get labels under userid
        public async Task<object> GetbylabelIdAndNotesId(int userid)
        {
            var query = "SELECT Label.* FROM Label WHERE Label.userid = @userid";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync(query, new { userid });
                    _logger.LogInformation($"Retrieved {result.Count()} label(s) for user with ID {userid}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving labels for user with ID {userid}");
                throw;
            }
        }
    }
}
