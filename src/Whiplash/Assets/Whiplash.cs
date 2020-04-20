using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiplash : MonoBehaviour
{
    
    private GameObject _selectedGameObject;
    private Rigidbody _selectedRigidBody;
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
            //Get target position
            _targetLockOnPos = transform.position + transform.forward * LockOnDistance;
            //Set velocity towards target position
            SetVelocityTowards(_selectedRigidBody, _targetLockOnPos, _selectedGameObject.transform.position);
            
        }

    }
    #region Select / Deselect
    public void SelectObject(GameObject obj)
    {
        //Disable Gaze Controller
        GetComponent<VRGazeController>().enabled = false;

        //Reference objects
        _selectedGameObject = obj;
        _selectedRigidBody = _selectedGameObject.GetComponent<Rigidbody>();


        //Enable listening
        _listeningForGesture = true;


    }
    public void DeselectObject(GameObject obj)
    {
        //Enable Gaze Controller
        GetComponent<VRGazeController>().enabled = true;

        //De-reference object
        _selectedGameObject = null;
        _selectedRigidBody = null;

        //Disable listening
        _listeningForGesture = false;


    }

    //Fixates the selected object to the players main camera
    void SetVelocityTowards(Rigidbody objectToMove, Vector3 targetPosition, Vector3 sourcePosition)
    {
        //Get distance from target position
        var vDist = Vector3.Distance(targetPosition, sourcePosition);
        //Get direction to target position
        var shootDir = targetPosition - sourcePosition;
        shootDir.Normalize();
        
        //Set velocity
        objectToMove.velocity = shootDir * (vDist * 3);

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
