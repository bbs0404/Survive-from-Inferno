using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Image scrollController;
    [SerializeField]
    private Animator playerAnimator;
    private float constant = 1;

    public GameObject background;
    public static bool isMoving = false;

    void Update()
    {
        if(InGameSystemManager.Inst().stamina < 10)
        {
            constant = 0.5f;
        }
        else
        {
            constant = 1;
        }
        if (!InGameSystemManager.Inst().isGameOver)
        {
            if (scrollController.rectTransform.localPosition.x > 25)
                scrollController.rectTransform.localPosition = new Vector3(25, scrollController.rectTransform.localPosition.y);
            if (scrollController.rectTransform.localPosition.x < -25)
                scrollController.rectTransform.localPosition = new Vector3(-25, scrollController.rectTransform.localPosition.y);
            if (scrollController.rectTransform.localPosition.x > 1)
            {
                player.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f), 0) * constant;
                background.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f) *constant * 4 / 5f, 0);
                InGameSystemManager.Inst().distance += scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f);
                playerAnimator.SetBool("RUN_right", true);
                playerAnimator.SetBool("RUN_left", false);
                PlayerManager.Inst().player.GetComponent<SpriteRenderer>().flipX = false;
                if (scrollController.rectTransform.localPosition.x > 10)
                    InGameSystemManager.Inst().stamina -= scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f) * 2;
                if (InGameSystemManager.Inst().stamina < 0)
                    InGameSystemManager.Inst().stamina = 0;
                isMoving = true;
            }
            else if (scrollController.rectTransform.localPosition.x < -1)
            {
                player.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f), 0) * constant;
                if (player.transform.position.x < 0)
                    player.transform.localPosition -= new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f), 0) * constant;
                else
                {
                    background.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f) * constant * 4 / 5f, 0);
                    InGameSystemManager.Inst().distance += scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f);
                    playerAnimator.SetBool("RUN_left", true);
                    playerAnimator.SetBool("RUN_right", false);
                    PlayerManager.Inst().player.GetComponent<SpriteRenderer>().flipX = true;
                    if (scrollController.rectTransform.localPosition.x < -10)
                        InGameSystemManager.Inst().stamina += scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f) * 2;
                    if (InGameSystemManager.Inst().stamina < 0)
                        InGameSystemManager.Inst().stamina = 0;
                    isMoving = false;
                }
            }
            else
            {
                playerAnimator.SetBool("RUN_right", false);
                playerAnimator.SetBool("RUN_left", false);
                InGameSystemManager.Inst().stamina += InGameSystemManager.Inst().stamCharge * Time.deltaTime;
                if (InGameSystemManager.Inst().stamina > 100)
                    InGameSystemManager.Inst().stamina = 100;
                isMoving = false;
            }
            if (Mathf.Abs(scrollController.rectTransform.localPosition.x) > 20)
            {
                InGameSystemManager.Inst().water -= (Mathf.Abs(scrollController.rectTransform.localPosition.x) / 5) * Gametime.deltaTime;
                if (InGameSystemManager.Inst().water < 0)
                    InGameSystemManager.Inst().water = 0;
            }
        }
    }
}
