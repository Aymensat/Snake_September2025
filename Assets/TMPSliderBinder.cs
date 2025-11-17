using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPSliderBinder : MonoBehaviour
{
    public Slider slider;           // assign in Inspector
    public TMP_Text valueText;      // assign in Inspector
    public string format = "{0:0}"; // change per-instance in Inspector

    private UnityEngine.Events.UnityAction<float> listener;

    void Start()
    {
        if (slider == null || valueText == null) return;

        listener = UpdateText;
        slider.onValueChanged.AddListener(listener);

        // initialize UI from current slider value
        UpdateText(slider.value);
    }

    void UpdateText(float v) => valueText.SetText(format, v);

    void OnDestroy()
    {
        if (slider != null && listener != null)
            slider.onValueChanged.RemoveListener(listener);
    }
}
