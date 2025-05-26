using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject nw1;
    public GameObject nw2;
    public GameObject sw1;
    public GameObject sw2;

    List<GameObject> _weapons = new List<GameObject>();



    private void Awake()
    {
        _weapons.Add(nw1);
        _weapons.Add(nw2);
        _weapons.Add(sw1);
        _weapons.Add(sw2);
    }



    int _currentWeapon = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(transform.GetChild(0).gameObject);
            _currentWeapon = (_currentWeapon + 1) % _weapons.Count;
            Instantiate(_weapons[_currentWeapon], transform);
        }
    }

    public void upgradeWeapon(GameObject from, GameObject to)
    {
        Destroy(transform.GetChild(0).gameObject);
        _weapons.Remove(from);
        _weapons.Add(to);
        _currentWeapon = _weapons.IndexOf(to);
        Instantiate(_weapons[_currentWeapon], transform);
    }

    public void addWeapon(GameObject weapon)
    { 
        _weapons.Add(weapon);

        if (transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
        }

        _currentWeapon = _weapons.IndexOf(weapon);

        Instantiate(weapon, transform.position, Quaternion.identity);
    }


}
