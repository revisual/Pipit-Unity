using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace revisual.pipit
{
    using data;
    public class ImageSequenceLoader : MonoBehaviour, IBookReciever
    {
        public GameObject[] eventTargets;
        private IBook _book;

        private BookImage _current;
        private int _index;
        private int _length;

        public void receiveBook(IBook book)
        {
            Debug.Log("START LOADING IMAGES");
            enabled = true;
            _book = book;
            reset();
            SendStartProgress();
            _book.completed.AddListener(LoadCompleted);
            LoadNext();
        }

        private void reset()
        {
            _index = 0;      
            _length = _book.Length;          
        }

        public void receiveBookFailure(string error)
        {
            // throw new NotImplementedException();
        }

        private void LoadNext()
        {
            _current = _book.Get(_index++);
            _current.Load();
        }

     

        private void LoadCompleted( BookImage bookImage)
        {
            if (_index >= _length)
            {
                _book.completed.RemoveListener(LoadCompleted);
                SendComplete();
                _index = 0;
                _current = null;
                _length = 0;
                _book = null;
                enabled = false;
            }

            else
            {
                SendResolved(_index-1);
                LoadNext();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_current == null) return;
            float prog = (_index + _current.progress - 1) / _length;
            SendProgress(prog);


        }

        private void SendStartProgress()
        {
            int len = eventTargets.Length;
            for (int i = 0; i < len; i++)
            {
                ExecuteEvents.Execute<IProgressStartReciever>(eventTargets[i], null, (x, y) => x.OnProgressStart());
            }
        }

        private void SendProgress(float prog)
        {
            int len = eventTargets.Length;
            for (int i = 0; i < len; i++)
            {
                ExecuteEvents.Execute<IProgressReciever>(eventTargets[i], null, (x, y) => x.OnProgress(prog));
            }
        }

        private void SendResolved(int index)
        {
            int len = eventTargets.Length;
            for (int i = 0; i < len; i++)
            {
                ExecuteEvents.Execute<IProgressResolvedReceiver>(eventTargets[i], null, (x, y) => x.OnItemResolved(index));
            }
        }

        private void SendComplete()
        {
            int len = eventTargets.Length;
            for (int i = 0; i < len; i++)
            {
                ExecuteEvents.Execute<IProgressCompleteReciever>(eventTargets[i], null, (x, y) => x.OnProgressComplete());
            }
        }


    }


}
