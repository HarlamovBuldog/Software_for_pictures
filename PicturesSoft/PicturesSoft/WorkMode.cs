using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesSoft
{
    public class WorkMode
    {
        public WorkModeType WorkType { get; set; }
    }

    public enum WorkModeType
    {
        Create,
        Edit,
        LoadFromFinalXml
    }
}
