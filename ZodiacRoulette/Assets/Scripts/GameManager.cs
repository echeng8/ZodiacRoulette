using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using System; 

public enum Sign {Aquarius, Pisces, Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn}
public enum GameState {Asking, Throwing, Displaying} 

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// How many seconds must be the ball remain in the sign triggers for them to activate? 
    /// </summary>
    public int signActivationSeconds;
    public bool canThrow;
    public GameState currentState;
    public UnityEvent OnGameRestart;
    public Sign activatedSign;
    public Dictionary<string, string> fortuneText;

    [SerializeField] GameObject textParticles;
    [SerializeField] GameObject environmentPlexus;
    [SerializeField] CanvasGroup questionCanvas;
    [SerializeField] Animator rouletteAnim;
    [SerializeField] Material ballMatLit;
    [SerializeField] Material ballMatDark;
    [SerializeField] GameObject restartUI;
    [SerializeField] GameObject roulette;
    Vector3 roulettePos;

    bool introFlag = true;

    
	private new void Awake()
    {
        
        base.Awake();
        currentState = GameState.Asking;
        LoadFortunes();
    }

    private void Start()
    {
        roulettePos = roulette.transform.position;
    }


    public void activateSign(Sign s)
    {
        activatedSign = s; 
        switchState(GameState.Displaying);
        restartUI.SetActive(true);
    }

    public void switchState(GameState state)
    {
        switch (state)
        {
            case GameState.Asking:
                if (currentState != state)  //restarting
                {
                    StopAllCoroutines();
                    OnGameRestart.Invoke();
                    var go = FindObjectsOfType<MonoBehaviour>().OfType<IWillReset>();
                    foreach (IWillReset r in go)
                    {
                        r.resetGameObject();
                        textParticles.SetActive(false);
                    }
                    environmentPlexus.SetActive(true);
                    rouletteAnim.Play("Empty");
                    roulette.transform.position = roulettePos;
                    GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                    ball.GetComponent<MeshRenderer>().material = ballMatDark;
                    ball.GetComponent<Throwable>().grabbable = false;
                    ball.GetComponentInChildren<ParticleSystem>().Stop();
                    introFlag = true;
                    restartUI.SetActive(false);
                }
                GameObject.FindGameObjectWithTag("Main Canvas").transform.Find("Menus").ToggleChildren(0);
                break;
            case GameState.Throwing:
                if (introFlag)
                {
                    StartCoroutine("IntroThrowSequence");
                    introFlag = false;
                }
                else
                    canThrow = true;
                break;
            case GameState.Displaying:
                GameObject.FindGameObjectWithTag("Main Canvas").transform.Find("Menus").ToggleChildren(2); 
                break; 
        }
        currentState = state;
    }

    /// <summary>
    /// Sets the state by index (0 - Asking, 1 - Throwing, 2 - Displaying) 
    /// </summary>
    /// <param name="state"></param>
    public void switchState(int i)
    {
        switchState((GameState)i); 
    }

    IEnumerator IntroThrowSequence()
    {
        textParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        bool ballParticleFlag = false;
        bool trailPlexusFlag = false;
        ParticleSystem ballParticle = null;
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        while (questionCanvas.alpha > 0) //alpha is not 1
        {
            questionCanvas.alpha -= Time.deltaTime / 5;
            if(questionCanvas.alpha < .75f && !ballParticleFlag)
            {
                ballParticle = ball.GetComponentInChildren<ParticleSystem>();
                ballParticle.Play();
                ballParticleFlag = true;
            }
            if(questionCanvas.alpha < .25f && !trailPlexusFlag)
            {
                textParticles.GetComponent<ParticleSystem>().Stop();
                trailPlexusFlag = true;
            }
            yield return null; // run this on the next opportunity of the next frame
        }

        ballParticle.Stop();
        yield return Fader.instance.FadeOut(2);
        yield return new WaitForSeconds(1);
        ball.GetComponent<MeshRenderer>().material = ballMatLit;
        environmentPlexus.SetActive(false);
        textParticles.gameObject.SetActive(false);
        yield return Fader.instance.FadeIn(2);
        rouletteAnim.Play("DropRoulette");
    }

    /// <summary>
    /// Initializes the fortuneText dictionary from entries in FortuneText.txt in the resources folder. 
    /// </summary>
    void LoadFortunes()
    {
        fortuneText = new Dictionary<string, string>(); 
        TextAsset rawText = Resources.Load("FortuneText") as TextAsset;

        foreach (string entry in rawText.text.Split(new string[] { "\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries))
        {
            entry.Split(new string[] { "\r\n" }, StringSplitOptions.None).Destructure(out string sign, out string text);
            fortuneText.Add(sign.TrimEnd(new[] {':',' '}), text);
        }
    }
}
