using UnityEngine;
using System.Collections;

public class ToggleActiveViews : MonoBehaviour
{
    private GameObject _flickBook;
    private GameObject _progress;
    private GameObject _menu;

    public void Awake()

    {
        _flickBook = transform.Find("FlickBook").gameObject;
        _menu = transform.Find("Menu").gameObject;
        _progress = transform.Find("Progress").gameObject;

    }

    public void toggleView(int stateType)
    {
        if ((int)ViewState.FLICK_BOOK == stateType)
        {
            _flickBook.SetActive(true);
            _progress.SetActive(true);
            _menu.SetActive(false);
        }

        else if ((int)ViewState.MENU == stateType)
        {
            _flickBook.SetActive(false);
            _progress.SetActive(false);
            _menu.SetActive(true);
        }
    }
}
