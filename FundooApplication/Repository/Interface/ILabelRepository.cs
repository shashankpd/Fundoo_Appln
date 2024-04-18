using ModelLayer.Entity;
using ModelLayer.Request_Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ILabelRepository
    {
        //start

        public Task<int> Addlabel(LabelBody labl,int userid);

        public Task<int> EditbyLabelId(int labelid, LabelBody labl,int userid);

        public Task<int> DeletebyLabelId(int lblid,int userid);

        public Task<object> GetbylabelIdAndNotesId(int userid);

    }
}
