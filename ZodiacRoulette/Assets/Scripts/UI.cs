using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
	private string m_text;

    public void Quit()
	{
		print("Quit!\n");
		Application.Quit();
	}
	public void UserInput(string text)
	{
		m_text = text;		print(m_text);
	}
}
