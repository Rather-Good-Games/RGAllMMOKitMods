using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//RatherGood MultiplayerARPG playe intro video mod
namespace MultiplayerARPG
{
    [RequireComponent(typeof(VideoPlayer))]
    public class UISceneLoadingWithIntroVideo : UISceneLoading
    {
        VideoPlayer loadScreenVideoPlayer;

        [SerializeField] bool videoIsPlaying = false;

        [SerializeField] GameObject introVideoRoot;

        [Tooltip("Can use Slider component instead of image slider")]
        [SerializeField] Slider loadingSlider;

        //CORE MOD: add "protected virtual" to LoadSceneRoutine
        protected override IEnumerator LoadSceneRoutine(string sceneName)
        {

            if ((sceneName == GameInstance.Singleton.HomeSceneName) && (GameInstance.Singleton.enableIntroVideo))
            {
                rootObject.gameObject.SetActive(false);
                introVideoRoot.gameObject.SetActive(true);

                loadScreenVideoPlayer.loopPointReached += EndVideo;

                videoIsPlaying = true;

                loadScreenVideoPlayer.Play();

                yield return new WaitForEndOfFrame();

                while (videoIsPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }

                loadScreenVideoPlayer.Stop();
                loadScreenVideoPlayer.loopPointReached -= EndVideo;

                introVideoRoot.gameObject.SetActive(false);

                //Loading will commence when video has finished or canceled.
                yield return LoadSceneRoutineRG(sceneName);

            }
            else
            {
                introVideoRoot.gameObject.SetActive(false);
                yield return LoadSceneRoutineRG(sceneName);
            }
        }

        protected virtual IEnumerator LoadSceneRoutineRG(string sceneName)
        {
            if (SceneManager.GetActiveScene().name.Equals(sceneName))
                yield break;
            if (rootObject != null)
                rootObject.SetActive(true);
            if (uiTextProgress != null)
                uiTextProgress.text = "0.00%";
            if (imageGage != null)
                imageGage.fillAmount = 0;
            if (loadingSlider != null)
                loadingSlider.value = 0;

            yield return null;
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!asyncOp.isDone)
            {
                if (uiTextProgress != null)
                    uiTextProgress.text = (asyncOp.progress * 100f).ToString("N2") + "%";
                if (imageGage != null)
                    imageGage.fillAmount = asyncOp.progress;
                if (loadingSlider != null)
                    loadingSlider.value = asyncOp.progress;
                yield return null;
            }
            yield return null;
            if (uiTextProgress != null)
                uiTextProgress.text = "100.00%";
            if (imageGage != null)
                imageGage.fillAmount = 1;
            if (loadingSlider != null)
                loadingSlider.value = 1;
            yield return new WaitForSecondsRealtime(0.25f);

            if (rootObject != null)
                rootObject.SetActive(false);
        }

        /// <summary>
        /// Will skip remaining video. Linked to skipIntro button if provided.
        /// </summary>
        public void SkipIntroButton()
        {
            videoIsPlaying = false;
        }

        //Can call to end early from a "SkipIntro" button or whatnot
        public void EndVideo(VideoPlayer vp)
        {
            if (loadScreenVideoPlayer == null)
                return;

            videoIsPlaying = false;
        }

        private void Start()
        {
            loadScreenVideoPlayer = GetComponent<VideoPlayer>();

            if (GameInstance.Singleton.loadIntroVideoSettingsFromPlayerPrefs)
            {

                if (PlayerPrefs.HasKey("PLAY_INTRO_VIDEO"))
                {
                    GameInstance.Singleton.enableIntroVideo = (PlayerPrefs.GetInt("PLAY_INTRO_VIDEO") == 1); //1 = on
                }
                else
                {
                    PlayerPrefs.SetInt("PLAY_INTRO_VIDEO", 1); //default on
                    GameInstance.Singleton.enableIntroVideo = true;
                    PlayerPrefs.Save();
                }

            }


        }


    }
}