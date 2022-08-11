using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public GameObject Ending1;
    public GameObject Ending2;
    public GameObject Ending3;
    public GameObject Ending4;
    public GameObject EndingGO;
    public Transform Canvas;
    private bool DidFinish = false;

    public void EndGame(int Ending)
    {
        Time.timeScale = 1;
        StartCoroutine(WaitBeforeTrigger(5, Ending));
    }

    IEnumerator WaitBeforeTrigger(int time, int Ending)
    {
        yield return new WaitForSeconds(time);

        if (Ending == 0)
        {
            EndScreen(Ending1);
        }
        if (Ending == 1)
        {
            EndScreen(Ending2);
        }
        if (Ending == 2)
        {
            EndScreen(Ending3);
        }
        if (Ending == 3)
        {
            EndScreen(Ending4);
        }
    }

    public void EndScreen(GameObject EndingPrefab)
    {
        EndingGO = Instantiate(EndingPrefab);
        EndingGO.transform.SetParent(Canvas);
        DidFinish = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && DidFinish == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }
}
