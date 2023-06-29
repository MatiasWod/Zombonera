using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Character : MonoBehaviour
{
    private MovementController _movementController;
    private CameraController _cameraController;

    // BINDING ATTACK KEYS
    [SerializeField] private KeyCode _shoot = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;
    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _MoveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;

    // SWITCH WEAPONS
    [SerializeField] private KeyCode _Pistol = KeyCode.Alpha1;
    [SerializeField] private KeyCode _machingun = KeyCode.Alpha2;
    [SerializeField] private KeyCode _shotgun = KeyCode.Alpha3;
    [SerializeField] private List<Gun> _weapons;

    [SerializeField] private Gun _gun;

    [SerializeField] private float _sens;

	[SerializeField] private LayerMask layermask;
	public AudioSource septima;
	bool m_ToggleChange = false;

    #region COMMANDS
    private CmdMovement _cmdMovementForward;
    private CmdMovement _cmdMovementBack;
    private CmdMovement _cmdMovementLeft;
    private CmdMovement _cmdMovementRight;
    private CmdMovement _cmdMovementZero;

    private CmdAttack _cmdAttack;
    private CmdReload _cmdReload;

    private CmdApplyDamage _cmdApplyDamage;

    private int _currentWeapon = 0;
    private int moved = 0;

    #endregion

    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _cameraController = GetComponent<CameraController>();

        _cmdMovementForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMovementBack = new CmdMovement(_movementController, -Vector3.forward);
        _cmdMovementLeft = new CmdMovement(_movementController, -Vector3.right);
        _cmdMovementRight = new CmdMovement(_movementController, Vector3.right);
        _cmdMovementZero = new CmdMovement(_movementController, Vector3.zero);

        //_cmdRotateCamera = 

        _cmdApplyDamage = new CmdApplyDamage(GetComponent<IDamagable>(), 10);
        

        ChangeWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(_moveForward))
        {
            _cmdMovementForward.Execute();
            moved = 1;
        }
        if (Input.GetKey(_moveBack))
        {
            _cmdMovementBack.Execute();
            moved = 1;
        }
        if (Input.GetKey(_moveRight))
        {
            _cmdMovementRight.Execute();
            moved = 1;
        }
        if (Input.GetKey(_MoveLeft))
        {
            _cmdMovementLeft.Execute();
            moved = 1;
        }
        if(moved == 0)
        {
            _cmdMovementZero.Execute();
        }

        moved = 0;


        if (Input.GetKey(_shoot)) EventQueueManager.instance.AddEventToQueue(_cmdAttack);
        if (Input.GetKeyDown(_reload)) EventQueueManager.instance.AddEventToQueue(_cmdReload);

        if (Input.GetKeyDown(KeyCode.Return)) EventManager.instance.EventGameOver(true);
        if (Input.GetKeyDown(KeyCode.Backspace)) EventQueueManager.instance.AddEventToQueue(_cmdApplyDamage);

        if (Input.GetKeyDown(_Pistol)) ChangeWeapon(0);
        if (Input.GetKeyDown(_machingun)) ChangeWeapon(1);
        if (Input.GetKeyDown(_shotgun)) ChangeWeapon(2);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layermask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
			if(m_ToggleChange == false){
				septima.Play();
				m_ToggleChange = true;
			}
        }
        else
        {
			if(m_ToggleChange == true){
				septima.Stop();
				m_ToggleChange = false;
			}
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime ;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime ;
        _cameraController.RotateCamera(mouseX, mouseY);
    }

    private void ChangeWeapon(int index)
    {
        foreach (var gun in _weapons) gun.gameObject.SetActive(false);
        _weapons[index].gameObject.SetActive(true);

        _gun = _weapons[index];
        _cmdAttack = new CmdAttack(_gun);
        _cmdReload = new CmdReload(_gun);

        _gun.hudBullet( _gun._currentBulletCount , _gun.MaxBulletCount);

        _currentWeapon = index;
    }

    private void OnDestroy()
    {
        
    }
}
