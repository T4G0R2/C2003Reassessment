using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour
{
    public int numberofbuttons;
    public Transform buttonContainer;
    public GameObject button;
    public List<GameObject> buttons;
    public GameObject textOnScreen;
    public Transform textContainter;

    public List<string> dialogue;

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/BucketHat/Resources/LorumIpsum.txt";
        foreach (string line in System.IO.File.ReadLines(path))
        {
            dialogue.Add(line);
        }
        CreateNumberOfButtons(numberofbuttons);


    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateButton(string textOnButton, string textToPrint)
    {
        GameObject newButton = Instantiate(button) as GameObject;
        newButton.transform.SetParent(buttonContainer);
        buttons.Add(newButton);
        newButton.GetComponentInChildren<Text>().text = textOnButton;
        newButton.GetComponentInChildren<Button>().onClick.AddListener(() => PrintText(textToPrint));
        newButton.GetComponentInChildren<Button>().onClick.AddListener(() => DestroyOldButtons());
        newButton.GetComponentInChildren<Button>().onClick.AddListener(() => CreateNumberOfButtons(Random.Range(1, 6)));

    }

    public void CreateNumberOfButtons(int buttonsToCreate)
    {
        if (buttonsToCreate > 0)
        {
            for (int i = 0; i < buttonsToCreate; i++)
            {
                CreateButton(i.ToString(), dialogue[Random.Range(0,dialogue.Count)]);
            }
        }

    }

    public void PrintText(string textToPrint)
    {
        Debug.Log(textToPrint);
        GameObject newText = Instantiate(textOnScreen) as GameObject;
        newText.transform.SetParent(textContainter);
        string logText = textToPrint;
        newText.GetComponentInChildren<Text>().text = logText;
        SoundManager.PlaySound();
    }

    public void DestroyOldButtons()
    {
        List<GameObject> oldButtons = buttons;

        foreach (GameObject button in oldButtons)
        {
            Destroy(button);
        }

    }
}
