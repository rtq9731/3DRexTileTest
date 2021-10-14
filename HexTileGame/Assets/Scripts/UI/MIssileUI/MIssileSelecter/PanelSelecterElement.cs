using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelecterElement : MonoBehaviour
{
    [SerializeField] Text textMissileWarhead;
    [SerializeField] Text textMissileRange;

    public Action onClickBtnSelect;

    private Transform selectedParent;
    private Transform unSelectedParent;

    MissileData data;
    public MissileData Data
    {
        get { return data; }
    }

    Button btnSelectMissile;

    bool isSelected = false;
    public bool IsSeleted
    {
        get { return isSelected; }
    }

    private void Awake()
    {
        onClickBtnSelect = () => { };
        btnSelectMissile = GetComponent<Button>();
    }

    public void InitPanelSelecterElement(Transform selectedParent, Transform unSelectedParent, MissileData data)
    {
        this.selectedParent = selectedParent;
        this.unSelectedParent = unSelectedParent;

        btnSelectMissile.onClick.RemoveAllListeners();
        btnSelectMissile.onClick.AddListener(Select);

        isSelected = false;
        this.data = data;

        SetData();

        gameObject.SetActive(true);
    }
    
    public void SetData() 
    {
        textMissileWarhead.text = data.WarheadType.ToString();
        textMissileRange.text = data.EngineTier.ToString();
    }
    
    public void Select()
    {
        isSelected = true;
        transform.SetParent(selectedParent);

        onClickBtnSelect();
        btnSelectMissile.onClick.RemoveListener(Select);
        btnSelectMissile.onClick.AddListener(Deselect);
    }

    public void Deselect()
    {
        isSelected = false;
        transform.SetParent(unSelectedParent);

        onClickBtnSelect();
        btnSelectMissile.onClick.RemoveListener(Deselect);
        btnSelectMissile.onClick.AddListener(Select);
    }

}
