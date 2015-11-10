
using System;
using revisual.pipit.data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace revisual.pipit
{
    public class MainRoot : MonoBehaviour , IProjectListReciever
    {
        public void receiveProjectList(IProjectList project )
        {
            Debug.Log("project - " + project);
        }

        public void receiveProjectListFailure(string error)
        {
            Debug.Log("project fail - " + error);
        }

        void Start()
        {
            Screen.autorotateToPortrait = true;

            Screen.autorotateToPortraitUpsideDown = true;

            Screen.orientation = ScreenOrientation.AutoRotation;

           //ExecuteEvents.Execute<IBookService>(gameObject, null, (x, y) => x.requestBook("glow-worm"));
          //  ExecuteEvents.Execute<IProjectListService>(gameObject, null, (x, y) => x.requestAllProjects());
        }      

       

      
           
    }
}


