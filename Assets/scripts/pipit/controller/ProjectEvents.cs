
using UnityEngine.EventSystems;
namespace revisual.pipit
{
    using data;
    using strange.extensions.signal.impl;

    public interface IProjectListReciever : IEventSystemHandler
    {
        void receiveProjectList(IProjectList project);

        void receiveProjectListFailure(string error);



    }

   

    

    public interface IProjectListService : IEventSystemHandler
    {
        Signal<ProjectList> RequestProjectList();

    }
}

   


