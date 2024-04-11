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
    public class LabelServiceBusinessLogic : ILabelBusiness
    {

        private readonly ILabelRepository label;

        public LabelServiceBusinessLogic(ILabelRepository label)
        {
            this.label = label;
        }
        //start

        public Task<int> Addlabel(label labl)
        {
            return label.Addlabel( labl);
        }

        public Task<int> EditbyLabelId(int labelid, label labl)
        {
            return label.EditbyLabelId(labelid, labl);
        }

        public Task<int> DeletebyLabelId(int lblid)
        {
            return label.DeletebyLabelId(lblid);
        }

        public Task<object> GetbylabelIdAndNotesId(int userid)
        {
            return label.GetbylabelIdAndNotesId(userid);
        }


    }
}
