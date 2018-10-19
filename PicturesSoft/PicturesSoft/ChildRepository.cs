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
    /// Represents a source of childs in the application.
    /// </summary>
    public class ChildRepository
    {
        #region Fields

        readonly List<Child> _childs;
        static string _childDataFile;

        #endregion // Fields

        public static string ChildDataFile
        {
            get
            {
                return _childDataFile;
            }
            set
            {
                _childDataFile = value;
            }
        }

        public WorkMode WorkMode { get; set; }

        //public static int GlobalGroupCode { get; private set; }

        public static XNamespace Xmlns { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new repository of childs.
        /// </summary>
        /// <param name="childDataFile">The relative path to an XML resource file that contains child data.</param>
        public ChildRepository(string childDataFile)
        {
            ChildDataFile = childDataFile;
            _childs = LoadChilds();
        }

        public ChildRepository(string childDataFile, WorkMode workMode)
        {
            WorkMode = workMode;
            ChildDataFile = childDataFile;

            if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                Xmlns = XNamespace.Get("http://crystals.ru/cash/settings");
                _childs = LoadChildsFromFinalXml();
            }
            else
            {
                _childs = LoadChilds();
            }
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

                if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml)
                {
                    AddChildToFinaXml(child);
                }
                else
                {
                    AddChildToXml(child);
                }

                // if (this.ChildAdded != null)
                //    this.ChildAdded(this, new ChildAddedEventArgs(child));
            }
        }

        public void UpdateChild(Child childToUpdate, Child oldChild, bool needToUpdateInFinalXml = true)
        {
            if (childToUpdate == null)
                throw new ArgumentNullException("child");

            int oldChildListIndex = _childs.IndexOf(oldChild);


            if (WorkMode.WorkType == WorkModeType.LoadFromFinalXml
                 && needToUpdateInFinalXml)
            {
                UpgradeChildInFinalXml(childToUpdate, oldChild);
            }
            else if (WorkMode.WorkType != WorkModeType.LoadFromFinalXml)
            {
                UpdateChildInXaml(childToUpdate, oldChildListIndex);
            }

            //< Childs list update
            _childs[oldChildListIndex].Code = childToUpdate.Code;
            _childs[oldChildListIndex].Name = childToUpdate.Name;
            _childs[oldChildListIndex].SimpleName = childToUpdate.SimpleName;
            _childs[oldChildListIndex].GroupCode = childToUpdate.GroupCode;
            _childs[oldChildListIndex].ImgName = childToUpdate.ImgName;
            //>

            
        }

        /// <summary>
        /// Deletes elements from repository of childs and from FinalXml file if
        /// needToDeleteInFinalXml param is true (by default it is).
        /// </summary>
        /// <param name="needToDeleteInFinalXml">
        /// Determines whether or not to delete element from final xml file.
        /// </param>
        /// 
        public void DeleteChild(Child childToDelete, bool needToDeleteInFinalXml = true)
        {
            int childListIndexToDelete = _childs.IndexOf(childToDelete);

            if(WorkMode.WorkType == WorkModeType.LoadFromFinalXml
                && needToDeleteInFinalXml)
            {
                DeleteChildInFinalXaml(childToDelete);
            }
            else if (WorkMode.WorkType != WorkModeType.LoadFromFinalXml)
            {
                DeleteChildInXaml(childListIndexToDelete);
            }

            _childs.RemoveAt(childListIndexToDelete);

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

        //< Final Xml methods

        private static List<Child> LoadChildsFromFinalXml()
        {
            List<Child> loadedChilds = new List<Child>();

            XDocument xDoc = XDocument.Load(ChildDataFile);

            foreach (XElement groupElem in xDoc.Element(Xmlns + "moduleConfig").
                Element(Xmlns + "property").Elements(Xmlns + "group"))
            {
                foreach(XElement childElem in groupElem.Elements(Xmlns + "good"))
                {
                    loadedChilds.Add(Child.CreateChild(
                    (int)childElem.Attribute("item"),
                    (string)childElem.Attribute("name"),
                    (string)childElem.Attribute("name"),
                    (int)groupElem.Attribute("id"),
                    (string)childElem.Attribute("item") + ".png"
                    ));
                }

            }

            return loadedChilds;
        }

        private void AddChildToFinaXml(Child newChild)
        {
            XDocument xDoc = XDocument.Load(ChildDataFile);

            XElement groupXElemChildOwner = xDoc.Element(Xmlns + "moduleConfig").
                Element(Xmlns + "property").Elements(Xmlns + "group")
                .Single(x => (int)x.Attribute("id") == newChild.GroupCode);

            groupXElemChildOwner.Add(new XElement(Xmlns + "good",
                   new XAttribute("item", newChild.Code),
                   new XAttribute("name", newChild.SimpleName)
                   ));

            xDoc.Save(ChildDataFile);
        }

        private void UpgradeChildInFinalXml(Child childToUpdate, Child oldChild)
        {
            XDocument xDoc = XDocument.Load(ChildDataFile);

            XElement childXElemToUpdate = xDoc.Element(Xmlns + "moduleConfig").
               Element(Xmlns + "property").Elements(Xmlns + "group")
               .Single(x => (int)x.Attribute("id") == oldChild.GroupCode).Elements(Xmlns + "good")
               .Single(x => (int)x.Attribute("item") == oldChild.Code);

            childXElemToUpdate.Attribute("name").Value = childToUpdate.SimpleName;
            childXElemToUpdate.Attribute("item").Value = childToUpdate.Code.ToString();

            xDoc.Save(ChildDataFile);
        }

        private void DeleteChildInFinalXaml(Child childToDelete)
        {
            XDocument xDoc = XDocument.Load(ChildDataFile);

            XElement childXElemToDelete = xDoc.Element(Xmlns + "moduleConfig").
               Element(Xmlns + "property").Elements(Xmlns + "group")
               .Single(x => (int)x.Attribute("id") == childToDelete.GroupCode).Elements(Xmlns + "good")
               .Single(x => (int)x.Attribute("item") == childToDelete.Code);

            childXElemToDelete.Remove();

            xDoc.Save(ChildDataFile);
        }

        //> Final Xml methods

        static List<Child> LoadChilds()
        {
                return
                    (from childElem in XDocument.Load(ChildDataFile).Element("childs").Elements("child")
                     select Child.CreateChild(
                        (int)childElem.Attribute("code"),
                        (string)childElem.Attribute("name"),
                        (string)childElem.Attribute("simpleName"),
                        (int)childElem.Attribute("groupCode"),
                        (string)childElem.Attribute("imgName")
                         )).ToList();
        }

        private void AddChildToXml(Child newChild)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(ChildDataFile);
            XmlElement xRoot = xDoc.DocumentElement;

            //< create new element child
            XmlElement childElement = xDoc.CreateElement("child");
            //>

            //< create attributes (elements) inside child
            XmlAttribute codeAttr = xDoc.CreateAttribute("code");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlAttribute simpleNameAttr = xDoc.CreateAttribute("simpleName");
            XmlAttribute groupCodeAttr = xDoc.CreateAttribute("groupCode");
            XmlAttribute imgNameAttr = xDoc.CreateAttribute("imgName");
            //>

            //< create text values for attributes
            XmlText codeText = xDoc.CreateTextNode(newChild.Code.ToString());
            XmlText nameText = xDoc.CreateTextNode(newChild.Name);
            XmlText simpleNameText = xDoc.CreateTextNode(newChild.SimpleName);
            XmlText groupCodeText = xDoc.CreateTextNode(newChild.GroupCode.ToString());
            XmlText imgNameText = xDoc.CreateTextNode(newChild.ImgName);
            //>

            //< add nodes
            codeAttr.AppendChild(codeText);
            nameAttr.AppendChild(nameText);
            simpleNameAttr.AppendChild(simpleNameText);
            groupCodeAttr.AppendChild(groupCodeText);
            imgNameAttr.AppendChild(imgNameText);
            //>

            childElement.Attributes.Append(codeAttr);
            childElement.Attributes.Append(nameAttr);
            childElement.Attributes.Append(simpleNameAttr);
            childElement.Attributes.Append(groupCodeAttr);
            childElement.Attributes.Append(imgNameAttr);

            xRoot.AppendChild(childElement);
            xDoc.Save(ChildDataFile);
        }

        private void UpdateChildInXaml(Child childToUpdate, int childLocationId)
        {
            #region Create new child
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(ChildDataFile);
            XmlElement xRoot = xDoc.DocumentElement;

            //< create new element child
            XmlElement childElement = xDoc.CreateElement("child");
            //>

            //< create attributes (elements) inside child
            XmlAttribute codeAttr = xDoc.CreateAttribute("code");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlAttribute simpleNameAttr = xDoc.CreateAttribute("simpleName");
            XmlAttribute groupCodeAttr = xDoc.CreateAttribute("groupCode");
            XmlAttribute imgNameAttr = xDoc.CreateAttribute("imgName");
            //>

            //< create text values for attributes
            XmlText codeText = xDoc.CreateTextNode(childToUpdate.Code.ToString());
            XmlText nameText = xDoc.CreateTextNode(childToUpdate.Name);
            XmlText simpleNameText = xDoc.CreateTextNode(childToUpdate.SimpleName);
            XmlText groupCodeText = xDoc.CreateTextNode(childToUpdate.GroupCode.ToString());
            XmlText imgNameText = xDoc.CreateTextNode(childToUpdate.ImgName);
            //>

            //< add nodes
            codeAttr.AppendChild(codeText);
            nameAttr.AppendChild(nameText);
            simpleNameAttr.AppendChild(simpleNameText);
            groupCodeAttr.AppendChild(groupCodeText);
            imgNameAttr.AppendChild(imgNameText);
            //>

            childElement.Attributes.Append(codeAttr);
            childElement.Attributes.Append(nameAttr);
            childElement.Attributes.Append(simpleNameAttr);
            childElement.Attributes.Append(groupCodeAttr);
            childElement.Attributes.Append(imgNameAttr);
            #endregion //Create new child

            //actual update
            int countId = 0;

            foreach (XmlNode xnode in xRoot)
            {
                if (countId == childLocationId)
                {
                    xRoot.ReplaceChild(childElement, xnode);
                    break;
                }

                countId++;
            }

            xDoc.Save(ChildDataFile);
        }

        private void DeleteChildInXaml(int childLocationId)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(ChildDataFile);
            XmlElement xRoot = xDoc.DocumentElement;

            //actual remove
            int countId = 0;

            foreach (XmlNode xnode in xRoot)
            {
                if (countId == childLocationId)
                {
                    xRoot.RemoveChild(xnode);
                    break;
                }

                countId++;
            }

            xDoc.Save(ChildDataFile);
        }

        #endregion // Private Helpers
    }
}
