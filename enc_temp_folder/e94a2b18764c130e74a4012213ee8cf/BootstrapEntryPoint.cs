using System;
using System.Threading;
using System.Threading.Tasks;
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

    private void Start()
    {
        tasks = new Task[]
        {
            LoadScene("MainMenuScene"),
            SetImage(),
            DoSomething(1000),
            DoSomething(4000),
            DoSomething(3000),
            DoSomething(10000)
        };
        //ChangeProgressBarValue();

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

        while (asyncOp.isDone == false) { await Task.Delay(1000 / 30);}
        if (request.isNetworkError || request.isHttpError)
        { Debug.Log(request.error); return null; }
        else
        {
            Debug.Log($"Картинка загружена. Метод закончил отрабатывать в потоке{Thread.CurrentThread.ManagedThreadId}");
            return DownloadHandlerTexture.GetContent(request);
        }
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

    private void ChangeProgressBarValue()
    {
        int completedTasks = 0;
        foreach (var task in tasks)
        {
            if (task.IsCompleted) completedTasks++;
        }
        var result = completedTasks + 1f / tasks.Length;
        _progressBar.Value = result;
        Debug.Log(result);
    }
}
