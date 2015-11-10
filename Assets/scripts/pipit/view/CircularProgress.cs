
namespace Assets.pipit.view
{
    using UnityEngine;
    using UnityEngine.UI;
    using revisual.pipit;
    using System;

    public class CircularProgress : MonoBehaviour, IProgressReciever, IProgressStartReciever, IProgressCompleteReciever
    {

        public Color start;
        public Color current;
        public Color end;

        private float _rotation = 0;
        private float _vel = 0;
        private float _currentProg = 0;


        private Image circleImage;

        void Awake()
        {
            circleImage = transform.Find("Indicator").GetComponent<Image>();
            circleImage.type = Image.Type.Filled;
            circleImage.fillMethod = Image.FillMethod.Radial360;
        }       

        public void OnProgress(float prog)
        {          
            _vel = calcElastic(_currentProg, prog, 0.1f, 0.50f, _vel);
            _currentProg += _vel;

            circleImage.fillAmount = Mathf.Max(_currentProg, 0.001f);
            circleImage.color = Color.Lerp(start, end, _currentProg);
            circleImage.transform.rotation = Quaternion.Euler(0, 0, _rotation);
            _rotation -= 0.33f;
        }

        public void OnProgressStart()
        {
            circleImage.transform.rotation = Quaternion.Euler(0, 0, _rotation);
            circleImage.fillAmount = 0;
            _currentProg = 0;
           
        }

        public void OnProgressComplete()
        {
          
        }

        private float calcElastic(float orig, float dest, float spring, float damp, float vel)
        {
            float elasticForce = -spring * (orig - dest);
            return (vel + elasticForce) * damp;
        }
    }





}
