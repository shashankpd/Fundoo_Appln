using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface Icollaborator
    {
        //start

        public Task<int> Addcollaborator(Collabarator collab);

        public Task<object> Getbycollabid(int collabid);

        public Task<int> Deletebycollabid(int colid);
    }
}
