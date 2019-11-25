using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject MenuUi;
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
    [SerializeField] GameObject HaverstButton;
    [SerializeField] Image MoveImage;
    [SerializeField] Text MoveText;
    [SerializeField] GameObject MoveButton;

    [SerializeField] float remainTime = 5.0f;
	[SerializeField] float reduseRate = 0.3f;
    [SerializeField] int point = 0;
    [SerializeField] int mob2Velue = 2;
    [SerializeField] bool changeMob = true;
    [SerializeField] int mobNum = 5;
    int TouchCount = 0;
    bool Timestate = true;
    int randomSponV;

    private void Start()
    {
        MenuUi.SetActive(false);
        gameOver.SetActive(false);
        manager = this;
		Mob_L = Mob;
		Mob.Grow();
	}
    private void Update()
    {
    }
    public void Process(string command)
	{
        //스탑 버튼의 멈추는 함수
        if (command == "Menu")
        {
            if (Timestate == true)
            {
                Time.timeScale = 0;
                HaverstButton.SetActive(false);
                MoveButton.SetActive(false);
                MenuUi.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                HaverstButton.SetActive(true);
                MoveButton.SetActive(true);
                MenuUi.SetActive(false);

            }
            Timestate = !Timestate;
        }
        //스탑 버튼의 멈췄다가 다시 작동하는 함수
        if (command == "Restart")
            Time.timeScale = 1;

        //무브 버튼 함수
        if (command == "Move")
        {
            if (point > 0)
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
                    StartCoroutine(GameOver());
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
                    StartCoroutine(GameOver());
                    return;
				}
				//이동
				Player.SetTrigger("LeftMove");
				Player.StartCoroutine("Move", Map.map.tiles[Player.currentTile.index - 1]);
				return;
			}
		}
        //수확 버튼의 함수.
		if (command == "Harvest")
		{
            int temp = Player.currentTile.index - Mob.currentTile.index;
			if (temp*temp == 1)
			{
				//수확모션
				Player.SetTrigger("Harvest");
                if (Mob.GetComponent<Monster>().mobstate == true)
                {
                    if (TouchCount == 1)
                    {
                        Mob.Harvest2();
                        point += 2;
                        TouchCount = 0;
                        changeMob = true;
                        StopAllCoroutines();
                    }
                    else
                    {
                        changeMob = false;
                        TouchCount++;
                    }
                }
                else if (Mob.GetComponent<Monster>().mobstate == false)
                {
                    Mob.Harvest();
                    point++;
                    changeMob = true;
                    StopCoroutine("TimeOut");
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

                    if(point > mob2Velue)
                        randomSponV = UnityEngine.Random.Range(0, 5);
    
                    if (randomSponV == mob2Velue)
                        {
                        Mob.Mob2ready();
                        Mob.GetComponent<Monster>().mobstate = true;
                            if (TouchCount == 0)
                                Mob.Grow2();
                        }
                        else
                            Mob.Grow();
                    }
                    //점수와 시간에 대한 곳.
                    remainTime = remainTime *(1 - reduseRate);
                    Slider.maxValue = remainTime;
                    if (remainTime < 0.3f)
                        remainTime = 0.3f;
                    StartCoroutine("TimeOut", remainTime);
                
                return;
			}
            //죽음처리
            StartCoroutine(GameOver());
		}
	}

    IEnumerator GameOver()
    {
        Player.SetTrigger("Die");
        HaverstButton.SetActive(false);
        MoveButton.SetActive(false);

        yield return new WaitForSeconds(1.2f);
        gameOver.SetActive(true);
        FinalGrade.text = "수확한 식물수 : " + point;
    }
    IEnumerator TimeOut(float time)
	{
		while ((time -= Time.deltaTime) > 0)
		{
            Slider.value = ((float)(Math.Truncate(time * 100) / 100 ));
            Grade.text = "수확한 식물수 : " + point;
			yield return null;
		}
        StartCoroutine(GameOver());
    }
}
