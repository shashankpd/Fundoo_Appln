using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ILabelBusiness
    {

        //start

        public Task<int> Addlabel(label labl);

        public Task<int> EditbyLabelId(int labelid, label labl);

        public Task<int> DeletebyLabelId(int lblid);

        public Task<object> GetbylabelIdAndNotesId(int userid);
    }

}
