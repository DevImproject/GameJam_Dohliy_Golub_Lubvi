using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text ScoreText;
    public Text HighscoreText;
    public GameObject FloatingText;
    public GameObject SlingshotBird;
    public GameObject StillBird;
    public GameObject LevelWon;
    public GameObject LevelLost;
    public Slingshot Slingshot;
    public GameObject NewHighscore;
    public int RemainingBirds = 9;
    public float BirdDestructionTime = 5f;
    public bool IsLevelCleared;
    public bool IsLevelCompleted;
    public bool ActiveTurn;
    public int Score;
    public AudioSource WoodDestruction;
    public AudioSource IceDestruction;
    public AudioSource PigDestroy;
    public AudioSource BirdDestroy;
    public AudioSource PigHit;
    public AudioSource LevelCleared;
    public AudioSource LevelFailed;
    public AudioSource LevelCompleted;

    //private int transformbirdxcord;
    //private int transformbirdycord;
    //private int transformbirdzcord;

    //void LoadBirdSettings()
    //{
    //    // Предполагаем, что ConfigLoader уже загружает настройки и доступен через Singleton или глобальный объект
    //    //var configLoader = FindObjectOfType<ConfigLoader>();
    //    //var configLoader = new ConfigLoader();
    //    var configLoader = gameObject.AddComponent<ConfigLoader>();
    //    configLoader.Start();

    //    if (configLoader != null && configLoader.settings != null)
    //    {
    //        var bsc = configLoader.settings.birdStartCoordinates;

    //        //DebugHelper.DebugObject(birdConfig.birdStartCoordinates);
    //        //DebugHelper.DebugObject(birdConfig.birdCount);
    //        DebugHelper.DebugObject(bsc);
         

    //        if (bsc != null)
    //        {
    //            //transformbirdxcord = birdConfig.xcord;
    //            //transformbirdycord = birdConfig.ycord;
    //            //transformbirdzcord = birdConfig.zcord;
    //        }
    //        else
    //        {
    //            Debug.LogError("Bird config not found in settings.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("ConfigLoader or settings not initialized.");
    //    }
    //}

    void Start()
    {
        //LoadBirdSettings();

        if (Instance == null)
        {
            Instance = this;
        }
        int level = SceneManager.GetActiveScene().buildIndex;
        HighscoreText.text = GetHighscore(level).ToString();
        SetNewBird();
    }

    void Update()
    {
        /*if (!IsLevelCleared && GameObject.FindGameObjectsWithTag("Pig").Length == 0)
        {
            IsLevelCleared = true;
            LevelCleared.Play();
            if (!ActiveTurn)
            {
                FinishLevel();
            }
        }*/
    }

    public void AddScore(int amount, Vector3 position, Color textColor)
    {
        if (IsLevelCompleted)
        {
            return;
        }

        int level = SceneManager.GetActiveScene().buildIndex;
        Score += amount;
        ScoreText.text = Score.ToString();
        GameObject floatingTextObj = Instantiate(FloatingText, position, Quaternion.identity);
        FloatingText floatingText = floatingTextObj.GetComponent<FloatingText>();
        floatingText.UpdateText(amount.ToString(), textColor);
    }

    public void SetNewBird()
    {
        ActiveTurn = false;
        //RemainingBirds--;
        if (RemainingBirds >= 0)
        {
            GameObject bird = Instantiate(SlingshotBird, new Vector3(Slingshot.transform.position.x - 0.08f, Slingshot.transform.position.y + 3.82f, Slingshot.transform.position.z - 0.29f), Quaternion.identity);
            bird.GetComponent<Bird>().DestructionTime = BirdDestructionTime;
            Slingshot.Bird = bird;
            Camera.main.GetComponent<MainCamera>().Bird = bird;

            foreach (StillBird stillBird in FindObjectsOfType<StillBird>())
            {
                Destroy(stillBird.gameObject);
            }

            if (RemainingBirds > 0)
            {
                for (int i = 0; i < RemainingBirds; i++)
                {
                    GameObject stillBird = Instantiate(StillBird, new Vector3(0, 0, 0), Quaternion.identity);
                    stillBird.transform.Find("Bird Body").transform.position = new Vector3(-2.5f * (i + 1), 0, -3.19f);
                    if (i % 2 == 0)
                    {
                        stillBird.GetComponent<StillBird>().WaitForSeconds = 0.45f;
                    }
                }
            }
        }

        //FinishLevel();
    }

    private void FinishLevel()
    {
        if (IsLevelCleared)
        {
            if (RemainingBirds >= 0)
            {
                StartCoroutine(AddFinalScores());
            }
            else
            {
                EndLevel(true);
            }
        }
        else if (RemainingBirds < 0)
        {
            if (FindObjectsOfType<Pig>().All(p => p.GetComponent<Rigidbody>().velocity.magnitude < 0.1f))
            {
                EndLevel(false);
            }
            else
            {
                StartCoroutine(CheckIfPigsStoppedMoving());
            }
        }
    }

    IEnumerator CheckIfPigsStoppedMoving()
    {
        yield return new WaitForSeconds(0.25f);

        FinishLevel();
    }

    IEnumerator AddFinalScores()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (StillBird stillBird in FindObjectsOfType<StillBird>())
        {
            AddScore(10000, stillBird.transform.Find("Bird Body").transform.position, Color.red);
        }
        foreach (Bird bird in FindObjectsOfType<Bird>())
        {
            AddScore(10000, bird.transform.position, Color.red);
        }

        yield return new WaitForSeconds(1);

        EndLevel(true);
    }

    private void EndLevel(bool wonLevel)
    {
        if (wonLevel)
        {
            int level = SceneManager.GetActiveScene().buildIndex;
            LevelCompleted.Play();
            IsLevelCompleted = true;

            int highscore = GetHighscore(level);
            int score = Score;
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt($"{level}-highscore", highscore);
                PlayerPrefs.Save();
                NewHighscore.SetActive(true);
            }

            LevelWon.transform.Find("Level Text").GetComponent<Text>().text = $"1-{level + 1}";
            LevelWon.transform.Find("Score Amount Text").GetComponent<Text>().text = score.ToString();
            HighscoreText.text = highscore.ToString();
            LevelWon.transform.Find("Highscore Amount Text").GetComponent<Text>().text = highscore.ToString();
            LevelWon.SetActive(true);
        }
        else
        {
            LevelFailed.Play();
            LevelLost.SetActive(true);
        }
    }

    private int GetHighscore(int level)
    {
        return PlayerPrefs.HasKey($"{level}-highscore") ? PlayerPrefs.GetInt($"{level}-highscore") : 0;
    }
}
//[System.Serializable]
//public class GameSettings
//{
//    public BirdStartCoordinatesSettings birdStartCoordinates;
//    public BirdCountSettings birdCount;
   
