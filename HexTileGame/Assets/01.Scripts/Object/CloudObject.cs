using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudObject : MonoBehaviour
{
    [SerializeField] float removeTime = 1f;

    [SerializeField] GameObject cloud1;
    [SerializeField] GameObject cloud2;
    [SerializeField] GameObject cloud3;

    public void RemoveCloud()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(cloud1.transform.DOMove(new Vector3(cloud1.transform.position.x - 0.25f, cloud1.transform.position.y, cloud1.transform.position.z - 0.25f), removeTime)).SetEase(Ease.OutQuart);
        seq.Join(cloud1.transform.DOScale(Vector3.zero, removeTime));

        seq.Join(cloud2.transform.DOMove(new Vector3(cloud2.transform.position.x + 0.25f, cloud2.transform.position.y, cloud2.transform.position.z - 0.25f), removeTime)).SetEase(Ease.OutQuart);
        seq.Join(cloud2.transform.DOScale(Vector3.zero, removeTime));

        seq.Join(cloud3.transform.DOMove(new Vector3(cloud3.transform.position.x - 0.25f, cloud3.transform.position.y, cloud3.transform.position.z + 0.25f), removeTime)).SetEase(Ease.OutQuart);
        seq.Join(cloud3.transform.DOScale(Vector3.zero, removeTime));

        seq.OnComplete(() => gameObject.SetActive(false));
        // ��� �������� ������ �������� �����̰� �ٿ���
    }
}
