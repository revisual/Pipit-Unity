
using UnityEngine;
using UnityEngine.UI;

namespace revisual.pipit
{
    using System;
    using data;
    public class FlickBook : MonoBehaviour, IProgressReciever, IProgressResolvedReceiver
    {
        public BookModel model;
        private RawImage _baseImage;
        private RawImage _overlayImage;
        private bool _imageSet;

        public void Awake()
        {
            _baseImage = transform.Find("BasePanel").GetComponent<RawImage>();
            _overlayImage = transform.Find("OverlayPanel").GetComponent<RawImage>();
        }

        public void OnProgress(float prog)
        {
            model.SetAlphaAtIndex(_baseImage, prog);
        }

        private IBook _book;
        private float _currentPosition;
        private int _currentIndex;
        private float _currentAlpha;

        public void Update()
        {
            model.SetBaseImage(_baseImage);
            model.SetOverlayImage(_overlayImage);
        }

        public void OnItemResolved(int index)
        {
            if (index == 0)
            {
                OnFirstImageResolved();
            }

        }

        private void OnFirstImageResolved()
        {
            transform.Find("BasePanel").GetComponent<AspectRatioFitter>().aspectRatio = model.AspectRatio;
            transform.Find("OverlayPanel").GetComponent<AspectRatioFitter>().aspectRatio = model.AspectRatio;
            model.SetImageAtIndex(_baseImage, 0);
        }
    }
}