//    [System.Serializable]
//    public class BirdStartCoordinatesSettings
//    {
//        public float xcord;
//        public float ycord;
//        public float zcord;
//    }

//    [System.Serializable]
//    public class BirdCountSettings
//    {
//        public int count;
//    }
//}

//public class ConfigLoader : MonoBehaviour
//{
//    public GameSettings settings;

//    public void Start()
//    {
//        LoadConfig("config.json");
//    }

//    void LoadConfig(string fileName)
//    {
//        var filePath = Path.Combine(Application.streamingAssetsPath, fileName);

//        if (File.Exists(filePath))
//        {
//            string json = File.ReadAllText(filePath);
//            //Debug.Log($"Loaded JSON: {json}");

//            try
//            {
//                settings = JsonUtility.FromJson<GameSettings>(json);
//                //DebugHelper.DebugObject(settings);
//                //Debug.Log("Config loaded successfully!");
//            }
//            catch (System.Exception ex)
//            {
//                Debug.LogError($"Failed to parse JSON: {ex.Message}");
//            }
//        }
//        else
//        {
//            Debug.LogError($"Config file not found: {filePath}");
//        }
//    }
//}

//public static class DebugHelper
//{
//    public static void DebugObject(object obj, string objectName = "Object")
//    {
//        if (obj == null)
//        {
//            Debug.LogWarning($"{objectName} is null.");
//            return;
//        }

//        Debug.Log($"--- Debugging {objectName} ---");

//        // Получение всех полей объекта
//        var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
//        foreach (var field in fields)
//        {
//            Debug.Log($"{field.Name}: {field.GetValue(obj)}");
//        }

//        // Получение всех свойств объекта
//        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
//        foreach (var property in properties)
//        {
//            if (property.CanRead)
//            {
//                Debug.Log($"{property.Name}: {property.GetValue(obj)}");
//            }
//        }

//        Debug.Log($"--- End of {objectName} ---");
//    }
//}