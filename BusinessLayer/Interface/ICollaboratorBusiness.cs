﻿using ModelLayer.Entity;
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

        public Task<int> Addcollaborator(Collabarator collab);

        public Task<object> Getbycollabid(int collabid);

        public Task<int> Deletebycollabid(int colid);
    }
}
