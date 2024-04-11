using Business.Interface;
using ModelLayer.Entity;
using Repository.Interface;
using System.Threading.Tasks;

namespace Business.Service
{
    public class UserNotesServiceBusinessLogic : IUsernotesBusiness
    {
        private readonly IUserNotesRepository usernotes;

        public UserNotesServiceBusinessLogic(IUserNotesRepository usernotes)
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
