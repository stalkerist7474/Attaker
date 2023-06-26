using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{

    [SerializeField] private int _heath;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootpoint;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber;
    private int _currentHeath;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    
    
    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentWeapon = _weapons[0];
        _currentHeath = _heath;
        _animator = GetComponent<Animator>();
    }

    
    private void Update()
    {
        
       if (Input.GetMouseButtonDown(0)) 
            {
            _currentWeapon.Shoot(_shootpoint);
            }
       
    }
    public void ApplyDamage(int damage)
    {
        _currentHeath -= damage;
        HealthChanged?.Invoke(_currentHeath, _heath);

        if (_currentHeath <= 0) 
        {

            Destroy(gameObject);
        }
    }



    private void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
        
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviosWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
