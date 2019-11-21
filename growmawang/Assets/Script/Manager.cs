using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]GameObject gameOver;
    [SerializeField] Text Grade;
    [SerializeField] Text FinalGrade;
    [SerializeField] Slider Slider;
    public static Manager manager;

	[SerializeField] Character Player;
	[SerializeField] public Monster Mob_L;
	[SerializeField] public Monster Mob_R;
	[SerializeField] Monster Mob;
    [SerializeField] Image HavestImage;
    [SerializeField] Text HavestText;
    [SerializeField] Image MoveImage;
    [SerializeField] Text MoveText;

    [SerializeField] float RemainTime = 5.0f;
	[SerializeField] float ReduseRate = 0.3f;
    [SerializeField] int Point = 0;
    [SerializeField] int Mob2Velue = 2;
    int twoTouch = -1;
    bool changeMob = true;

    private void Start()
	{
		manager = this;
		Mob_L = Mob;
		Mob.Grow();
	}
    private void Update()
    {
    
    }
    public void Process(string command)
	{
		if (command == "Move")
        {
            if (Point > 0)
            {
                HavestImage.gameObject.SetActive(false);
                HavestText.gameObject.SetActive(false);
                MoveImage.gameObject.SetActive(false);
                MoveText.gameObject.SetActive(false);
            }
            //오른쪽으로 이동
            if (Player.currentTile.index < Mob.currentTile.index)
			{

                //식물쪽으로 한칸 더 움직일때
                if (Player.currentTile.index - Mob.currentTile.index== -1)
                {
                    //죽음 처리
                    GameOver();
					return;
				}
				//이동
				Player.SetTrigger("RightMove");
				Player.StartCoroutine("Move", Map.map.tiles[Player.currentTile.index + 1]);
				return;
			}
			//왼쪽으로 이동
			if (Player.currentTile.index > Mob.currentTile.index)
			{
				//식물쪽으로 한칸 더 움직일때
				if (Player.currentTile.index - Mob.currentTile.index == 1)
				{
					//죽음 처리
					GameOver();
					return;
				}
				//이동
				Player.SetTrigger("LeftMove");
				Player.StartCoroutine("Move", Map.map.tiles[Player.currentTile.index - 1]);
				return;
			}
		}
		if (command == "Harvest")
		{
			int temp = Player.currentTile.index - Mob.currentTile.index;
			if (temp*temp == 1)
			{
				//수확모션
				Player.SetTrigger("Harvest");
                if (Mob.GetComponent<Monster>().mobstate == true)
                {
                    if (twoTouch == 1)
                    {
                        Mob.Harvest2();
                        twoTouch *= -1;
                        changeMob = true;
                        StopAllCoroutines();
                    }
                    else
                    {
                        changeMob = false;
                        twoTouch *= -1;
                    }
                }
                else if (Mob.GetComponent<Monster>().mobstate == false)
                {
                    Mob.Harvest();
                    changeMob = true;
                    StopAllCoroutines();
                }
                //몹위치 변경
                if (changeMob == true)
                {
                    if (Mob == Mob_L)
                    {
                        Mob.leftSpawn();
                        Mob = Mob_R;
                    }
                    else if (Mob == Mob_R)
                    {
                        Mob.rightSpawn();
                        Mob = Mob_L;
                    }

                    if (Point > Mob2Velue)
                    {
                        Mob.GetComponent<Monster>().mobstate = true;
                        if (twoTouch == -1)
                            Mob.Grow2();
                    }
                    else
                        Mob.Grow();

                    //점수와 시간에 대한 곳.
                    RemainTime = RemainTime * ReduseRate;
                    Slider.maxValue = RemainTime;
                    if (RemainTime < 0.3f)
                        RemainTime = 0.3f;
                    StartCoroutine("TimeOut", RemainTime);
                    Point++;
                }
                return;
			}
			//죽음처리
			GameOver();
		}
	}
	void GameOver()
	{
		Debug.Log("게임오버");
		gameOver.SetActive(true);
        FinalGrade.text = "수확한 식물수 : " + Point;
    }
	IEnumerator TimeOut(float time)
	{
		while ((time -= Time.deltaTime) > 0)
		{
            Slider.value = ((float)(Math.Truncate(time * 100) / 100 ));
            Grade.text = "수확한 식물수 : " + Point;
			yield return null;
		}
		GameOver();
	}
}
