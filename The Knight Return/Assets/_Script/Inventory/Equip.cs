using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour
{
    [SerializeField]
    private Sprite[] belts, body, boots, bracers, earrings, gloves, helmets, necklace, pants, rings, shoulders;

    private Transform _helmet, _necklace, _earrings, _shoulders, _bracers, _belt;
    private ArmorJewelry _selectedArmorJewelry;

    public static Equip Instance { get; private set; }
    public Transform Canvas { get; private set; }
    private enum ArmorJewelry
    {
        Helmet,
        Necklace,
        Earrings,
        Shoulders,
        Bracers,
        Belt,
        /*Gloves,
        Pants,
        Boots,
        Rings*/
    }

    void Awake()
    {
        Instance = this;
        Canvas = GameObject.Find("UICanvas").transform;
        _helmet = Canvas.Find("Equiped/Helmet");
        _necklace = Canvas.Find("Equiped/Necklace");
        _earrings = Canvas.Find("Equiped/Earrings");
        _shoulders = Canvas.Find("Equiped/Shoulders");
        _bracers = Canvas.Find("Equiped/Bracers");
        _belt = Canvas.Find("Equiped/Belt");

    }

    public void ValidateAndApplyDrop(GameObject dragGO, GameObject dropGO)
    {
        var dropTransform = dropGO.transform;
        switch (_selectedArmorJewelry)
        {
            case ArmorJewelry.Helmet:
                if (dropTransform == _helmet)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_helmet.transform);
                    icon.transform.SetParent(_helmet.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Necklace:
                if (dropTransform == _necklace)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_necklace.transform);
                    icon.transform.SetParent(_necklace.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Earrings:
                if (dropTransform == _earrings)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_earrings.transform);
                    icon.transform.SetParent(_earrings.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Shoulders:
                if (dropTransform == _shoulders)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_shoulders.transform);
                    icon.transform.SetParent(_shoulders.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());                
                }
                break;
            case ArmorJewelry.Bracers:
                if (dropTransform == _bracers)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_bracers.transform);
                    icon.transform.SetParent(_bracers.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Belt:
                if (dropTransform == _belt)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_belt.transform);
                    icon.transform.SetParent(_belt.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            /*case ArmorJewelry.Gloves:
                if (dropTransform == _gloves1 || dropTransform == _gloves2)
                {
                    var icon = Instantiate(dragGO);
                    var icon2 = Instantiate(dragGO);
                    DestroyChildren(_gloves1.transform);
                    DestroyChildren(_gloves2.transform);
                    icon2.transform.rotation = Quaternion.Euler(0, 180, 0);
                    icon.transform.SetParent(_gloves1.transform, false);
                    icon2.transform.SetParent(_gloves2.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                    StretchIcon(icon2.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Pants:
                if (dropTransform == _pants)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_pants.transform);
                    icon.transform.SetParent(_pants.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Boots:
                if (dropTransform == _boots)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_boots.transform);
                    icon.transform.SetParent(_boots.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;
            case ArmorJewelry.Rings:
                if (dropTransform == _ring1)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_ring1.transform);
                    icon.transform.SetParent(_ring1.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                else if (dropTransform == _ring2)
                {
                    var icon = Instantiate(dragGO);
                    DestroyChildren(_ring2.transform);
                    icon.transform.SetParent(_ring2.transform, false);
                    StretchIcon(icon.GetComponent<RectTransform>());
                }
                break;*/
        }
    }

    private void StretchIcon(RectTransform rt)
    {
        rt.anchorMax = Vector3.one;
        rt.anchorMin = Vector3.zero;
        rt.anchoredPosition = Vector3.zero;
    }

    private void DestroyChildren(Transform parent)
    {
        if (parent.childCount > 0)
            foreach (Transform child in parent)
                Destroy(child.gameObject);
    }
}
