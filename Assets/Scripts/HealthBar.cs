using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;
    public HeadAnt headAnt;
    public Image meterImage;
    public TextMeshProUGUI hpText;
    float maxHitPoints;
    // Start is called before the first frame update
    void Start()
    {
        maxHitPoints = headAnt.maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (headAnt != null) {
            meterImage.fillAmount = hitPoints.value / maxHitPoints;
            hpText.text = "HP: " + (meterImage.fillAmount * 100);
        }
    }
}
