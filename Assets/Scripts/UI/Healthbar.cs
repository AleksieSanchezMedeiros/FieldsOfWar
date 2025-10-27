using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    Transform cam;
    public void setMaxValue(int val)
    {
        slider.maxValue = val;
        slider.value = val;
    }

    public void reduceHP(int val)
    {
        slider.value -= val;
    }

    void LateUpdate()
    {
        if(!cam) cam = FindFirstObjectByType<Camera>().gameObject.transform;
        transform.LookAt(transform.position + cam.forward);
    }
}