using Business.Interface;
using ModelLayer.Entity;
using ModelLayer.Request_Body;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class LabelServiceBusinessLogic : ILabelBusiness
    {

        private readonly ILabelRepository label;

        public LabelServiceBusinessLogic(ILabelRepository label)
        {
            this.label = label;
        }
        //start

        public Task<int> Addlabel(LabelBody labl, int userid)
        {
            return label.Addlabel( labl, userid);
        }

        public Task<int> EditbyLabelId(int labelid, LabelBody labl, int userid)
        {
            return label.EditbyLabelId(labelid, labl,userid);
        }

        public Task<int> DeletebyLabelId(int LabelId,int userid)
        {
            return label.DeletebyLabelId(LabelId,userid);
        }

        public Task<object> GetbylabelIdAndNotesId(int userid)
        {
            return label.GetbylabelIdAndNotesId(userid);
        }


    }
}
