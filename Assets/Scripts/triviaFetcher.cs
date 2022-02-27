using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//https://opentdb.com/api_config.php
//https://opentdb.com/api.php?amount=10&category=12&difficulty=medium&type=multiple
public class triviaFetcher : MonoBehaviour
{
    [SerializeField] private int gameLength;
    private int _questionCount = -1;
    public bool isListReady = false;
    List<triviaQuestion> questionList = new List<triviaQuestion>();

    private void Start()
    {
        generateQuestionList();        
    }
    
    public triviaQuestion getSpecificQuestion(int round) 
    {
        return questionList[round];
    }

    public triviaQuestion getNextQuestion() 
    {
        if (_questionCount==questionList.Count-1)
        {
            Debug.LogWarning("Generating new questions!");
            generateQuestionList();
            _questionCount = -1;
        }

        _questionCount++;
        return questionList[_questionCount];        
    }

    public void generateQuestionList()
    {
        //string URL = "https://trivia.willfry.co.uk/api/questions?categories=general_knowledge,movies,music&limit=10";
        //String builder, get playerPrefs and set all genres
        string URL = "https://trivia.willfry.co.uk/api/questions?categories=" + stringifyPlayerPrefs();
        //URL = "https://opentdb.com/api.php?amount=10&category=12&difficulty=medium&type=multiple";

        StartCoroutine(ProcessRequest(URL));

        IEnumerator ProcessRequest(string uri)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    questionList = JsonConvert.DeserializeObject<List<triviaQuestion>>(request.downloadHandler.text);
                    isListReady = true;
                }
            }            
        }
        
    }

    private string stringifyPlayerPrefs()
    {
        string genres = "";
        
        for (int i = 0; i < 9; i++)
        {
            genres += PlayerPrefs.GetString(i.ToString()) + ",";
        }

        return genres;
    }

    private void translateQuestions(string txtTrans) 
    {
        string URL = "https://deep-translate1.p.rapidapi.com/language/translate/v2";
        string textToTranslate = "{\r\"q\": \"Hello World!\",\r\"source\": \"en\",\r\"target\": \"es\"\r}"; 
        //string textToTranslate = "q" +":"+"Hello World!"+","+"source"+":"+"en"+","+"target"+":"+"es"; 
       
        StartCoroutine(ProcessTranslation(URL));

        IEnumerator ProcessTranslation(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            request.SetRequestHeader("content-type", "application/json");
            request.SetRequestHeader("x-rapidapi-key", "e0eb241ba1msh9eaee5094dc1639p11904cjsn3e0617f6d631");
            request.SetRequestHeader("x-rapidapi-host", "deep-translate1.p.rapidapi.com");
            request.SetRequestHeader("application/json", textToTranslate); 

            using (request = UnityWebRequest.Get(uri))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    //questionList = JsonConvert.DeserializeObject<List<triviaQuestion>>(request.downloadHandler.text);   
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }
    }


}
