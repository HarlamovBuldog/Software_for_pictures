using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesSoft
{
    public class Group
    {
        #region Creation

        public static Group CreateNewGroup()
        {
            return new Group();
        }

        public static Group CreateGroup(
            int id,
            string name,
            string imgPath
            )
        {
            return new Group
            {
                Id = id,
                Name = name,
                ImgPath = imgPath
            };
        }

        protected Group()
        {
        }

        #endregion // Creation

        #region State Properties

        /// <summary>
        /// Gets/sets the unique id for the group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets/sets the name for the group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the group background image's path.  
        /// </summary>
        public string ImgPath { get; set; }

        #endregion // State Properties

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Id, Name, ImgPath);
        }
    }
}
