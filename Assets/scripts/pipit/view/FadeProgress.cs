
namespace Assets.pipit.view
{
    using UnityEngine;
    using revisual.pipit;

    public class FadeProgress : MonoBehaviour, IProgressStartReciever, IProgressCompleteReciever, IProgressActivityStart, IProgressActivityEnd
    {

        private CanvasRenderer _backgroundRenderer;
        private CanvasRenderer _indicatorRenderer;

        private float _currentAlpha = 0;
        private bool _in = true;

        void Awake()
        {
            _backgroundRenderer = gameObject.transform.Find("Background").GetComponent<CanvasRenderer>();
            _indicatorRenderer = gameObject.transform.Find("Indicator").GetComponent<CanvasRenderer>();
            _backgroundRenderer.SetAlpha(_currentAlpha);
            _indicatorRenderer.SetAlpha(_currentAlpha);
        }


        public void Update()
        {
            if (_in && _currentAlpha < 1)
            {
                _currentAlpha += 0.03f;
                _backgroundRenderer.SetAlpha(_currentAlpha);
                _indicatorRenderer.SetAlpha(_currentAlpha);

            }

            else if (!_in && _currentAlpha > 0)
            {
                _currentAlpha -= 0.03f;
                _backgroundRenderer.SetAlpha(_currentAlpha);
                _indicatorRenderer.SetAlpha(_currentAlpha);

            }


        }

        public void OnProgressComplete()
        {
            _in = false;
        }

        public void OnProgressStart()
        {
            _in = true;
        }

        public void OnProgressActivityStart()
        {
            _in = true;
        }

        public void OnProgressActivityEnd()
        {
            _in = false;
        }
    }

}
