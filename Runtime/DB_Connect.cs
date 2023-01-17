using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

namespace JelleVer.DatabaseTools
{ 
    // connects to the database through a webrequest
    // todo convert to async await
    // todo add single element download

    /// <summary>
    /// The main connection to the databse server
    /// </summary>
    public class DB_Connect : MonoBehaviour
    {
        [Header("Server Files")]
        [SerializeField]
        [Tooltip("The base URL for the server PHP files")]
        private string serverUrl = "";
        [SerializeField]
        [Tooltip("The second part of the url for uploading data, be sure to include the '/' at the start.")]
        private string uploadDataUrl = "";
        [SerializeField]
        [Tooltip("The second part of the url for downloading, be sure to include the '/' at the start.")]
        private string downloadDataUrl = "";
        [Header("Table Settings")]
        [SerializeField]
        [Tooltip("The name of the table to store the data to")]
        private string tableName = "";

        [Header("Callbacks for different server events")]
        [Tooltip("Called when 'TryUploadData' has been excecuted correctly")]
        public UnityEvent uploadSuccesEvent = new UnityEvent();

        [Tooltip("Called when 'TryDownloadData' has been excecuted correctly \n contains all the UserData's in an array")]
        public UserDataArrayEvent downloadSuccesEvent = new UserDataArrayEvent();


        [Tooltip("The Error responce code in case of a PHP error")]
        private string errorCode = "MySQL_ERROR";

        /// <summary>
        /// Start Uploading data to the serever
        /// </summary>
        /// <param name="username">the name of the user to link the data to</param>
        /// <param name="data">the actual data, to be serialised to Json</param>
        public void TryUploadData(string username, Object data)
        {
            StartCoroutine(UploadData(username, JsonUtility.ToJson(data)));
        }

        /// <summary>
        /// Start Uploading data to the serever
        /// </summary>
        /// <param name="username">the name of the user to link the data to</param>
        /// <param name="data">the actual data</param>
        public void TryUploadData(string username, string data)
        {
            StartCoroutine(UploadData(username, data));
        }

        IEnumerator UploadData(string username, string data)
        {
            Log("Starting Data Upload...");

            // create a new form to send to the database
            List<IMultipartFormSection> postForm = new List<IMultipartFormSection>();
            postForm.Add(new MultipartFormDataSection("table", tableName));
            postForm.Add(new MultipartFormDataSection("username", username));
            postForm.Add(new MultipartFormDataSection("data", data));

            UnityWebRequest webRequest = UnityWebRequest.Post(serverUrl + uploadDataUrl, postForm);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) // check for any network errors
            {
                Log(webRequest.error, LogType.Error);
            }
            else if (webRequest.downloadHandler.text.Contains(errorCode)) // check for any database error
            {
                Log(webRequest.downloadHandler.text, LogType.Warning);
            }
            else
            {
                Log("Data Uploaded Successfully for user: " + name + ", with return: " + webRequest.downloadHandler.text);
                uploadSuccesEvent.Invoke(); // call the unity event
            }

        }

        /// <summary>
        /// Starts Downloading the data from the server
        /// </summary>
        public void TryDownloadData()
        {
            StartCoroutine(DownloadData());
        }

        IEnumerator DownloadData()
        {
            Log("Starting Data Download...");

            UnityWebRequest webRequest = UnityWebRequest.Get(serverUrl + downloadDataUrl + "?table=" + tableName);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) // check for any network errors
            {
                Log(webRequest.error, LogType.Error);
            }
            else if (webRequest.downloadHandler.text.Contains(errorCode)) // check for any database error
            {
                Log(webRequest.downloadHandler.text, LogType.Warning);
            }
            else
            {
                Log("Data downloaded Successfully with return: " + webRequest.downloadHandler.text);
                ParseDownloadData(webRequest.downloadHandler.text);
            }
        }

        void ParseDownloadData(string data)
        {
            //The data should be returned as a Json formatted 'UserDataArray' wrapper for the actual Array, there is no support for direct arrays in JsonUtility
            UserDataArray userDatas = JsonUtility.FromJson<UserDataArray>(data);
            downloadSuccesEvent.Invoke(userDatas.userDataArray);
        }


        // Easy logging
        void Log(string message, LogType logType = LogType.Log)
        {
            string newMessage = "<b><color=blue>DB_Connect: </color></b>" + message;

            switch (logType)
            {
                case LogType.Warning:
                    Debug.LogWarning(newMessage);
                    break;
                case LogType.Error:
                    Debug.LogError(newMessage);
                    break;
                default:
                    Debug.Log(newMessage);
                    break;
            }
        }

    }

    
}

