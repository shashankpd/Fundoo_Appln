using Business.Interface;
using ModelLayer.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class CollaboratorServiceBusinessLogic : ICollaboratorBusiness
    {
        private readonly IcollaboratorRepository collaborator;

        public CollaboratorServiceBusinessLogic(IcollaboratorRepository collaborator)
        {
            this.collaborator = collaborator;
        }
        //start

        public Task<int> Addcollaborator(Collabarator collab, int userid)
        {
            return collaborator.Addcollaborator(collab,userid);
        }

        public Task<IEnumerable<Collabarator>> Getbycollabid(int collabid, int userid)
        {
            return collaborator.Getbycollabid(collabid,userid);
        }

        public Task<int> Deletebycollabid(int colid,int userid)
        {
            return collaborator.Deletebycollabid(colid,userid);
        }


    }
}
