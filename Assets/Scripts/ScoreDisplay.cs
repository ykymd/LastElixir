using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreDisplay : MonoBehaviour {
	
        public Text scoreText1; //Text用変数
        public Text scoreText2; //Text用変数
        public Text scoreText3; //Text用変数
        public Text scoreText4; //Text用変数
        private int score = 0; //スコア計算用変数
        public GameObject Title;
        public GameObject Retry;

        private int count = 0;
	
        void Start (){
        }
	
        void Update (){

            if(count==0){
                scoreText4.text = "4P 000000pt"; //初期スコアを代入して画面に表示、000000ptのところを受け取った値に
            }else if(count==20){
                scoreText3.text = "3P 000000pt"; //初期スコアを代入して画面に表示、000000ptのところを受け取った値に
            }else if(count==40){
                scoreText2.text = "2P 000000pt"; //初期スコアを代入して画面に表示、000000ptのところを受け取った値に
            }else if(count==60){
                scoreText1.text = "1P 000000pt"; //初期スコアを代入して画面に表示、000000ptのところを受け取った値に
            }else if(count==80){
                //ここで二つのボタンをイネーブル
                Title.SetActiveRecursively(true);
                Retry.SetActiveRecursively(true);
            }

            count++;
		
        }
    }
}