using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(QuitApplication());
    }

    IEnumerator QuitApplication() {
        yield return new WaitForSeconds(7f);
        Application.Quit();
    }
}
