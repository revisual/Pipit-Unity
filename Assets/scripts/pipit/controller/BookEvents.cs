
using UnityEngine.EventSystems;
namespace revisual.pipit
{
    using data;
    public interface IBookReciever : IEventSystemHandler
    {
        void receiveBook(IBook book);

        void receiveBookFailure(string error);
    }

    public interface IBookService : IEventSystemHandler
    {
        void requestBook(string id);

    }
}

   


