using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public TextMesh health;
    public TextMesh ammo;
    public TextMesh score;

    string nameId;
    DatabaseReference reference;

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
        health.text = "5";
        ammo.text = "7";
        score.text = "0";
        nameId = gameObject.GetComponent<ButtonSubmit>().returnUserId();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-ar-2203a.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        checkForChanges();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void checkForChanges()
    {
       var temp = FirebaseDatabase.DefaultInstance
      .GetReference("users");

      temp.ChildChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        string json = args.Snapshot.GetRawJsonValue();
        User instance = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
        if (instance.userId == nameId)
        {
            if (health.text != instance.health)
                health.text = instance.health;
            if (ammo.text != instance.ammo)
                ammo.text = instance.ammo;
            if (score.text != instance.score)
                score.text = instance.score;
            return;
        }
    }
}