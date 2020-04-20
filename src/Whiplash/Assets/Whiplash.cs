using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiplash : MonoBehaviour
{
    
    private GameObject _selectedGameObject;
    private bool _listeningForGesture;
    private Vector3 _targetLockOnPos = new Vector3();
    private Quaternion _targetLockOnRot = new Quaternion();
    private int _intervalCounter = 0;

    private List<Quaternion> _movementOverTime = new List<Quaternion>();


    public float LockOnDistance;
    public float MovementSpeed;
    public int ScanInterval;
      
    // Start is called before the first frame update
    void Start()
    {
        LockOnDistance = 6f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
            DeselectObject(_selectedGameObject);

        if (_listeningForGesture)
        {
            ObjectLockOn();
            
        }

    }
    #region Select / Deselect
    public void SelectObject(GameObject obj)
    {
        //Disable Gaze Controller
        GetComponent<VRGazeController>().enabled = false;

        //Manipulate object
        _selectedGameObject = obj;


        //Enable listening
        _listeningForGesture = true;


    }
    public void DeselectObject(GameObject obj)
    {
        //Enable Gaze Controller
        GetComponent<VRGazeController>().enabled = true;

        //Manipulate object
        _selectedGameObject = null;
        _selectedGameObject.GetComponent<Rigidbody>().useGravity = false;

        //Disable listening
        _listeningForGesture = false;


    }

    //Fixates the selected object to the players main camera
    void ObjectLockOn()
    {
        //Get target position
        _targetLockOnPos = transform.position + transform.forward * LockOnDistance;
        //Get distance from target position
        var vDist = Vector3.Distance(_targetLockOnPos, _selectedGameObject.transform.position);
        //Get direction to target position
        var shootDir = _targetLockOnPos - _selectedGameObject.transform.position;
        shootDir.Normalize();
        
        //Add velocity towards target position
        Rigidbody selectedRigidBody = _selectedGameObject.GetComponent<Rigidbody>();
        selectedRigidBody.velocity = shootDir * (vDist * 3);




    }

    #endregion

    void AppendMovement()
    {
        _movementOverTime.Add(this.transform.rotation);
    }

    void ScanForGesture()
    {
   
        
    }



}
