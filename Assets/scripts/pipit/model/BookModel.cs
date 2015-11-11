

namespace revisual.pipit
{
    using System;
    using data;
    using UnityEngine;
    using UnityEngine.UI;

    public interface IBookModel
    {
        void PageIncrement(float increment);
    }

    public class BookModel : MonoBehaviour, IBookReciever, IBookModel
    {
        public float dampening;
        public float spring;
        private IBook _book;
        private float _currentPosition = 0;
        private float _aspectRatio = 1;
        private int _currentImageIndex = 0;
        private float _currentAlpha = 0;
        private float _targetMultiplier = 0;
        private float _currentMultiplier = 0;
        private float _vel = 0;
        private int _previousImageIndex = -1;
        private int _previousOverlayImageIndex = -1;

        internal void killVelocity()
        {
            _vel = 0;
            _targetMultiplier = _currentMultiplier;
        }

        private float _previousAlpha = -1;
        private AspectRatioBinding _aspectRatioResolved = new AspectRatioBinding();

        public void receiveBook(IBook book)
        {
            _book = book;
            _book.completed.AddOnce(image =>
            {
                _aspectRatioResolved.Dispatch((float)image.texture.width / image.texture.height);
            });
        }

        public AspectRatioBinding aspectRatioResolved
        {
            get { return _aspectRatioResolved; }
        }

        public void SetImageAtIndex(RawImage image, int index)
        {
            if (_book == null) return;
            BookImage data = _book.Get(index);
            if (!data.isDone) return;

            image.texture = data.texture;



        }

        public void SetAlphaAtIndex(RawImage image, float alpha)
        {
            if (_book == null) return;
            Color newColor = new Color(1, 1, 1, alpha);
            image.color = newColor;
        }




        public void SetBaseImage(RawImage image)
        {
            if (_book == null || _previousImageIndex == _currentImageIndex || _vel == 0) return;
            BookImage data = _book.Get(_currentImageIndex);
            if (!data.isDone) return;

            image.texture = data.texture;
            _previousImageIndex = _currentImageIndex;
        }



        public void SetOverlayImage(RawImage image)
        {
            if (_book == null || _vel == 0) return;

            BookImage data = _book.Get(_currentImageIndex + 1);

            if (!data.isDone) return;

            if (_previousOverlayImageIndex != _currentImageIndex + 1)
            {

                image.texture = data.texture;
                _previousOverlayImageIndex = _currentImageIndex + 1;
            }

            if (_previousAlpha != _currentAlpha)
            {

                Color newColor = new Color(1, 1, 1, _currentAlpha);
                image.color = newColor;
                _previousAlpha = _currentAlpha;
            }

        }

        public void SetFromMultiplier(float multiplier)
        {
            if (_book == null || _currentMultiplier == multiplier) return;
            _targetMultiplier = multiplier;
        }

        public void PageIncrement(float increment)
        {
            if (_book == null || increment == 0) return;
            float incrMultiplier = (increment / _book.Length);

            if (_targetMultiplier < 0 && incrMultiplier > 0)
            {
                _targetMultiplier = incrMultiplier;
            }

            else if (_targetMultiplier > _book.Length - 2 && incrMultiplier < 0)
            {
                _targetMultiplier = 1 + incrMultiplier;
            }

            else
            {
                _targetMultiplier += incrMultiplier;// 
            }
            //  _targetMultiplier = Mathf.Clamp(_targetMultiplier, 0, _book.Length - 2);
        }

        public void Update()
        {
            if (_book == null) return;
            _vel = calcElastic(_currentMultiplier, _targetMultiplier, dampening, spring, _vel);
            _currentMultiplier += _vel;
            _currentPosition = Mathf.Clamp(_currentMultiplier * _book.Length, 0, _book.Length - 2);
            _currentImageIndex = Mathf.FloorToInt(_currentPosition);
            _currentAlpha = _currentPosition - _currentImageIndex;
            if (_vel > 0.01)
            {
                _vel = 0;
            }
        }

        private float calcElastic(float orig, float dest, float spring, float damp, float vel)
        {
            float elasticForce = -spring * (orig - dest);
            return (vel + elasticForce) * damp;
        }



        public void receiveBookFailure(string error)
        {
            throw new NotImplementedException();
        }
    }
}
