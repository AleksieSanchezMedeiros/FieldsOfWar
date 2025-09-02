using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    Transform cam;
    void Awake()
    {
        cam = FindFirstObjectByType<Camera>().gameObject.transform;
    }
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
        transform.LookAt(transform.position + cam.forward);
    }
}