using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserNotesRepository
    {
        public Task<int> Addnotes(usernotes notes);

        public Task<usernotes> GetNotesById(int id);

        public Task<int> DeletenotesbyId(int userid);

        public Task<int> EditbynoteId(int Noteid, usernotes note);
    }
}
