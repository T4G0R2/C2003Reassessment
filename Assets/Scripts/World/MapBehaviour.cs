using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _groundPlane;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _brownMaterial;

    private GameObject _playerMapMarker;
    private GameObject _enemyMapMarker;
    private GameObject _sniper1MapMarker;
    private GameObject _sniper2MapMarker;
    private GameObject _sniper3MapMarker;
    private GameObject _hostageMapMarker;
    
    private GameObject _playerController;
    private GameObject _enemyController;
    private GameObject _sniper1Controller;
    private GameObject _sniper2Controller;
    private GameObject _sniper3Controller;
    private GameObject _hostageController;

    // Start is called before the first frame update
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player");
        _enemyController = GameObject.FindGameObjectWithTag("Enemy");
        _sniper1Controller = GameObject.FindGameObjectWithTag("Sniper1");
        _sniper2Controller = GameObject.FindGameObjectWithTag("Sniper2");
        _sniper3Controller = GameObject.FindGameObjectWithTag("Sniper3");
        _hostageController = GameObject.FindGameObjectWithTag("Hostage");
        _mapMarkerInitialization();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _updatePos(_playerController, _playerMapMarker);
        _updatePos(_enemyController, _enemyMapMarker);
        _updatePos(_sniper1Controller, _sniper1MapMarker);
        _updatePos(_sniper2Controller, _sniper2MapMarker);
        _updatePos(_sniper3Controller, _sniper3MapMarker);
        _updatePos(_hostageController, _hostageMapMarker);
    }

    private void _mapMarkerInitialization() 
    {
        _playerMapMarker = _mapMarkerCreation("Player");
        _enemyMapMarker = _mapMarkerCreation("Enemy");
        _sniper1MapMarker = _mapMarkerCreation("Sniper1");
        _sniper2MapMarker = _mapMarkerCreation("Sniper2");
        _sniper3MapMarker = _mapMarkerCreation("Sniper3");
        _hostageMapMarker = _mapMarkerCreation("Hostage");
    }

    private GameObject _mapMarkerCreation(string tag)
    {
        GameObject _targetObject;
        Vector3 _defaultPoss = new Vector3(0, 0, 0);
        var _defaultRot = Quaternion.Euler(0, 0, 0);
        Vector3 _defaultScale = new Vector3(0.3f, 0.005f, 0.3f);

        _targetObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        _targetObject.transform.SetParent(gameObject.transform);
        _targetObject.transform.localPosition = _defaultPoss;
        _targetObject.transform.localRotation = _defaultRot;
        _targetObject.transform.localScale = _defaultScale;

        if (tag == "Player")
        {
            _targetObject.name = "PlayerMapMarker";
            _targetObject.GetComponent<Renderer>().material = _greenMaterial;
        }
        else if (tag == "Sniper1")
        {
            _targetObject.name = "Sniper1MapMarker";
            _targetObject.GetComponent<Renderer>().material = _blueMaterial;
        }
        else if (tag == "Sniper2")
        {
            _targetObject.name = "Sniper2MapMarker";
            _targetObject.GetComponent<Renderer>().material = _blueMaterial;
        }
        else if (tag == "Sniper3")
        {
            _targetObject.name = "Sniper3MapMarker";
            _targetObject.GetComponent<Renderer>().material = _blueMaterial;
        }
        else if (tag == "Enemy")
        {
            _targetObject.name = "EnemyMapMarker";
            _targetObject.GetComponent<Renderer>().material = _redMaterial;
        }
        else if (tag == "Hostage")
        {
            _targetObject.name = "HostageMapMarker";
            _targetObject.GetComponent<Renderer>().material = _brownMaterial;
        }
        return _targetObject;
    }

    private void _updatePos(GameObject _controller, GameObject _marker)
    {
        float _posX = _controller.transform.position.x / _groundPlane.transform.localScale.x;
        float _posY = 0;
        float _posZ = _controller.transform.position.z / _groundPlane.transform.localScale.z;

        Vector3 _newPoss = new Vector3(_posX, _posY, _posZ);
        _marker.transform.localPosition = _newPoss;
    }
}