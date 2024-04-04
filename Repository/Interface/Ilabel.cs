using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface Ilabel
    {
        //start

        public Task<int> Addlabel(label labl);

        public Task<int> EditbyLabelId(int labelid, label labl);

        public Task<int> DeletebyLabelId(int lblid);

        public Task<object> GetbylabelIdAndNotesId(int userid);

    }
}
