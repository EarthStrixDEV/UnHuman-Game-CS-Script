using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static List<string> answerQuest = new List<string>() {
        "kusrc",
        "cpe",
        "python",
        "3",
        "23"
    };

    public static bool[] completeStateQuest = {
        false, false, false ,false, false
    };

    public static string NotifyStatusQuest;
    private static string getAnswer;
    private static int questNumber;

    int totalQuestCompleted;
    int completeStateQuestLength;
    
    void Start()
    {
        totalQuestCompleted = 0;
        completeStateQuestLength = completeStateQuest.Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }

        totalQuestCompleted = CheckCompletedQuest();
        if (totalQuestCompleted == completeStateQuestLength) {
            Debug.Log("All Quest Completed!");
            StartCoroutine(LoadToCompleteScene());
        }
    }

    IEnumerator LoadToCompleteScene() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("CompleteGameplay");
    }

    private int CheckCompletedQuest() {
        int count = 0;
        foreach (var quest in completeStateQuest)
        {
            if (quest == true) {
                count++;
            }
        }
        return count;
    }

    public void PlayButton() {
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitButton() {
        Application.Quit();
    }

    public static bool setQuestData(string answer ,int Number) {
        getAnswer = answer;
        questNumber = Number;
        var result = answerQuest.Where<string>(ans => ans.ToLower().Equals(answer.ToLower()));
        Debug.Log("result: " + result);
        if (result.Any()) {
            NotifyStatusQuest = "ทำเควสสำเร็จ";
            completeStateQuest[questNumber - 1] = true;
            return true;
        } else {
            NotifyStatusQuest = "คำตอบไม่ถูกต้อง";
            completeStateQuest[questNumber - 1] = false;
            return false;
        }
    }
}