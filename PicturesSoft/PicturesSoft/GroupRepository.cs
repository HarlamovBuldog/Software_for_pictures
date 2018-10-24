using System;
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

        public WorkMode WorkMode { get; set; }

        public static int GlobalId { get; private set; }

        public static XNamespace Xmlns { get; set; }

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

        public GroupRepository(string groupDataFile, WorkMode workMode)
        {
            WorkMode = workMode;
            GroupDataFile = groupDataFile;

            if(WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                Xmlns = XNamespace.Get("http://crystals.ru/cash/settings");
                GlobalId = 1;
                _groups = LoadGroupsFromFinalXml();
            }
            else
            {
                _groups = LoadGroups();
            }   
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

                if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
                {
                    AddGroupToFinaXml(group);
                }
                else
                {
                    AddGroupToXml(group);
                }
            }
        }

        public void AddGroup(Group newGroup, Group groupToInsertAfter)
        {
            if (newGroup == null)
                throw new ArgumentNullException("group");

            if (!_groups.Contains(newGroup))
            {
                int groupIndexToInsertAfter = _groups.IndexOf(groupToInsertAfter);
                _groups.Insert(groupIndexToInsertAfter + 1, newGroup);

                if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
                {
                    AddGroupToFinaXml(newGroup, groupToInsertAfter);
                }
                else
                {
                    AddGroupToXml(newGroup);
                }
            }
        }

        public void UpdateGroup(Group groupToUpdate, Group oldGroup)
        {
            if (groupToUpdate == null)
                throw new ArgumentNullException("group");

            int oldGroupListIndex = _groups.IndexOf(oldGroup);


            if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                UpgradeGroupInFinalXml(groupToUpdate, oldGroup);
            }
            else
            {
                UpdateGroupInXaml(groupToUpdate, oldGroupListIndex);
            }

            //< Groups list update
            _groups[oldGroupListIndex].Id = groupToUpdate.Id;
            _groups[oldGroupListIndex].Name = groupToUpdate.Name;
            _groups[oldGroupListIndex].ImgName = groupToUpdate.ImgName;
            //>

                
        }

        public void DeleteGroup(Group groupToDelete)
        {
            int groupToDeleteListIndex = _groups.IndexOf(groupToDelete);

            if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                DeleteGroupInFinalXaml(groupToDelete);
            }
            else
            {
                DeleteGroupInXaml(groupToDeleteListIndex);
            }

            _groups.RemoveAt(groupToDeleteListIndex); 
        }

        public void SwapGroups(int firstGroupIndexToSwap, int secondGroupIndexToSwap)
        {
            SwapGroupsInFinalXmlFile(_groups[firstGroupIndexToSwap], _groups[secondGroupIndexToSwap]);

            var tempGroup = _groups[firstGroupIndexToSwap];
            _groups[firstGroupIndexToSwap] = _groups[secondGroupIndexToSwap];
            _groups[secondGroupIndexToSwap] = tempGroup;
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

        public bool ValidateGroupId(int groupIdToValidate)
        {
            bool isValid = true;

            foreach (var gr in _groups)
            {
                if (gr.Id == groupIdToValidate)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        #endregion // Public Interface

        #region Private Helpers

        //< Final Xml methods
        private static List<Group> LoadGroupsFromFinalXml()
        {
            List<Group> loadedGroups = new List<Group>();

            XDocument xDoc = XDocument.Load(GroupDataFile);

            foreach(XElement groupElem in xDoc.Element(Xmlns + "moduleConfig").
                Element(Xmlns + "property").Elements(Xmlns + "group"))
            {
                //adding attribute id to group element if it doesn't exist
                if(groupElem.Attribute("id") == null)
                {
                    groupElem.Add(new XAttribute("id", GlobalId));
                }

                loadedGroups.Add(Group.CreateGroup(
                    (int)groupElem.Attribute("id"),
                    (string)groupElem.Attribute("name"),
                    (string)groupElem.Attribute("image-name")
                    ));
                GlobalId++;
            }

            xDoc.Save(GroupDataFile);

            return loadedGroups;
        }

        private void AddGroupToFinaXml(Group newGroup)
        {
            XDocument xDoc = XDocument.Load(GroupDataFile);
            XElement groupRoot = xDoc.Element(Xmlns + "moduleConfig").Element(Xmlns + "property");

            groupRoot.Add(new XElement(Xmlns + "group",
                new XAttribute("name", newGroup.Name),
                new XAttribute("image-name", newGroup.ImgName),
                new XAttribute("id", newGroup.Id)
                ));

            xDoc.Save(GroupDataFile);
        }
        
        private void AddGroupToFinaXml(Group newGroup, Group groupToInsertAfter)
        {
            XDocument xDoc = XDocument.Load(GroupDataFile);

            XElement groupXElemToInsertAfter = xDoc.Element(Xmlns + "moduleConfig").
                Element(Xmlns + "property").Elements(Xmlns + "group")
                .Single(x => (int)x.Attribute("id") == groupToInsertAfter.Id);

            groupXElemToInsertAfter.AddAfterSelf(new XElement(Xmlns + "group",
                new XAttribute("name", newGroup.Name),
                new XAttribute("image-name", newGroup.ImgName),
                new XAttribute("id", newGroup.Id)
                ));

            xDoc.Save(GroupDataFile);
        }


        private void UpgradeGroupInFinalXml(Group groupToUpdate, Group oldGroup)
        {
            XDocument xDoc = XDocument.Load(GroupDataFile);

            XElement groupXElemToUpdate = xDoc.Element(Xmlns + "moduleConfig").
                Element(Xmlns + "property").Elements(Xmlns + "group")
                .Single(x => (int)x.Attribute("id") == oldGroup.Id);

            groupXElemToUpdate.Attribute("name").Value = groupToUpdate.Name;
            groupXElemToUpdate.Attribute("image-name").Value = groupToUpdate.ImgName;
            groupXElemToUpdate.Attribute("id").Value = groupToUpdate.Id.ToString();

            xDoc.Save(GroupDataFile);
        }

        private void DeleteGroupInFinalXaml(Group groupToDelete)
        {
            XDocument xDoc = XDocument.Load(GroupDataFile);

            XElement groupXElemToDelete = xDoc.Element(Xmlns + "moduleConfig").
                Element(Xmlns + "property").Elements(Xmlns + "group")
                .Single(x => (int)x.Attribute("id") == groupToDelete.Id);

            groupXElemToDelete.Remove();

            xDoc.Save(GroupDataFile);
        }

        private void SwapGroupsInFinalXmlFile(Group firstGroupToSwap, Group secondGroupToSwap)
        {
            XDocument xDoc = XDocument.Load(GroupDataFile);

            XElement firstGroupXElemToSwap = xDoc.Element(Xmlns + "moduleConfig").
               Element(Xmlns + "property").Elements(Xmlns + "group")
               .Single(x => (int)x.Attribute("id") == firstGroupToSwap.Id);

            XElement secondGroupXElemToSwap = xDoc.Element(Xmlns + "moduleConfig").
               Element(Xmlns + "property").Elements(Xmlns + "group")
               .Single(x => (int)x.Attribute("id") == secondGroupToSwap.Id);

            XElement tempXElement = new XElement(firstGroupXElemToSwap);

            firstGroupXElemToSwap.ReplaceWith(secondGroupXElemToSwap);
            secondGroupXElemToSwap.ReplaceWith(tempXElement);

            xDoc.Save(GroupDataFile);
        }

        //> Final Xml methods

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
