using ModelLayer.Entity;
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

        public Task<int> Addcollaborator(Collabarator collab,int userid);

        public Task<IEnumerable<Collabarator>> Getbycollabid(int collabid,int userid);

        public Task<int> Deletebycollabid(int colid,int userid);
    }
}
