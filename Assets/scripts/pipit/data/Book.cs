
using BestHTTP;
using strange.extensions.signal.impl;
using System;
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
            _progress = 0;
        }

        private string _url;
        private Signal<BookImage> _complete;
        private Texture2D _texture;
        private string _error;
        private bool _isCompleted;
        private float _progress;
        private HTTPRequest _request;

        public float progress
        {
            get
            {
                return _progress;
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
                return _isCompleted;
            }
        }

        public Signal<BookImage> completed
        {
            get
            {
                return _complete;
            }
        }

        internal Signal<BookImage> Load()
        {
            _request = new HTTPRequest(new Uri(_url), OnComplete);
            _request.OnProgress = OnDownloadProgress;
            _request.Send();
            return _complete;
        }

        void OnDownloadProgress(HTTPRequest request, int downloaded, int length)
        {
            _progress = (downloaded / (float)length);
        }

        void OnComplete(HTTPRequest request, HTTPResponse response)
        {
            _texture = _request.Response.DataAsTexture2D;
            _error = (_request.State == HTTPRequestStates.Error)
                ? "Request Finished with Error! " +
                    (_request.Exception != null ?
                    (_request.Exception.Message + "\n" + _request.Exception.StackTrace) :
               "No Exception")
               : null;

            _progress = 1;
            _isCompleted = true;
            _complete.Dispatch(this);
        }



    }
}



