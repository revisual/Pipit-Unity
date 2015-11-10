using UnityEngine;
using System.Collections;
using revisual.pipit;
using revisual.pipit.data;
using UnityEngine.EventSystems;
using strange.extensions.signal.impl;

public class ProjectListService : MonoBehaviour, IProjectListService
{
    public string DOMAIN = "http://pipit-staging.herokuapp.com/";
    public string API = "api/listProject?";

    private Signal<ProjectList> _resolved = new Signal<ProjectList>();


    public GameObject[] eventTargets;

    public Signal<ProjectList> RequestProjectList()
    {
        StartCoroutine(GetAllProjects());
        return _resolved;
    }

  

    IEnumerator GetAllProjects()
    {
        Debug.Log("URL = " + DOMAIN);
        WWW www = new WWW(DOMAIN + API);

        yield return www;

        if (www.error == null)
        {
            Debug.Log("DATA = " + www.text);
            _resolved.Dispatch(new ProjectListDecoder(www.text, ProjectType.LIST).Decode());
        }
        else
        {
            Debug.Log("FAIL = " + www.error);
            _resolved.Dispatch(null);
        }

    }



    
   

}
