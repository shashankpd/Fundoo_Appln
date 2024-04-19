using ModelLayer.Entity;
using ModelLayer.Request_Body;
using Repository.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IcollaboratorRepository
    {
        //start

        public Task<int> Addcollaborator(CollaboratorBody collab,int userid);

        public Task<IEnumerable<Collabarator>> Getbycollabid(int collabid,int userid);

        public Task<int> Deletebycollabid(int colid,int userid);
    }
}
