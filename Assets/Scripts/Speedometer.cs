using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody target;
    public PostProcessProfile razmytie;
    ChromaticAberration gg;
    public float maxSpeed = 0.0f; 

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;
    
    [Header("UI")]
    public Text speedLabel;
    public RectTransform arrow;

    private float speed = 0.0f;
    private void Update()
    {
        speed = target.velocity.magnitude * 3.6f;
        
        if (speedLabel != null)
        {
            speedLabel.text = ((int)speed) + " km/h";
            gg=razmytie.GetSetting<ChromaticAberration>();
            gg.intensity.value=speed / 200 * 0.33f;
        }
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}