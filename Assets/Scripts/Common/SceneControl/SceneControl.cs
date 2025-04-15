using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using Kuchinashi;
using TMPro;
using UnityEngine.Events;
using Kuchinashi.Utils.Progressable;
using System.Runtime.InteropServices;

# if UNITY_EDITOR

using UnityEditor;

# endif

namespace Common.SceneControl
{
    public class SceneControl : MonoSingleton<SceneControl>
    {
        public static string CurrentScene;
        public static bool IsLoading;
        public static bool CanTransition;
        public static string PreviousScene;

        [Header("References")]
        public CanvasGroupAlphaProgressable CanvasGroup;
        public CanvasGroupAlphaProgressable LoadingPanelCG;
        public CanvasGroupAlphaProgressable BlackLayerCG;

        public TMP_Text TipsText;
        public Slider LoadingProgress;

        private static Coroutine mCurrentCoroutine;
        private static UnityAction mOnSceneLoaded;

        private AsyncOperation mAsyncOperation;

        public string ToSwitchSceneName { get; set; }

# if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        IntPtr currentWindow;

# endif

        void Start()
        {
            StartCoroutine(InitializeSceneControl());

# if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

            currentWindow = GetForegroundWindow();

# endif

        }

        private IEnumerator InitializeSceneControl()
        {
            mAsyncOperation = SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Additive);
            yield return mAsyncOperation;
            // SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("ControlScene"));
        }

