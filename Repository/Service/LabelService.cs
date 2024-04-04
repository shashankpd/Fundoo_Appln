using Dapper;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Service
{
    public class LabelService : Ilabel
    {
        private readonly DapperContext _context;

        public LabelService(DapperContext context)
        {
            _context = context;
        }

        //start

        public async Task<int> Addlabel(label labl)
        { 
        var query = @"
        INSERT INTO Label (  LabelName, userid,NoteId)
        VALUES ( @labelname, @userid,@noteid)";

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new {labl.LabelId,labl.LabelName,labl.userid,labl.NoteId });

                return affectedRows;
            }
        }

        public async Task<int> EditbyLabelId(int labelid, label labl)
        {
            var query = $"update Label set LabelName=@labelname,userid=@userid,NoteId=@noteid where LabelId=@labelid";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query, new
                {
                    labelid = labelid,
                    labelname = labl.LabelName,
                   userid =labl.userid,
                    noteid = labl.NoteId

                });
                return user;
            }
        }

        public async Task<int> DeletebyLabelId(int lblid)
        {
            var query = $"Delete from Label where LabelId={lblid}";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query, lblid);
                return user;
            }
        }

        //get labels by userid
        public async Task<object> GetbylabelIdAndNotesId(int userid)
        {
            var query = @"
        SELECT Label.* 
        FROM Label
        WHERE Label.userid = @userid";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync(query, new { userid });

                // Return the dynamic result
                return result;
            }
        }




    }
}
