using ModelLayer.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface INotesBusiness
    {
        public Task<int> Addnotes(int userid, Notes notes);

        public Task<IEnumerable<Notes>> GetNotesById(int UserId);

        public Task<int> DeletenotesbyId(int userid);

        public Task<int> EditbynoteId(int userid,int Noteid, Notes note);


    }
}
