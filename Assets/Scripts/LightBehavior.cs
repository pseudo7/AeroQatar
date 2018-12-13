using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material onMat, offMat;


    void Awake()
    {
        meshRenderer = GetComponentInParent<MeshRenderer>();
        onMat = FindObjectOfType<Utility>().onMaterial;
        offMat = FindObjectOfType<Utility>().offMaterial;
    }

    void OnEnable()
    {
        meshRenderer.material = onMat;
        // meshRenderer.material = Utility.Instance.onMaterial;
    }

    void OnDisable()
    {
        meshRenderer.material = offMat;
        // meshRenderer.material = Utility.Instance.offMaterial;
    }
}