        public static void SetActive(string targetSceneName)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
        }

        public static void SwitchScene(string targetSceneName)
        {
            if (IsLoading || mCurrentCoroutine != null || targetSceneName == CurrentScene) return;
            IsLoading = true;

            CanTransition = false;
            mCurrentCoroutine = Instance.StartCoroutine(Instance.SwitchSceneEnumerator(targetSceneName));
        }

        public static void SwitchSceneWithEvent(string targetSceneName, Action action, float delay = 0f)
        {
            if (IsLoading || mCurrentCoroutine != null || targetSceneName == CurrentScene) return;
            IsLoading = true;

            CanTransition = false;
            mCurrentCoroutine = Instance.StartCoroutine(Instance.SwitchSceneWithEventEnumerator(targetSceneName, action, delay: delay));
        }

        public static void SwitchSceneWithoutConfirm(string targetSceneName, float delay = 0f)
        {
            if (IsLoading || mCurrentCoroutine != null || targetSceneName == CurrentScene
                || SceneManager.GetSceneByName(targetSceneName) == null) return;
            IsLoading = true;

            mCurrentCoroutine = Instance.StartCoroutine(Instance.SwitchSceneWithoutConfirmEnumerator(targetSceneName, delay));
        }

        public static bool TrySwitchSceneWithoutConfirm(string targetSceneName, float delay = 0f)
        {
            if (IsLoading || mCurrentCoroutine != null || targetSceneName == CurrentScene
                || SceneManager.GetSceneByName(targetSceneName) == null) return false;

            IsLoading = true;
            mCurrentCoroutine = Instance.StartCoroutine(Instance.SwitchSceneWithoutConfirmEnumerator(targetSceneName, delay));
            return true;
        }

        public static void LoadScene(string targetSceneName)
        {
            if (IsLoading || mCurrentCoroutine != null) return;
            IsLoading = true;
            
            CanTransition = false;
            mCurrentCoroutine = Instance.StartCoroutine(Instance.LoadSceneEnumerator(targetSceneName));
        }

        public static void LoadSceneWithoutConfirm(string targetSceneName)
        {
            if (IsLoading || mCurrentCoroutine != null) return;
            IsLoading = true;

            CanTransition = true;
            mCurrentCoroutine = Instance.StartCoroutine(Instance.LoadSceneEnumerator(targetSceneName));
        }

        public static void UnloadCurrentScene()
        {
            if (IsLoading || mCurrentCoroutine != null) return;
            IsLoading = true;
            
            mCurrentCoroutine = Instance.StartCoroutine(Instance.UnloadCurrentSceneEnumerator());
        }

        IEnumerator UnloadCurrentSceneEnumerator()
        {
            yield return Fade(1);

            yield return SceneManager.UnloadSceneAsync(CurrentScene);
            yield return new WaitForSeconds(0.5f);

            yield return Fade(0);
            mCurrentCoroutine = null;
        }

        IEnumerator LoadSceneEnumerator(string sceneName)
        {
            yield return Fade(1);

            mAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return mAsyncOperation;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            yield return new WaitUntil(() => {return CanTransition;});

            yield return Fade(0);
            mCurrentCoroutine = null;
        }

        IEnumerator SwitchSceneEnumerator(string sceneName, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            PreviousScene = CurrentScene;
            
            LoadingProgress.value = 0;
            yield return Fade(1);

            mAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            CurrentScene = sceneName;
            
            while (mAsyncOperation.progress < 0.9f)
            {
                LoadingProgress.value = mAsyncOperation.progress;
                yield return null;
            }
            LoadingProgress.value = 1;
            mAsyncOperation.allowSceneActivation = true;

            yield return mAsyncOperation;
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            yield return new WaitUntil(() => {return CanTransition;});

            yield return Fade(0);
            
            mCurrentCoroutine = null;
            mAsyncOperation = null;
        }

        IEnumerator SwitchSceneWithEventEnumerator(string sceneName, Action action, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            PreviousScene = CurrentScene;

            LoadingProgress.value = 0;
            yield return Fade(1);
            
            mAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            CurrentScene = sceneName;
            
            mAsyncOperation.allowSceneActivation = true;

            while (mAsyncOperation.progress < 0.9f)
            {
                LoadingProgress.value = mAsyncOperation.progress;
                yield return null;
            }
            LoadingProgress.value = 1;

            yield return mAsyncOperation;
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

            action();

            yield return new WaitUntil(() => {return CanTransition;});

            yield return Fade(0);

            mCurrentCoroutine = null;
            mAsyncOperation = null;
        }

        IEnumerator SwitchSceneWithoutConfirmEnumerator(string sceneName, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            PreviousScene = CurrentScene;

            LoadingProgress.value = 0;
            yield return Fade(1);

            mAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            CurrentScene = sceneName;
            
            while (mAsyncOperation.progress <= 0.9f)
            {
                LoadingProgress.value = Mathf.Lerp(LoadingProgress.value, Mathf.Min(mAsyncOperation.progress + 0.5f, 0.9f), 0.05f);
                yield return new WaitForFixedUpdate();

                if (mAsyncOperation.progress == 0.9f)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        LoadingProgress.value = Mathf.Lerp(LoadingProgress.value, 1f, 0.05f);
                        yield return new WaitForFixedUpdate();
                    }
                }
            }
            LoadingProgress.value = 1;
            mAsyncOperation.allowSceneActivation = true;

            yield return mAsyncOperation;
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

            yield return Fade(0);
            mCurrentCoroutine = null;
            mAsyncOperation = null;
        }
        
        private IEnumerator Fade(float targetAlpha)
        {
            IsLoading = targetAlpha == 1;

            if (targetAlpha == 1)
            {
                BlackLayerCG.Progress = 1;
                LoadingPanelCG.Progress = 0;

                TypeEventSystem.Global.Send<OnSceneControlActivatedEvent>();

                yield return CanvasGroup.LinearTransition(0.2f, 0f);
                LoadingPanelCG.Progress = 1;
                yield return BlackLayerCG.InverseLinearTransition(0.2f, 0f);

                yield return new WaitForSeconds(0.5f);
            }
            else if (targetAlpha == 0)
            {
                TypeEventSystem.Global.Send<OnSceneControlDeactivatedEvent>();

                yield return BlackLayerCG.LinearTransition(0.2f, 0f);
                LoadingPanelCG.Progress = 0;
                yield return CanvasGroup.InverseLinearTransition(0.2f, 0f);
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(SceneControl))]
    [CanEditMultipleObjects]
    public class SceneControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SceneControl manager = (SceneControl)target;

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Control", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            manager.ToSwitchSceneName = EditorGUILayout.TextField(manager.ToSwitchSceneName);
            if (GUILayout.Button("Switch"))
            {
                SceneControl.SwitchSceneWithoutConfirm(manager.ToSwitchSceneName);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

#endif

}
