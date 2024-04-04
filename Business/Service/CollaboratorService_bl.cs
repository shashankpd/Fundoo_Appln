using Business.Interface;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class CollaboratorService_bl : Icollaborator_bl
    {
        private readonly Icollaborator collaborator;

        public CollaboratorService_bl(Icollaborator collaborator)
        {
            this.collaborator = collaborator;
        }
        //start

        public Task<int> Addcollaborator(Collabarator collab)
        {
            return collaborator.Addcollaborator(collab);
        }

        public Task<object> Getbycollabid(int collabid)
        {
            return collaborator.Getbycollabid(collabid);
        }

        public Task<int> Deletebycollabid(int colid)
        {
            return collaborator.Deletebycollabid(colid);
        }


    }
}
