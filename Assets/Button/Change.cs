using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    // ƒV[ƒ“–¼‚ğ•Û‘¶‚·‚é‚½‚ß‚Ì•Ï”
    public string sceneName;

    public void SceneChange()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
