
using System;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace revisual.pipit.data
{
    public enum ProjectType
    {
        LIST,
        PROJECT,
        BOOK

    }

    public interface IProjectList
    {
        ProjectItem Get(int index);

        int Length
        {
            get;
        }

        Signal<ProjectItem> clicked
        {
            get;
        }
    }

    public interface IClickableItem
    {
        void Click();
    }

    public class ProjectList : IProjectList
    {
        private Signal<ProjectItem> _clicked = new Signal<ProjectItem>();

        public Signal<ProjectItem> clicked
        {
            get
            {
                return _clicked;
            }
        }

        public ProjectList(int length)
        {
            _items = new ProjectItem[length];
            _count = 0;
        }

        public ProjectItem Get(int index)
        {
            return _items[index];
        }

        private ProjectItem[] _items;

        private uint _count;

        internal ProjectItem Add(string id)
        {
            ProjectItem item = new ProjectItem(id, _clicked);
            _items[_count++] = item;
            return item;
        }

        public int Length
        {
            get
            {
                return _items.Length;
            }
        }

    }

    public class ProjectItem : IClickableItem
    {
        internal ProjectType type;
        internal string id;
        internal string title;
        internal string thumbnailURL;
        internal string description;
        internal uint priority;

        private Signal<ProjectItem> _clicked;



        public ProjectItem(string id, Signal<ProjectItem> clicked)
        {
            this.id = id;
            _clicked = clicked;
        }

        public string GetTitle()
        {
            return title;
        }

        public ProjectType GetProjectType()
        {
            return type;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetID()
        {
            return id;
        }

        public void Click()
        {
            if (_clicked == null) return;
            _clicked.Dispatch(this);
        }
    }
}



