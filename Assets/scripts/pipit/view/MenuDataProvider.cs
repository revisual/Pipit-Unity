using UnityEngine;
using System.Collections;
using revisual.pipit.data;
using revisual.pipit;
using System;

public class MenuDataProvider : MonoBehaviour, IProjectListReciever, ICoroutineHandler
{

    public GameObject prefab;
    public Transform content;

    void Populate(IProjectList data)
    {

        int len = Math.Max(data.Length, content.childCount);
        for (int i = 0; i < len; i++)
        {
            GameObject newItem;
            ProjectItem item;
            ProjectItemDataProvider provider;
            bool usePooled = (i < content.childCount);

            newItem = usePooled
                ? content.GetChild(i).gameObject
                : Instantiate(prefab) as GameObject;

            if (i < data.Length)
            {
                item = data.Get(i);
                provider = newItem.GetComponent<ProjectItemDataProvider>();
                provider.Populate(item);
                provider.loadImages(this);

                if (!usePooled)
                {
                    newItem.transform.SetParent(content);                    
                }

                newItem.SetActive(true);
            }

            else
            {
                newItem.SetActive(false);
            }

        }

    }

    public void receiveProjectList(IProjectList project)
    {
        Populate(project);
    }

    public void receiveProjectListFailure(string error)
    {
        throw new NotImplementedException();
    }
}
