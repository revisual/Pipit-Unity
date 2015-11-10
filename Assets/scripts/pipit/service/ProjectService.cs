using UnityEngine;
using System.Collections;
using revisual.pipit.data;
using strange.extensions.signal.impl;

public class ProjectService : MonoBehaviour
{
    public string DOMAIN = "http://pipit-staging.herokuapp.com/";
    public string API = "api/project?id={0}";

    private Signal<ProjectList> _resolved = new Signal<ProjectList>();
   

    public Signal<ProjectList> RequestProjects(string id)
    {
        StartCoroutine(GetProjects(id));
        return _resolved;
    }

    IEnumerator GetProjects(string id)
    {
        WWW www = new WWW(DOMAIN + string.Format(API, id));

        yield return www;

        Debug.Log("PROJECT " + id + " " + www.text);
        if (www.error == null)
        {
            dispatchProject(new ProjectListDecoder(www.text, ProjectType.PROJECT).Decode());
        }
        else
        {
            dispatchProject(null);
        }
    }

    private void dispatchProject(ProjectList projectList)
    {
        _resolved.Dispatch(projectList);
    }

  

}
