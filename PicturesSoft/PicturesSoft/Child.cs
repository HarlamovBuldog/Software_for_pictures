using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesSoft
{
    public class Child
    {
        #region Creation

        public static Child CreateNewChild()
        {
            return new Child();
        }

        public static Child CreateChild(
            int code,
            string name,
            string simpleName,
            int groupCode,
            string imgName
            )
        {
            return new Child
            {
                Code = code,
                Name = name,
                SimpleName = simpleName,
                GroupCode = groupCode,
                ImgName = imgName
            };
        }

        protected Child()
        {
        }

        #endregion // Creation

        #region State Properties

        /// <summary>
        /// Gets/sets the unique code for the child.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets/sets the name for the child.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the simpleName for the child.
        /// </summary>
        public string SimpleName { get; set; }

        /// <summary>
        /// Gets/sets the GroupCode to which the child belongs.
        /// </summary>
        public int GroupCode { get; set; }

        /// <summary>
        /// Gets/sets the child container background image's path.  
        /// </summary>
        public string ImgName { get; set; }

        #endregion // State Properties
    }
}
