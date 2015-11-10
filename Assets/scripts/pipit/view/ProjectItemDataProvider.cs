using UnityEngine;
using System.Collections;
using revisual.pipit.data;
using UnityEngine.UI;
using revisual.pipit;
using BestHTTP;
using System;

public class ProjectItemDataProvider : MonoBehaviour
{
    public Button button;
    public Text title;
    public Text description;
    public RawImage thumbnail;

    private ProjectItem _data;

    public void Populate(ProjectItem data)
    {
        _data = data;
        title.text = _data.GetTitle();
        description.text = _data.GetDescription();

    }

    public void loadImages(ICoroutineHandler handler)
    {
        handler.StartCoroutine(load(_data.thumbnailURL));
    }

    private IEnumerator load(string url)
    {
        HTTPRequest request = new HTTPRequest(new Uri(url));
        request.Send();
        
        yield return StartCoroutine(request);

        if (request.State == HTTPRequestStates.Error)
        {
            Debug.LogError("Request Finished with Error! " +
               (request.Exception != null ?
               (request.Exception.Message + "\n" + request.Exception.StackTrace) :
               "No Exception"));
        }
        else
        {
            thumbnail.texture = request.Response.DataAsTexture2D;
            Color newColor = new Color(1, 1, 1, 1);
            thumbnail.color = newColor;
        }
    }

    public void dispatch()
    {
        _data.Click();
    }
}
