using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Sign {Aquarius, Pisces, Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn}

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// How many seconds must be the ball remain in the sign triggers for them to activate? 
    /// </summary>
    public int signActivationSeconds; 
    public bool signActivated = false;
    public UnityEvent gameReset;

	public Canvas Aqua;
	public Canvas Pisc;
	public Canvas Arie;
	public Canvas Taur;
	public Canvas Gemi;
	public Canvas Canc;
	public Canvas Leo_;
	public Canvas Virg;
	public Canvas Libr;
	public Canvas Scor;
	public Canvas Sagi;
	public Canvas Capr;

	private new void Awake()
    {
        base.Awake();  
    }

    public void resetGame()
    {
        signActivated = false;
        gameReset.Invoke();  
    }

    public void activateSign(Sign s)
    {
        //placeholder
        Debug.Log("LANDED ON: " + s);
        signActivated = true;

		switch (s) //I feel like this could be done in an
				//array but idk how arrays work in C#
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
					if (Leo_ != null)
						Leo_.enabled = true;
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
				//default:
				//print("Error GameManager.cs line 85");
		}

	} 

}
