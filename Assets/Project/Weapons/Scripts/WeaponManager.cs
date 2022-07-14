using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager>
{
    [SerializeField] private List<WeaponConfig> _weaponConfigs;

    public WeaponConfig GetWeaponConfig(WeaponID weaponID)
    {
        return this._weaponConfigs.First(r => r.weaponID == weaponID);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
