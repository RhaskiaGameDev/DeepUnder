using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Value
{
    public float current;
    public float max;
    public float decrease;
    public Slider slider;

    public Value(float current, float max)
    { this.current = current; this.max = max; this.decrease = 0; }

    public Value(float current, float max, float decrease)
    { this.current = current; this.max = max; this.decrease = decrease; }
}

public class PlayerManager : MonoBehaviour
{
    public Value health, o2, power;
    public Value fear;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Fear
        fear.current += health.current / health.max;

        //O2
        o2.current -= (fear.current / fear.max) * o2.decrease * Time.deltaTime;

        //Sliders
        var values = new Value[4] { health, o2, power, fear };
        foreach (var value in values)
        {
            value.current = Mathf.Clamp(value.current, 0, value.max);

            value.slider.maxValue = value.max;
            value.slider.value = value.current;
        }

        //Death
        if (health.current == 0) Die();
    }

    void Die()
    {
        Debug.LogWarning("get good");
    }

    public void TakeDamage() { health.current--; }
}
