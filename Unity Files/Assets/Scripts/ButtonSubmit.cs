using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ButtonSubmit : MonoBehaviour
{
    public InputField userName;
    public Dropdown marker;
    public static string userId;

    DatabaseReference reference;
    string[] markerList = new string[4];

    public class User
    {
        public string userId;
        public string score;
        public string health;
        public string ammo;
        public string marker;

        public User()
        {
        }

        public User(string userId, string score, string health, string ammo, string marker)
        {
            this.userId = userId;
            this.score = score;
            this.health = health;
            this.ammo = ammo;
            this.marker = marker;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        markerList[0] = "marker1";
        markerList[1] = "marker2";
        markerList[2] = "marker3";
        markerList[3] = "marker4";
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-ar-2203a.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void sendToDatabase()
    {
        userId = userName.text;
        string marks = markerList[marker.value];
        User user = new User(userId, "0", "5", "7", marks);
        string json = JsonUtility.ToJson(user);
        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
        print("Inserted");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public string returnUserId()
    {
        return userId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
