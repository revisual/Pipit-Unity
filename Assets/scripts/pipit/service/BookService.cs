using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace revisual.pipit
{
    using data;
    public class BookService : MonoBehaviour, IBookService
    {
       

        public string DOMAIN = "http://pipit-staging.herokuapp.com/";
        public string API = "api/book?id={0}&size={1}";
        private ImageSizeModel imageSize = new ImageSizeModel();

        public GameObject[] eventTargets;

        public void requestBook(string id)
        {
            StartCoroutine(GetBook(id));
        }

        IEnumerator GetBook(string id)
        {
            string url = DOMAIN +  string.Format(API, id, imageSize.GetImageSize() );

            Debug.Log(url);

            WWW www = new WWW(url);

            yield return www;

			if (www.error == null)
            {
				dispatchBook (new BookDecoder (www.text).Decode ());
			} 
			else 
			{
                dispatchFailure(www.error);
            }            

        }

        private void dispatchBook(Book book)
        {
            if (eventTargets == null) return;
            int len = eventTargets.Length;
            for (int i = 0; i < len; i++)
            {
                ExecuteEvents.Execute<IBookReciever>(eventTargets[i], null, (x, y) => x.receiveBook(book));
            }

       }

        private void dispatchFailure(string error)
        {
            if (eventTargets == null) return;
            int len = eventTargets.Length;
            for (int i = 0; i < len; i++)
            {
                ExecuteEvents.Execute<IBookReciever>(eventTargets[i], null, (x, y) => x.receiveBookFailure(error));
            }
        }

       
    }

}
