using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireMissileInfo : MonoBehaviour
{
    [SerializeField] Image warheadIcon;
    [SerializeField] Text warheadNameText;
    [SerializeField] Text warheadDamageText;

    public void UpdateFireMissileInfo(MissileTypes.MissileWarheadType type)
    {
        warheadIcon.sprite = MainSceneManager.Instance.GetWarheadSprite(type);
        warheadNameText.text = MainSceneManager.Instance.GetWarheadData(type).Name;
        warheadDamageText.text = $"탄두 데미지 {MainSceneManager.Instance.GetWarheadData(type).Atk}";
    }    

    public void UpdateFireMissileInfoToNull()
    {
        warheadIcon.sprite = MainSceneManager.Instance.GetWarheadSprite(MissileTypes.MissileWarheadType.CommonTypeWarhead);
        warheadNameText.text = "지정되지 않음!";
        warheadDamageText.text = "";
    }
}
