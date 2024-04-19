using Business.Interface;
using ModelLayer.Entity;
using ModelLayer.Request_Body;
using Repository.Interface;
using System.Threading.Tasks;

namespace Business.Service
{
    public class NotesServiceBusinessLogic : INotesBusiness
    {
        private readonly INotesRepository usernotes;

        public NotesServiceBusinessLogic(INotesRepository usernotes)
        {
            this.usernotes = usernotes;
        }

        public Task<int> Addnotes(int userid, NotesBody notes)
        {
            return usernotes.Addnotes(userid,notes);
        }

        public Task<IEnumerable<Notes>> GetNotesById(int UserId)
        {
            return usernotes.GetNotesById(UserId);
        }

        public Task<int> DeletenotesbyId(int userid)
        {
            return usernotes.DeletenotesbyId(userid);
        }
        public Task<int> EditbynoteId(int userid, int Noteid, NotesBody note)
        {
            return usernotes.EditbynoteId(userid,Noteid, note);
        }


    }
}
