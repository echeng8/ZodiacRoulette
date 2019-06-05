using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

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

	public Canvas Aqua;
	public Canvas Pisc;
	public Canvas Arie;
	public Canvas Taur;
	public Canvas Gemi;
	public Canvas Canc;
	public Canvas Leo;
	public Canvas Virg;
	public Canvas Libr;
	public Canvas Scor;
	public Canvas Sagi;
	public Canvas Capr;

    [SerializeField] GameObject textParticles;
    [SerializeField] GameObject environmentPlexus;
    [SerializeField] CanvasGroup questionCanvas;
    [SerializeField] Animator rouletteAnim;

    bool introFlag = true;
    
	private new void Awake()
    {
        base.Awake();
        currentState = GameState.Asking;  
    }


    public void activateSign(Sign s)
    {
        //placeholder
        Debug.Log("LANDED ON: " + s);
        switchState(GameState.Displaying); 

		switch (s)
		{
			case Sign.Aquarius:
				{
					if(Aqua!= null)
						Aqua.enabled = true;
					break;
				}
			case Sign.Aries:
				{
					if(Arie!= null)
						Arie.enabled = true;
					break;
				}
			case Sign.Cancer:
				{
					if (Canc != null)
						Canc.enabled = true;
					break;
				}
			case Sign.Capricorn:
				{
					if (Capr != null)
						Capr.enabled = true;
					break;
				}
			case Sign.Gemini:
				{
					if (Gemi != null)
						Gemi.enabled = true;
					break;
				}
			case Sign.Leo:
				{
					if (Leo != null)
						Leo.enabled = true;
					break;
				}
			case Sign.Libra:
				{
					if (Libr != null)
						Libr.enabled = true;
					break;
				}
			case Sign.Pisces:
				{
					if (Pisc != null)
						Pisc.enabled = true;
					break;
				}
			case Sign.Sagittarius:
				{
					if (Sagi != null)
						Sagi.enabled = true;
					break;
				}
			case Sign.Scorpio:
				{
					if (Scor != null)
						Scor.enabled = true;
					break;
				}
			case Sign.Taurus:
				{
					if (Taur != null)
						Taur.enabled = true;
					break;
				}
			case Sign.Virgo:
				{
					if (Virg != null)
						Virg.enabled = true;
					break;
				}
		}

	} 

    public void switchState(GameState state)
    {
        switch (state)
        {
            case GameState.Asking:
                if (currentState != state)  //restarting
                {
                    OnGameRestart.Invoke();
                    var go = FindObjectsOfType<MonoBehaviour>().OfType<IWillReset>();
                    foreach (IWillReset r in go)
                    {
                        r.resetGameObject();
                        textParticles.SetActive(false);
                    }
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
                //todo remove need for scene reloading and make this a variable reference
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

        while (questionCanvas.alpha > 0) //alpha is not 1
        {
            questionCanvas.alpha -= Time.deltaTime / 8;
            yield return null; // run this on the next opportunity of the next frame
        }
        textParticles.GetComponent<ParticleSystem>().Stop();
        yield return Fader.instance.FadeOut(3);
        yield return new WaitForSeconds(1);
        environmentPlexus.SetActive(false);
        textParticles.gameObject.SetActive(false);
        yield return Fader.instance.FadeIn(2);
        rouletteAnim.Play("DropRoulette");
        yield return new WaitForSeconds(4);
        canThrow = true;
    }
}
