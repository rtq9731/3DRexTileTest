using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clock : MonoBehaviour
{
    [SerializeField] RectTransform longHand;
    [SerializeField] RectTransform shrotHand;

    float longHandZRotation;
    float shrotHandZRotation;

    private void OnMouseOver()
    {
        longHandZRotation = longHand.rotation.z - Time.deltaTime / 5;
        longHand.rotation = Quaternion.Euler(new Vector3(0, 0, longHandZRotation));

        shrotHandZRotation = longHand.rotation.z - Time.deltaTime * 12;
        shrotHand.rotation = Quaternion.Euler(new Vector3(0, 0, shrotHandZRotation));
    }
}
