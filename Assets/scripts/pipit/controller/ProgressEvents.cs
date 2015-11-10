
using UnityEngine.EventSystems;
namespace revisual.pipit
{
    public interface IProgressReciever : IEventSystemHandler
    {
        void OnProgress(float prog);
    }

    public interface IProgressCompleteReciever : IEventSystemHandler
    {
        void OnProgressComplete();
    }

    public interface IProgressStartReciever : IEventSystemHandler
    {
        void OnProgressStart();
    }

    public interface IProgressResolvedReceiver : IEventSystemHandler
    {
        void OnItemResolved(int index);
    }

    public interface IProgressActivityStart : IEventSystemHandler
    {
        void OnProgressActivityStart();
    }

    public interface IProgressActivityEnd : IEventSystemHandler
    {
        void OnProgressActivityEnd();
    }


}

   


