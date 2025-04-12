using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using QFramework;


#if UNITY_EDITOR

using UnityEditor;

# endif

namespace Phosphorescence.Narration
{
    public class InkReader : MonoBehaviour
    {
        private Story _currentStory;
        public TextAsset ToInitializeStory { get; set; }

        private void Awake()
        {
            TypeEventSystem.Global.Register<RequestNewLineEvent>(e => Continue()).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<SelectOptionEvent>(e => {
                if (_currentStory == null) return;

                _currentStory.ChooseChoiceIndex(e.index);
                Continue();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public InkReader Initialize(TextAsset rawStory)
        {
            if (rawStory == null) return this;

            _currentStory = new Story(rawStory.text);
            return this;
        }

        public void Continue()
        {
            if (_currentStory == null) return;

            if (_currentStory.canContinue)
            {
                _currentStory.Continue();

                var content = _currentStory.currentText;
                var tags = ParseTags(_currentStory.currentTags);

                TypeEventSystem.Global.Send(new OnLineReadEvent
                {
                    content = content,
                    tags = tags
                });
            }
            else if (_currentStory.currentChoices.Count > 0)
            {
                var linesEvent = new OnLinesReadEvent();
                linesEvent.lines = new();
                linesEvent.tags = ParseTags(_currentStory.currentTags);

                foreach (var choice in _currentStory.currentChoices)
                {
                    linesEvent.lines.Add(new OnLineReadEvent
                    {
                        content = choice.text,
                        tags = ParseTags(choice.tags)
                    });
                }

                TypeEventSystem.Global.Send(linesEvent);
            }
            else  // Story End
            {
                TypeEventSystem.Global.Send(new OnStoryEndEvent());
                _currentStory = null;
            }
        }

        private Dictionary<string, string> ParseTags(List<string> rawTags)
        {
            var tags = new Dictionary<string, string>();
            if (rawTags == null) return tags;

            foreach (var rawTag in rawTags)
            {
                var splitTag = rawTag.Split(':');
                if (splitTag.Length != 2)
                {
                    Debug.LogError($"Tag could not be parsed: {rawTag}");
                    continue;
                }

                var key = splitTag[0].Trim().ToLower();
                var value = splitTag[1].Trim();  // Tag value is case sensitive

                tags.Add(key, value);
            }

            return tags;
        }
    }

    public struct OnLineReadEvent {
        public string content;
        public Dictionary<string, string> tags;
    }

    public struct OnLinesReadEvent {
        public Dictionary<string, string> tags;
        public List<OnLineReadEvent> lines;
    }

    public struct OnStoryEndEvent {}

    public struct RequestNewLineEvent {}

    public struct SelectOptionEvent {
        public int index;
    }

    
#if UNITY_EDITOR

    [CustomEditor(typeof(InkReader))]
    [CanEditMultipleObjects]
    public class SceneControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            InkReader manager = (InkReader)target;

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Control", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            manager.ToInitializeStory = (TextAsset)EditorGUILayout.ObjectField("Story", manager.ToInitializeStory, typeof(TextAsset), false);
            if (GUILayout.Button("Initialize"))
            {
                manager.Initialize(manager.ToInitializeStory).Continue();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

#endif

}