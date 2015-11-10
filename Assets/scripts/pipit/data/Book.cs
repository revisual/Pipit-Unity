
using strange.extensions.signal.impl;
using UnityEngine;

namespace revisual.pipit.data
{
    public interface IBook
    {
        BookImage Get(int index);

        Signal<BookImage> completed
        {
            get;
        }

        int Length
        {
            get;
        }
    }

    public class Book : IBook
    {
        public Book(int length)
        {
            _images = new BookImage[length];
            _completed = new Signal<BookImage>();
            _count = 0;
        }

        public BookImage Get(int index)
        {
            return _images[index];
        }

        private BookImage[] _images;
        public string id;

        private uint _count;
        private Signal<BookImage> _completed;



        internal void Add(string str)
        {
            _images[_count++] = new BookImage(str, _completed);
        }



        public Signal<BookImage> completed
        {
            get
            {
                return _completed;
            }
        }

        public int Length
        {
            get
            {
                return _images.Length;
            }
        }

    }

    public class BookImage
    {
        public BookImage(string url, Signal<BookImage> completed)
        {
            _url = url;
            _complete = completed;
        }

        private WWW _www;
        private string _url;
        private Signal<BookImage> _complete;
        private Texture2D _texture;
        private string _error;
        private bool _isCompleted;

        public WWW www
        {
            set
            {
                _www = value;
            }
        }

        public float progress
        {
            get
            {
                return _www.progress;
            }
        }

        public string error
        {
            get
            {
                return _error;
            }
        }

        public Texture texture
        {
            get
            {
                return _texture;
            }
        }

        public bool isDone
        {
            get
            {
                return (_isCompleted) ? true : (_www == null) ? false : _www.isDone;
            }
        }

        public Signal<BookImage> completed
        {
            get
            {
                return _complete;
            }
        }

        internal WWW Load()
        {
            _www = new WWW(_url);
            return _www;
        }

        internal void LoadCompleted()
        {
            _texture = _www.texture;
            _error = _www.error;
            _www.Dispose();
            _www = null;
            _isCompleted = true;
            _complete.Dispatch(this);
        }
    }
}



