using Business.Interface;
using Repository.Entity;
using Repository.Interface;
using System.Threading.Tasks;

namespace Business.Service
{
    public class usernotesService_bl : Iusernotes_bl
    {
        private readonly Iusernotes usernotes;

        public usernotesService_bl(Iusernotes usernotes)
        {
            this.usernotes = usernotes;
        }

        public Task<int> Addnotes(usernotes notes)
        {
            return usernotes.Addnotes(notes);
        }

        public Task<usernotes> GetNotesById(int id)
        {
            return usernotes.GetNotesById(id);
        }

        public Task<int> DeletenotesbyId(int userid)
        {
            return usernotes.DeletenotesbyId(userid);
        }
        public Task<int> EditbynoteId(int Noteid, usernotes note)
        {
            return usernotes.EditbynoteId(Noteid, note);
        }


    }
}
