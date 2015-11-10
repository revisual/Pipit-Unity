using UnityEngine;
using System.Collections;
using revisual.pipit.data;
using UnityEngine.UI;
using revisual.pipit;

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

    public void loadImages( ICoroutineHandler handler)
    {
        handler.StartCoroutine(load(_data.thumbnailURL));
    }

    private IEnumerator load(string url)
    {

        
		WWW www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            thumbnail.texture = www.texture;
            Color newColor = new Color(1, 1, 1, 1);
            thumbnail.color = newColor;
        }
        else
        {
            Debug.Log("FAIL = " + www.error);          
        }
    }

    public void dispatch()
    {
        _data.Click();
    }
}
