using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface Icollaborator_bl
    {
        //start

        public Task<int> Addcollaborator(Collabarator collab);

        public Task<object> Getbycollabid(int collabid);

        public Task<int> Deletebycollabid(int colid);
    }
}
