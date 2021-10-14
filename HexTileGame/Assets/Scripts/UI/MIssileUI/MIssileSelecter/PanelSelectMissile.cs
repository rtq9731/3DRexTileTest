using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectMissile : MonoBehaviour
{
    [SerializeField] GameObject MissilePanelPrefab = null;

    [SerializeField] Button btnOk = null;

    [SerializeField] Transform unselectedMissilePanel = null;
    [SerializeField] Transform selectedMissilePanel = null;

    private List<MissileData> waitingMissiles = new List<MissileData>();
    private List<MissileData> fireReadyMissiles = new List<MissileData>();

    private PlayerScript player;

    private List<GameObject> missilePanelPool = new List<GameObject>();

    private void Awake()
    {
        btnOk.onClick.AddListener(FinishSelect);
    }

    private void FinishSelect()
    {
        if(fireReadyMissiles.Count < 1)
        {
            return;
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        waitingMissiles = player.missileReadyToShoot;
        fireReadyMissiles = new List<MissileData>();

        missilePanelPool.ForEach(x => x.SetActive(false));

        foreach (var item in waitingMissiles)
        {
            GetNewMissilePanel(out PanelSelecterElement element);
            element.InitPanelSelecterElement(selectedMissilePanel, unselectedMissilePanel, item);
            Debug.Log(element.onClickBtnSelect);
            element.onClickBtnSelect += () =>
            {
                if (element.IsSeleted)
                {
                    if (waitingMissiles.Contains(item))
                    {
                        waitingMissiles.Remove(item);
                        fireReadyMissiles.Add(item);
                    }
                }
                else
                {
                    if (fireReadyMissiles.Contains(item))
                    {
                        fireReadyMissiles.Remove(item);
                        waitingMissiles.Add(item);
                    }
                }
            };
        }
    }

    public GameObject GetNewMissilePanel(out PanelSelecterElement element)
    {
        GameObject result = null;
        element = null;
        if (missilePanelPool.Count > 1)
        {
            result = missilePanelPool.Find(x => !x.activeSelf);
            if (result != null)
            {
                element = result.GetComponent<PanelSelecterElement>();
            }
            else
            {
                result = Instantiate(MissilePanelPrefab, unselectedMissilePanel);
                missilePanelPool.Add(result);
                element = result.GetComponent<PanelSelecterElement>();
            }
        }
        else
        {
            result = Instantiate(MissilePanelPrefab, unselectedMissilePanel);
            missilePanelPool.Add(result);
            element = result.GetComponent<PanelSelecterElement>();
        }

        return result;
    }


}
