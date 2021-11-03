using UnityEngine;
using UnityEngine.Events;

namespace JelleVer.DatabaseTools
{
    /// <summary>
    /// A custom class to store the Username and Data together
    /// </summary>
    [System.Serializable]
    public class UserData
    {
        [Tooltip("The identifier for the data")]
        public string username = "";
        [Tooltip("This can contain all the data you want, if formatted as a json string, it can contain a whole serialisable class")]
        public string data = "";
    }

    /// <summary>
    /// A wrapper class for a UserData array so it can be parsed correctly from Json
    /// </summary>
    [System.Serializable]
    public class UserDataArray
    {
        public UserData[] userDataArray;
    }

    /// <summary>
    /// A custom event To return an array of UserData's
    /// </summary>
    [System.Serializable]
    public class UserDataArrayEvent : UnityEvent<UserData[]> { }
}
