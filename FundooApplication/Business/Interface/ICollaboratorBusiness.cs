﻿using ModelLayer.Entity;
using ModelLayer.Request_Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ICollaboratorBusiness
    {
        //start

        public Task<int> Addcollaborator(CollaboratorBody collab,int userid);

        public Task<IEnumerable<Collabarator>> Getbycollabid(int collabid, int userid);

        public Task<int> Deletebycollabid(int colid,int userid);
    }
}
