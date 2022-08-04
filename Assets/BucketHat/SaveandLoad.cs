using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveandLoad : MonoBehaviour
{
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public Camera Camera4;
    private int cameranumber = 0;
    private float camerarotationx = 0;
    private bool updated = true;

    Settings settings;
    private void Awake() {
        if (!File.Exists(Application.dataPath + "/settings.cfg")){
            Debug.Log("No settings file found, creating new one");
            settings = new Settings();
            string jsonExport = JsonUtility.ToJson(settings);
            File.WriteAllText(Application.dataPath + "/settings.cfg", jsonExport);
        }
        else{
            Debug.Log("Settings file found, Loading settings");
            string jsonImport = File.ReadAllText(Application.dataPath + "/settings.cfg");
            settings = JsonUtility.FromJson<Settings>(jsonImport);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cameranumber = settings.cameranumber;
        updated = true;
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
        Camera4.enabled = false;
    }
    public void buttonup()
    {
        cameranumber += 1;
        updated = true;
    }
    public void buttondown()
    {
        cameranumber -= 1;
        updated = true;
    }
    public void save()
    {
        settings.cameranumber = cameranumber;
        settings.camerarotationx = camerarotationx;
        string jsonExport = JsonUtility.ToJson(settings);
        File.WriteAllText(Application.dataPath + "/settings.cfg", jsonExport);
    }
    public void load()
    {
        cameranumber = settings.cameranumber;
        camerarotationx = settings.camerarotationx;
        updated = true;
    }
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            save();
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            camerarotationx -= Mathf.PI * Time.deltaTime * 5;
            updated = true;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            camerarotationx += Mathf.PI * Time.deltaTime * 5;
            updated = true;
        }
        if (Input.GetKeyDown(KeyCode.R)){
            camerarotationx = 0f;
            updated = true;
        }
        if (updated)
        {
            updated = false;
            cameranumber = (cameranumber % 4 + 4) % 4;//Modulus
            Camera1.enabled = false;
            Camera2.enabled = false;
            Camera3.enabled = false;
            Camera4.enabled = false;
            switch (cameranumber){
                case 1:
                    Camera2.enabled = true;
                    Camera2.transform.eulerAngles = new Vector3 (camerarotationx,Camera2.transform.eulerAngles.y,Camera2.transform.eulerAngles.z);
                    break;
                case 2:
                    Camera3.enabled = true;
                    Camera3.transform.eulerAngles = new Vector3 (camerarotationx,Camera3.transform.eulerAngles.y,Camera3.transform.eulerAngles.z);
                    break;
                case 3:
                    Camera4.enabled = true;
                    Camera4.transform.eulerAngles = new Vector3 (camerarotationx,Camera4.transform.eulerAngles.y,Camera4.transform.eulerAngles.z);
                    break;
                default:
                    Camera1.enabled = true;
                    Camera1.transform.eulerAngles = new Vector3 (camerarotationx,Camera1.transform.eulerAngles.y,Camera1.transform.eulerAngles.z);
                    break;
            }
            
        }
        
    }
}
[System.Serializable]
public class Settings{
    [Header("CameraNumber")]
    [Range(0, 3)]
    public int cameranumber = 1;

    [Header("CameraRotation")]
    public float camerarotationx = 0;
}