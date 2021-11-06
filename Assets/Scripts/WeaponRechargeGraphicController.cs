using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRechargeGraphicController : MonoBehaviour
{
    public GameObject graphic;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cooldownfrac = (float)player.GetComponent<PlayerController>().timeSinceLastAttack / (float)player.GetComponent<PlayerController>().knifeCooldown;

        if (player.GetComponent<PlayerController>().bowActive) {
            cooldownfrac = (float)player.GetComponent<PlayerController>().timeSinceLastAttack / (float)player.GetComponent<PlayerController>().bowCooldown;
        }

        graphic.GetComponent<Image>().fillAmount = 1 - cooldownfrac;
        
    }
}
