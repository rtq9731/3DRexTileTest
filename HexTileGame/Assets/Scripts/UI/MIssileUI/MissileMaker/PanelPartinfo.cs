using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartinfo : MonoBehaviour
{
    public PanelPartSelector partSelector;

    [SerializeField] Text textName;
    [SerializeField] Toggle myToggle;

    public void InitPanelPartInfo(MissileWarheadData warhead)
    {
        textName.text = warhead.Name;

        myToggle.onValueChanged.RemoveAllListeners();
        myToggle.onValueChanged.AddListener( isOn =>
        {
            if (isOn)
            {
                partSelector.InitPartSelector(warhead);
            }
        });
    }

    public void InitPanelPartInfo(MissileEngineData engine)
    {
        textName.text = engine.Name;
        partSelector.InitPartSelector(engine);

        myToggle.onValueChanged.RemoveAllListeners();
        myToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                partSelector.InitPartSelector(engine);
            }
        });
    }
}
