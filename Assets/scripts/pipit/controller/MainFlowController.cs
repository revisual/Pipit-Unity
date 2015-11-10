using UnityEngine;
using System.Collections;
using revisual.pipit;
using revisual.pipit.data;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class MainFlowController : MonoBehaviour
{
	public ProjectListService projectListService;
	public ProjectService projectService;
	public BookService bookService;
	
    public GameObject[] eventTargets;  
    
    [Serializable]
    public class ToggleViewEvent :UnityEvent<int> { }

    [SerializeField]
    private ToggleViewEvent _toggleView = new ToggleViewEvent();


    public void Start()
    {
        LoadProjectList();
    }

    public void LoadProjectList()
	{	
		projectListService
			.RequestProjectList ()
			.AddOnce (RecievedProjectList);
	}

	public void RecievedProjectList(ProjectList projectList)
	{
		projectList.clicked.AddOnce (LoadProject);
		dispatchToView (projectList);
	}

    
	public void LoadProject(ProjectItem item)
	{	
		projectService
			.RequestProjects (item.GetID ())
			.AddOnce (RecievedProject);
	}
	


	public void RecievedProject(ProjectList projectList)
	{
		projectList.clicked.AddOnce (LoadBook);
		dispatchToView (projectList);
	}

	public void LoadBook(ProjectItem item)
	{
        _toggleView.Invoke((int)ViewState.FLICK_BOOK);
        bookService.requestBook (item.GetID ());
	}


	
	
	private void dispatchToView(ProjectList projectList)
	{
		if (eventTargets == null) return;
		int len = eventTargets.Length;
		for (int i = 0; i < len; i++)
		{
			ExecuteEvents.Execute<IProjectListReciever>(eventTargets[i], null, (x, y) => x.receiveProjectList(projectList));
		}
	}


	
	public void receiveProjectListFailure(string error)
    {
        throw new NotImplementedException();
    }
}
