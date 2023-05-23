using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    Health health;
    [SerializeField]
    BoxCollider2D boxCollider;
    [SerializeField]
    Canvas canvas;

    private void Update()
    {
        SetHealth();
        StartCoroutine(DisableCanvas()); 
    }
    private IEnumerator DisableCanvas()
    {
        yield return new WaitForSeconds(5f);
        if (boxCollider.enabled == false)
        {
            canvas.enabled = false;
        }

    }
    public void SetHealth()
    {
    
        slider.value = 1 - (health.currentHealth / health.startingHealth);
    }

}
