using CollectionCardGame.Gameplay;
using CollectionCardGame.UI;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootstrapEntryPoint : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private ProgressBarUI _progressBar;

    private Task[] tasks;

    private AsyncOperation asyncOperation;
    private int fieldForCalculation;

    public Transform _transform;
    private void Start()
    {
        tasks = new Task[]
        {
            SetImage(),
            LoadResources("Prefab/Cards/Card"),
            LoadScene("MainMenuScene"),
            DoSomething(1000),
            DoSomething(4000),
            DoSomething(3000),
            DoSomething(10000),
            Task.Run(() => {fieldForCalculation = Calculation(12); })
        };

        SwitchScene();
    }
    private async Task SetImage()
    {
        Texture2D texture = await GetImageByURL("https://steamuserimages-a.akamaihd.net/ugc/1536248156646902788/A143219882B4E00503B1F574721430B292633A8D/?imw=512&amp");
        _backgroundImage.material.mainTexture = texture;
        _backgroundImage.material = _backgroundImage.defaultMaterial;
        Debug.Log("Картинка установлена");
        ChangeProgressBarValue();
    }

    private async Task<Texture2D> GetImageByURL(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        var asyncOp = request.SendWebRequest();

        while (asyncOp.isDone == false) { await Task.Delay(1000 / 30); }
        Debug.Log($"Картинка загружена. Метод закончил отрабатывать в потоке{Thread.CurrentThread.ManagedThreadId}");
        return DownloadHandlerTexture.GetContent(request);
    
    }
    
    private async Task DoSomething(int time)    //Симуляция долгой и тяжелой работы
    {
        await Task.Delay(time);
        Debug.Log($"Метод DoSomething закончил отрабатывать в потоке{Thread.CurrentThread.ManagedThreadId}");
        ChangeProgressBarValue();
    }

    private async Task LoadScene(string sceneName)
    {
        await Task.Yield();
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Сцена загружена");
        ChangeProgressBarValue();
    }
        
    private async void SwitchScene()
    {
        await Task.WhenAll(tasks);
        await Task.Delay(1000);
        asyncOperation.allowSceneActivation = true;
    }

    private async Task LoadResources(string path)
    {
        await Task.Yield();
        ResourceRequest request = Resources.LoadAsync(path);
        Debug.Log("Ресурс загружен");
        ChangeProgressBarValue();
    }

    private void ChangeProgressBarValue()
    {
        int completedTasks = 0;
        foreach (var task in tasks)
        {
            if (task.IsCompleted) completedTasks++;
        }
        var result = (completedTasks + 1f) / tasks.Length;
        _progressBar.Value = result;
    }

    private int Calculation(int a)
    {
        a = a * a;
        Thread.Sleep(1100);     //Симуляция сложных и долгих вычислений
        Debug.Log($"Произведены вычисления в потоке {Thread.CurrentThread.ManagedThreadId} с результатом {a}");
        return a;
    }
}
