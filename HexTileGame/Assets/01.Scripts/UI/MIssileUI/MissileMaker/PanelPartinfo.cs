using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartinfo : MonoBehaviour
{
    public PanelPartSelector partSelector;

    [SerializeField] Text textName;
    [SerializeField] Button btn;

    public void InitPanelPartInfo(MissileWarheadData warhead)
    {
        textName.text = warhead.Name;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener( () =>
        {
            partSelector.InitPartText(warhead);
        });
    }

    public void InitPanelPartInfo(MissileEngineData engine)
    {
        textName.text = engine.Name;
        partSelector.InitPartText(engine);

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            partSelector.InitPartText(engine);
        });
    }
    public void InitPanelPartInfo(BodyData body)
    {
        textName.text = body.Name;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            partSelector.InitPartText(body);
        });
    }

}
