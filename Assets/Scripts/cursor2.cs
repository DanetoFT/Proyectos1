using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cursor2 : MonoBehaviour
{
    public void Click()
    {
        AudioController.Instance.PlaySFX("Click");
    }

    public void Music()
    {
        //AudioController.Instance.musicSource.Stop();
        AudioController.Instance.PlaySFX("Terror");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
