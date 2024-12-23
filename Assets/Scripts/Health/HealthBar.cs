using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public EnemyHealth eHealth;
    private void Start()
    {
        //e = GetComponent<EnemyHealth>();
        eHealth.OnHealthChanged += E_OnHealthChanged;
    }

    private void E_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(eHealth.GetHealthPercent(), 1);
    }

    private void Update()
    {
        //transform.Find("Bar").localScale = new Vector3(e.GetHealthPercent(), 1);
        //Debug.Log(e.GetHealthPercent());
    }
}
