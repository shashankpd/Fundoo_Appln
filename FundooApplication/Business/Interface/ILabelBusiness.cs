using ModelLayer.Entity;
using ModelLayer.Request_Body;
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

        public Task<int> Addlabel(LabelBody labl, int userid);

        public Task<int> EditbyLabelId(int labelid, LabelBody labl, int userid);

        public Task<int> DeletebyLabelId(int LabelId,int userid);

        public Task<object> GetbylabelIdAndNotesId(int userid);
    }

}
