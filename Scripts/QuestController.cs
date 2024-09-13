using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas QuestUI;
    public TMP_InputField QuestInput;
    public Button QuestButtonSubmit;
    public Canvas QuestSubmitNotify;
    private bool QuestSubmitNotifyState = false;
    public TextMeshProUGUI QuestSubmitNotifyText;
    private string getInputText;
    private bool showQuest = false;
    public static bool cursorLocker;
    public int questNumber = 1;
    private bool IsClicked = true;

    //
    public static bool GirlGhostShedEventisActive = false;

    //
    bool answerCheck = true;
    void Start()
    {
        QuestSubmitNotify.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        QuestUI.enabled = showQuest;
        QuestInput.enabled = showQuest;
        getInputText = QuestInput.text;
    }

    public void OnSelect()
    {
        QuestInput.Select();
        QuestInput.caretPosition = 0;
    }

    IEnumerator showNotify(string notifyStatusQuest) {
        QuestSubmitNotifyText.text = notifyStatusQuest;
        QuestSubmitNotify.enabled = true;
        yield return new WaitForSeconds(3f);
        QuestSubmitNotify.enabled = false;
        QuestSubmitNotifyText.text = "";
    }

    public void submitQuest() {
        Debug.Log("Submit Quest!");
        Debug.Log(getInputText);
        if (getInputText.Length > 0 && IsClicked) {
            answerCheck = GameManager.setQuestData(getInputText, questNumber);
            IsClicked = answerCheck == true ? false : true;
            SpawnSlender.IsRespawned = answerCheck == true ? true : false;
            StartCoroutine(showNotify(GameManager.NotifyStatusQuest));
            StopCoroutine(showNotify(GameManager.NotifyStatusQuest));
            if (questNumber == 4) {
                GirlGhostShedEventisActive = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                showQuest = true;
                cursorLocker = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            showQuest = false;
            cursorLocker = false;
        }
    }
}