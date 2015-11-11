using strange.extensions.signal.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace revisual.pipit
{
   public class AspectRatioBinding

    {
        private Signal<float> _received= new Signal<float>();
        private float _value;
        private bool _beenSet = false;

        public void Dispatch(float value)
        {
            _value = value;
            _beenSet = true;
            _received.Dispatch(_value);
        }

        public void AddOnce(Action<float> handler)
        {
            _received.AddOnce(handler);
            DispatchValue();
        }

        public void AddListener(Action<float> handler)
        {
            _received.AddListener(handler);
            DispatchValue();
        }

        public void RemoveListener(Action<float> handler)
        {
            _received.RemoveListener(handler);
            DispatchValue();
        }

        public void Reset()
        {
            _received.RemoveAllListeners();
            _beenSet = false;
        }

        private void DispatchValue()
        {
            if (!_beenSet) return;
            _received.Dispatch(_value);

        }
    }
}
