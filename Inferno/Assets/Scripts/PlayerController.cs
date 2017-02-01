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

    void Update()
    {
        if (!InGameSystemManager.Inst().isGameOver)
        {
            if (scrollController.rectTransform.localPosition.x > 25)
                scrollController.rectTransform.localPosition = new Vector3(25, scrollController.rectTransform.localPosition.y);
            if (scrollController.rectTransform.localPosition.x < -25)
                scrollController.rectTransform.localPosition = new Vector3(-25, scrollController.rectTransform.localPosition.y);
            if (scrollController.rectTransform.localPosition.x > 1)
            {
                player.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f), 0);
                InGameSystemManager.Inst().distance += scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f);
                playerAnimator.SetBool("RUN_right", true);
                playerAnimator.SetBool("RUN_left", false);
                PlayerManager.Inst().player.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (scrollController.rectTransform.localPosition.x < -1)
            {
                player.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f), 0);
                InGameSystemManager.Inst().distance += scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f);
                playerAnimator.SetBool("RUN_left", true);
                playerAnimator.SetBool("RUN_right", false);
                PlayerManager.Inst().player.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                playerAnimator.SetBool("RUN_right", false);
                playerAnimator.SetBool("RUN_left", false);
            }
            if (Mathf.Abs(scrollController.rectTransform.localPosition.x) > 15)
            {
                InGameSystemManager.Inst().water -= Mathf.Abs(scrollController.rectTransform.localPosition.x) / 50;
                if (InGameSystemManager.Inst().water < 0)
                    InGameSystemManager.Inst().water = 0;
            }
        }
    }
}
