﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace PicturesSoft
{
    /// <summary>
    /// Represents a source of childs in the application.
    /// </summary>
    public class ChildRepository
    {
        #region Fields

        readonly List<Child> _childs;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of childs.
        /// </summary>
        /// <param name="childDataFile">The relative path to an XML resource file that contains child data.</param>
        public ChildRepository(string childDataFile)
        {
            _childs = LoadChilds(childDataFile);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a child is placed into the repository.
        /// </summary>
        //public event EventHandler<ChildAddedEventArgs> ChildAdded;

        /// <summary>
        /// Places the specified child into the repository.
        /// If the child is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void AddChild(Child child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            if (!_childs.Contains(child))
            {
                _childs.Add(child);

                // if (this.ChildAdded != null)
                //    this.ChildAdded(this, new ChildAddedEventArgs(child));
            }
        }

        /// <summary>
        /// Returns true if the specified child exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsChild(Child child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            return _childs.Contains(child);
        }

        /// <summary>
        /// Returns a shallow-copied list of all childs in the repository.
        /// </summary>
        public List<Child> GetChilds()
        {
            return new List<Child>(_childs);
        }

        public List<Child> GetChildsBelongToGroup(int groupId)
        {
            List<Child> children = new List<Child>();
            foreach (Child child in _childs)
            {
                if (child.GroupCode == groupId)
                {
                    children.Add(child);
                }
            }
            return children;
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<Child> LoadChilds(string childDataFile)
        {
                return
                    (from childElem in XDocument.Load(childDataFile).Element("childs").Elements("child")
                     select Child.CreateChild(
                        (int)childElem.Attribute("code"),
                        (string)childElem.Attribute("name"),
                        (string)childElem.Attribute("simpleName"),
                        (int)childElem.Attribute("groupCode"),
                        (string)childElem.Attribute("imgName")
                         )).ToList();
        }

        static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);
            Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(uri.ToString());
            //StreamResourceInfo info = Application.GetResourceStream(uri);
            if (resource == null || resource == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return resource;
        }

        #endregion // Private Helpers
    }
}
