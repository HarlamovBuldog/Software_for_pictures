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
    /// Represents a source of groups in the application.
    /// </summary>
    public class GroupRepository
    {
        #region Fields

        readonly List<Group> _groups;
        static string _groupDataFile;

        #endregion // Fields

        public static string GroupDataFile
        {
            get
            {
                return _groupDataFile;
            }
            set
            {
                _groupDataFile = value;
            }
        }

        #region Constructor

        /// <summary>
        /// Creates a new repository of groups.
        /// </summary>
        /// <param name="groupDataFile">The relative path to an XML resource file that contains group data.</param>
        public GroupRepository(string groupDataFile)
        {
            GroupDataFile = groupDataFile;
            _groups = LoadGroups(); 
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a group is placed into the repository.
        /// </summary>
        //public event EventHandler<GroupAddedEventArgs> GroupAdded;

        /// <summary>
        /// Places the specified group into the repository.
        /// If the group is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void AddGroup(Group group)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            if (!_groups.Contains(group))
            {
                _groups.Add(group);

                AddGroupToXml(group);

               // if (this.GroupAdded != null)
                //    this.GroupAdded(this, new GroupAddedEventArgs(group));
            }
        }

        public void UpdateGroup(Group groupToUpdate, int groupListIndex)
        {
            if (groupToUpdate == null)
                throw new ArgumentNullException("group");

            //< Groups list update
            _groups[groupListIndex].Id = groupToUpdate.Id;
            _groups[groupListIndex].Name = groupToUpdate.Name;
            _groups[groupListIndex].ImgName = groupToUpdate.ImgName;
            //>

            UpdateGroupInXaml(groupToUpdate, groupListIndex);
        }

        public void DeleteGroup(int groupListIndex)
        {
            _groups.RemoveAt(groupListIndex);
            DeleteGroupInXaml(groupListIndex);
        }

        /// <summary>
        /// Returns true if the specified group exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsGroup(Group group)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            return _groups.Contains(group);
        }

        /// <summary>
        /// Returns a shallow-copied list of all groups in the repository.
        /// </summary>
        public List<Group> GetGroups()
        {
            return new List<Group>(_groups);
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<Group> LoadGroups()
        {
                return
                    (from groupElem in XDocument.Load(GroupDataFile).Element("groups").Elements("group")
                     select Group.CreateGroup(
                        (int)groupElem.Attribute("id"),
                        (string)groupElem.Attribute("name"),
                        (string)groupElem.Attribute("imgName")
                         )).ToList();
        }

        private void AddGroupToXml(Group newGroup)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(GroupDataFile);
            XmlElement xRoot = xDoc.DocumentElement;

            //< create new element group
            XmlElement groupElement = xDoc.CreateElement("group");
            //>

            //< create attributes (elements) inside group
            XmlAttribute idAttr = xDoc.CreateAttribute("id");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlAttribute imgNameAttr = xDoc.CreateAttribute("imgName");
            //>

            //< create text values for attributes
            XmlText idText = xDoc.CreateTextNode(newGroup.Id.ToString());
            XmlText nameText = xDoc.CreateTextNode(newGroup.Name);
            XmlText imgNameText = xDoc.CreateTextNode(newGroup.ImgName);
            //>

            //< add nodes
            idAttr.AppendChild(idText);
            nameAttr.AppendChild(nameText);
            imgNameAttr.AppendChild(imgNameText);
            //>

            groupElement.Attributes.Append(idAttr);
            groupElement.Attributes.Append(nameAttr);
            groupElement.Attributes.Append(imgNameAttr);

            xRoot.AppendChild(groupElement);
            xDoc.Save(GroupDataFile);
        }

        private void UpdateGroupInXaml(Group groupToUpdate, int groupLocationId)
        {
            #region Create new group
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(GroupDataFile);
            XmlElement xRoot = xDoc.DocumentElement;

            //< create new element group
            XmlElement groupElement = xDoc.CreateElement("group");
            //>

            //< create attributes (elements) inside group
            XmlAttribute idAttr = xDoc.CreateAttribute("id");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlAttribute imgNameAttr = xDoc.CreateAttribute("imgName");
            //>

            //< create text values for attributes
            XmlText idText = xDoc.CreateTextNode(groupToUpdate.Id.ToString());
            XmlText nameText = xDoc.CreateTextNode(groupToUpdate.Name);
            XmlText imgNameText = xDoc.CreateTextNode(groupToUpdate.ImgName);
            //>

            //< add nodes
            idAttr.AppendChild(idText);
            nameAttr.AppendChild(nameText);
            imgNameAttr.AppendChild(imgNameText);
            //>

            groupElement.Attributes.Append(idAttr);
            groupElement.Attributes.Append(nameAttr);
            groupElement.Attributes.Append(imgNameAttr);
            #endregion //Create new group

            //actual update
            int countId = 0;

            foreach (XmlNode xnode in xRoot)
            {
                if(countId == groupLocationId)
                {
                    xRoot.ReplaceChild(groupElement, xnode);
                    break;
                }

                countId++;
            }

            xDoc.Save(GroupDataFile);
        }

        private void DeleteGroupInXaml(int groupLocationId)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(GroupDataFile);
            XmlElement xRoot = xDoc.DocumentElement;

            //actual remove
            int countId = 0;

            foreach (XmlNode xnode in xRoot)
            {
                if (countId == groupLocationId)
                {
                    xRoot.RemoveChild(xnode);
                    break;
                }

                countId++;
            }

            xDoc.Save(GroupDataFile);
        }

        #endregion // Private Helpers
    }
}
