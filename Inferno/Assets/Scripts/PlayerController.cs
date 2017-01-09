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
        if (scrollController.rectTransform.localPosition.x > 25)
            scrollController.rectTransform.localPosition = new Vector3(25, scrollController.rectTransform.localPosition.y);
        if (scrollController.rectTransform.localPosition.x < -25)
            scrollController.rectTransform.localPosition = new Vector3(-25, scrollController.rectTransform.localPosition.y);
        if (scrollController.rectTransform.localPosition.x > 1)
        {
            player.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f), 0);
            GameManager.Inst().distance = GameManager.Inst().distance + scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f);
            //playerAnimator.SetBool("RUN_right", true);
            //playerAnimator.SetBool("RUN_left", false);
        }
        else if (scrollController.rectTransform.localPosition.x < -1)
        {
            player.transform.localPosition += new Vector3(scrollController.rectTransform.localPosition.x * 0.008f* (1 + GameManager.Inst().speedLevel * 0.125f), 0);
            GameManager.Inst().distance = GameManager.Inst().distance + scrollController.rectTransform.localPosition.x * 0.008f * (1 + GameManager.Inst().speedLevel * 0.125f);
            //playerAnimator.SetBool("RUN_left", true);
            //playerAnimator.SetBool("RUN_right", false);
        }
        else
        {
            //playerAnimator.SetBool("RUN_right", false);
            //playerAnimator.SetBool("RUN_left", false);
        }
    }
}
