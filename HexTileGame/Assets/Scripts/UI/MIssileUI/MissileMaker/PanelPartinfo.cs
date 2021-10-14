using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartinfo : MonoBehaviour
{
    public PanelPartSelector partSelector;

    [SerializeField] Text textName;
    [SerializeField] Toggle myToggle;

    public void InitPanelPartInfo(MissileWarheadData warhead, ToggleGroup tg)
    {
        textName.text = warhead.Name;

        myToggle.group = tg;
        myToggle.onValueChanged.RemoveAllListeners();
        myToggle.onValueChanged.AddListener( isOn =>
        {
            if (isOn)
            {
                partSelector.InitPartText(warhead);
            }
        });
    }

    public void InitPanelPartInfo(MissileEngineData engine, ToggleGroup tg)
    {
        textName.text = engine.Name;
        partSelector.InitPartText(engine);

        myToggle.group = tg;
        myToggle.onValueChanged.RemoveAllListeners();
        myToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                partSelector.InitPartText(engine);
            }
        });
    }
}
