using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject heart_1;
    [SerializeField]
    GameObject heart_2;
    [SerializeField]
    GameObject heart_3;
    [SerializeField]
    GameObject heart_4;
    [SerializeField]
    GameObject heart_5;
    [SerializeField]
    GameObject heart_6;

    [SerializeField]
    Sprite fullHeartSprite;
    [SerializeField]
    Sprite halfHeartSprite;
    [SerializeField]
    Sprite emptyHeartSprite;

    private PlayerHealth playerHealth;

    void Start()
    {
        // Get the PlayerHealth component from the player object
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        int fullHearts = Mathf.FloorToInt(playerHealth.currentHealth);
        float halfHearts = playerHealth.currentHealth - fullHearts;

        SetHeartSprite(heart_1, playerHealth.currentHealth >= 1 ? fullHeartSprite : (playerHealth.currentHealth == 0.5 ? halfHeartSprite : emptyHeartSprite));
        SetHeartSprite(heart_2, playerHealth.currentHealth >= 2 ? fullHeartSprite : (playerHealth.currentHealth == 1.5 ? halfHeartSprite : emptyHeartSprite));
        SetHeartSprite(heart_3, playerHealth.currentHealth >= 3 ? fullHeartSprite : (playerHealth.currentHealth == 2.5 ? halfHeartSprite : emptyHeartSprite));
        SetHeartSprite(heart_4, playerHealth.currentHealth >= 4 ? fullHeartSprite : (playerHealth.currentHealth == 3.5 ? halfHeartSprite : emptyHeartSprite));
        SetHeartSprite(heart_5, playerHealth.currentHealth >= 5 ? fullHeartSprite : (playerHealth.currentHealth == 4.5 ? halfHeartSprite : emptyHeartSprite));
        SetHeartSprite(heart_6, playerHealth.currentHealth >= 6 ? fullHeartSprite : (playerHealth.currentHealth == 5.5 ? halfHeartSprite : emptyHeartSprite));
    }

    void SetHeartSprite(GameObject heartObject, Sprite sprite)
    {
        SpriteRenderer renderer = heartObject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }
    }
}