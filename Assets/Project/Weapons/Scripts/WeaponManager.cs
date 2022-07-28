using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoSingleton<WeaponManager>
{
    [SerializeField] public List<WeaponInfo> _weaponInfos;
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _inventoryPanel;


    // Start is called before the first frame update
    void Start()
    {
        foreach (WeaponConfig weaponConfig in _weaponConfigs)
        {
            GameObject card = Instantiate(_cardPrefab, _inventoryPanel.transform);
            card.GetComponent<WeaponInfo>().weaponConfig = weaponConfig;
            card.transform.Find("Sprite").GetComponent<Image>().sprite=weaponConfig.weaponSprite;
        }
    }

    public void EquipWeapon (WeaponInfo weaponInfo)
    {
        if (_weaponInfos.Count >= 2)
        {
            _weaponInfos[0].enabled = false;
            _weaponInfos[0] = _weaponInfos[1];
            _weaponInfos[1] = weaponInfo;
        }
        else
        {
            _weaponInfos.Add(weaponInfo);
        }  
    }
}
