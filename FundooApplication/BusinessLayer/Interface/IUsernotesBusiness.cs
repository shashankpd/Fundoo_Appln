using ModelLayer.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUsernotesBusiness
    {
        public Task<int> Addnotes(usernotes notes);

        public Task<usernotes> GetNotesById(int id);

        public Task<int> DeletenotesbyId(int userid);

        public Task<int> EditbynoteId(int Noteid, usernotes note);


    }
}
