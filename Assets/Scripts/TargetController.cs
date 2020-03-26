using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public GameController gameController;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        if (gameObject != null) {
            Destroy(gameObject);
            if (gameController != null) {
                gameController.CoinHit();
            }
        }
    }
}
