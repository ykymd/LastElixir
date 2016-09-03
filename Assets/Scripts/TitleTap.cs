using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class TitleTap : MonoBehaviour {

	 void Update()
    {
        if (MultiTouch.GetTouch() == TouchInfo.Began)//ワンクリックまたはタッチした場合
        {
			SceneManager.LoadScene ("Main");
		}
	}
	
}