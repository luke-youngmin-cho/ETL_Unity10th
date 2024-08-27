using Demo.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Test_Async : MonoBehaviour
{
    public void DoSum()
    {
        StartCoroutine(C_Sum5Times(5,
                                   result => UIManager.instance.Get<UI_AlertWindow>()
                                                               .Show(result.ToString())));
    }

    // async void : 이 함수는 비동기이지만 외부에서 이 함수를 기다리는 로직은 작성할 수 없음.
    // async Task : 이 함수는 비동기이며 외부에서 이 함수를 기다리는 로직을 작성할 수 있음. 
    // async Task<T> : 이 함수는 비동기이며 외부에서 이 함수를 기다리는 로직 및 이 함수의 반환값을 받을 수 있음.
    public async void DoSumAsync()
    {
        int result = await Sum5TimesAsync(5);
        UpdateDispatcher.instance.Enqueue(() =>
        {
            UIManager.instance.Get<UI_AlertWindow>()
                          .Show(result.ToString());
        });
    }

    // 코루틴에서 결과를 반환받아 처리하는 일반적인 방법
    IEnumerator C_Sum5Times(int factor, Action<int> complete)
    {
        int result = 0;

        for (int i = 0; i < 5; i++)
        {
            result += factor;
            UIManager.instance.Get<UI_AlertWindow>()
                              .Show(result.ToString());
            yield return new WaitForSeconds(0.1f);
        }

        complete?.Invoke(result);
        yield return null;
    }

    int Sum5Times(int factor)
    {
        int result = 0;

        for (int i = 0; i < 5; i++)
        {
            result += factor;
        }

        return result;
    }

    async Task<int> Sum5TimesAsync(int factor)
    {
        int result = 0;

        for(int i = 0; i < 5; i++)
        {
            result += factor;
            UIManager.instance.Get<UI_AlertWindow>()
                              .Show(result.ToString());
            await Task.Delay(100);
        }

        return result;
    }

    async UniTask<int> Sum5TimesUniAsync(int factor)
    {
        int result = 0;

        for (int i = 0; i < 5; i++)
        {
            result += factor;
            UIManager.instance.Get<UI_AlertWindow>()
                              .Show(result.ToString());
            await UniTask.Delay(100);
        }

        await UniTask.Yield();
        await UniTask.WaitForSeconds(10);
        await UniTask.WaitUntil(() => true);

        CancellationTokenSource cts = new CancellationTokenSource();
        await
        UniTask.Create(async (ctsToken) =>
        {
            await UniTask.Yield();
            UIManager.instance.Get<UI_AlertWindow>()
                              .Show("UniTask Created");
        }, cancellationToken: cts.Token);

        return result;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 40), "Do Sum with Coroutine"))
        {
            DoSum();
        }
        if (GUI.Button(new Rect(0, 40, 200, 40), "Do Sum with async-await"))
        {
            DoSumAsync();
        }
    }
}
