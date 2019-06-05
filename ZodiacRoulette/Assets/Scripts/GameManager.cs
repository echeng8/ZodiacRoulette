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
                    }
                }
                GameObject.FindGameObjectWithTag("Main Canvas").transform.Find("Menus").ToggleChildren(0);
                break;
            case GameState.Throwing:
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
}
