using Repository.Context;
using Repository.Interface;
using System;
using System.Threading.Tasks;
using Dapper;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using ModelLayer.Entity;

namespace Repository.Service
{
    public class usernotesServiceRepoLogic : IUserNotesRepository
    {
        private readonly DapperContext _context;

        public usernotesServiceRepoLogic(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Addnotes(usernotes notes)
        {
            var query = @"
        INSERT INTO Notes (NoteId, Title, Description, BgColor, ImagePath, Remainder, IsArchive, IsPinned, IsTrash, CreatedAt, ModifiedAt, LabelName, Userid)
        VALUES (@NoteId, @Title, @Description, @BgColor, @ImagePath, @Remainder, @IsArchive, @IsPinned, @IsTrash, @CreatedAt, @ModifiedAt, @LabelName, @Userid)
    ";

            // Execute the query
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new
                {
                    notes.NoteId,
                    notes.Title,
                    notes.Description,
                    notes.BgColor,
                    notes.ImagePath,
                    notes.Remainder,
                    notes.IsArchive,
                    notes.Ispinned,
                    notes.IsTrash, 
                    notes.CreatedAt,
                    notes.ModifiedAt,
                    notes.LabelName,
                    notes.userid
                });
                return affectedRows;
            }

        }
        public async Task<usernotes> GetNotesById(int id)
            {
                var query = "SELECT * FROM Notes WHERE userid = @Id";

                using (var connection = _context.CreateConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<usernotes>(query, new { Id = id });
                }
            }


        public async Task<int> DeletenotesbyId(int userid)
        {
            var query = $"Delete from Notes where userid={userid}";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query, userid);
                return user;
            }
        }

        public async Task<int> EditbynoteId(int Noteid, usernotes note)
        {
            var query = $"update Notes set NoteId=@noteid,Title=@title,Description=@description,BgColor=@bgcolor,ImagePath=@imagepath,Remainder=@remainder,IsArchive=@isarchive,Ispinned=@ispinned," +
                $"IsTrash=@istrash,CreatedAt=@createat,ModifiedAt=@modifiedat,LabelName=@labelname,userid=@userid  where NoteId=@noteid";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query, new { noteid = Noteid, title = note.Title, description = note.Description, bgcolor = note.BgColor, imagepath =note.ImagePath, remainder=note.Remainder, isarchive=note.IsArchive, ispinned=note.Ispinned,
                    istrash=note.IsTrash,
                    createat=note.CreatedAt,
                    modifiedat=note.ModifiedAt,
                    labelname=note.LabelName,
                    userid=note.userid


                });
                return user;
            }
        }




    }
}
