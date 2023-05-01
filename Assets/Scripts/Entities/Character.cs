using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private MovementController _movementController;
    [SerializeField] private Gun _gun;

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

    #region COMMANDS
    private CmdMovement _cmdMovementForward;
    private CmdMovement _cmdMovementBack;
    private CmdMovement _cmdMovementLeft;
    private CmdMovement _cmdMovementRight;

    private CmdAttack _cmdAttack;
    private CmdReload _cmdReload;

    private CmdApplyDamage _cmdApplyDamage;
    #endregion

    void Start()
    {
        _movementController = GetComponent<MovementController>();

        _cmdMovementForward = new CmdMovement(_movementController, transform.forward);
        _cmdMovementBack = new CmdMovement(_movementController, -transform.forward);
        _cmdMovementLeft = new CmdMovement(_movementController, -transform.right);
        _cmdMovementRight = new CmdMovement(_movementController, transform.right);

        _cmdApplyDamage = new CmdApplyDamage(GetComponent<IDamagable>(), 10);

        ChangeWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEvent(_cmdMovementForward);
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddEvent(_cmdMovementBack);
        if (Input.GetKey(_moveRight)) EventQueueManager.instance.AddEvent(_cmdMovementRight);
        if (Input.GetKey(_MoveLeft)) EventQueueManager.instance.AddEvent(_cmdMovementLeft);

        if (Input.GetKey(_shoot)) EventQueueManager.instance.AddEventToQueue(_cmdAttack);
        if (Input.GetKeyDown(_reload)) EventQueueManager.instance.AddEventToQueue(_cmdReload);

        if (Input.GetKeyDown(KeyCode.Return)) EventManager.instance.EventGameOver(true);
        if (Input.GetKeyDown(KeyCode.Backspace)) EventQueueManager.instance.AddEventToQueue(_cmdApplyDamage);

        if (Input.GetKeyDown(_Pistol)) ChangeWeapon(0);
        if (Input.GetKeyDown(_machingun)) ChangeWeapon(1);
        if (Input.GetKeyDown(_shotgun)) ChangeWeapon(2);
    }

    private void ChangeWeapon(int index)
    {
        foreach (var gun in _weapons) gun.gameObject.SetActive(false);
        _weapons[index].gameObject.SetActive(true);
        
        _gun = _weapons[index];
        _cmdAttack = new CmdAttack(_gun);
        _cmdReload = new CmdReload(_gun);
    }
}
