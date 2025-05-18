using System.Collections;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class OpeningDirector : MonoSingleton<OpeningDirector>
    {
        public Progressable LighthouseOutside;
        public Progressable Logo;

        public Vector3 TargetOffset;
        public float TargetZoom;
        public float CameraDuration;
        public float CameraDelay;
        public float LighthouseOutsideDuration;
        public float LighthouseOutsideDelay;
        public float LogoDuration;
        public float LogoDelay;

        public void Climb()
        {
            StartCoroutine(ClimbCoroutine());
        }

        private IEnumerator ClimbCoroutine()
        {
            yield return new WaitForSeconds(1f);
            float counter = 0f;
            while (counter < 100f)
            {
                counter += Time.deltaTime;
                CameraStack.Instance.Offset += new Vector3(0, 0.3f * Time.deltaTime);
                yield return null;
            }
        }
        
        public void Play()
        {
            StartCoroutine(PlayCoroutine());
        }
        private IEnumerator PlayCoroutine()
        {
            GameManager.Instance.moveAction.Disable();

            Audio.AudioManager.PlayMusic(GameDesignData.GetAudioData("opening", out var audioData) ? audioData.clip : null);
            TargetOffset.x = - PlayerController.Instance.transform.position.x;

            var cameraCoroutine = CameraStack.Instance.DampToSize(TargetZoom, CameraDuration, CameraDelay);
            LighthouseOutside.LinearTransition(LighthouseOutsideDuration, LighthouseOutsideDelay);
            while (Vector3.Distance(CameraStack.Instance.Offset, TargetOffset) > 0.01f)
            {
                CameraStack.Instance.Offset = Vector3.MoveTowards(CameraStack.Instance.Offset, TargetOffset, 0.01f);
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            TargetOffset.y = -8f;
            while (Vector3.Distance(CameraStack.Instance.Offset, TargetOffset) > 0.01f)
            {
                CameraStack.Instance.Offset = Vector3.MoveTowards(CameraStack.Instance.Offset, TargetOffset, 0.01f);
                yield return null;
            }
            
            yield return cameraCoroutine;

            CameraStack.Instance.DampToSize(5f, 2f, 0f);
            LighthouseOutside.InverseLinearTransition(1f, 0f);
            while (Vector3.Distance(CameraStack.Instance.Offset, Vector3.zero) > 0.01f)
            {
                CameraStack.Instance.Offset = Vector3.Lerp(CameraStack.Instance.Offset, Vector3.zero, Time.deltaTime);
                yield return null;
            }

            GameManager.Instance.moveAction.Enable();
        }
    }

}