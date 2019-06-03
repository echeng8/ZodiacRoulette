using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public enum Sign {Aquarius, Pisces, Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn}

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// How many seconds must be the ball remain in the sign triggers for them to activate? 
    /// </summary>
    public int signActivationSeconds; 
    public bool signActivated = false;
    public UnityEvent gameReset;  

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

		switch (s)
		{
			case Sign.Aquarius:
				{
					break;
				}
			case Sign.Aries:
				{
					break;
				}
			case Sign.Cancer:
				{
					break;
				}
			case Sign.Capricorn:
				{
					break;
				}
			case Sign.Gemini:
				{
					break;
				}
			case Sign.Leo:
				{
					break;
				}
			case Sign.Libra:
				{
					break;
				}
			case Sign.Pisces:
				{
					break;
				}
			case Sign.Sagittarius:
				{
					break;
				}
			case Sign.Scorpio:
				{
					break;
				}
			case Sign.Taurus:
				{
					break;
				}
			case Sign.Virgo:
				{
					break;
				}
			//default:
				//print("Error GameManager.cs line 85");
		}
				
    } 

}
