using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]GameObject gameOver;
    [SerializeField]GameObject timeSlider;
    [SerializeField] Text Timer;
	public static Manager manager;

	[SerializeField] Character Player;
	[SerializeField] public Monster Mob_L;
	[SerializeField] public Monster Mob_R;
	[SerializeField] Monster Mob;

	[SerializeField] float 남은시간;
	[SerializeField] float 줄어드는_비율;

	private void Start()
	{
		manager = this;
		Mob_L = Mob;
		Mob.Grow();
	}
	public void Process(string command)
	{

		if (command == "Move")
		{
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
				StopAllCoroutines();
				//수확모션
				Player.SetTrigger("Harvest");

				Mob.Harvest();

				//몹위치 변경
				if (Mob == Mob_L)
				{
					Mob.leftSpawn();
					Mob = Mob_R;
				}
				else
				{
					Mob.rightSpawn();
					Mob = Mob_L;
				}
				Mob.Grow();
				남은시간 = 남은시간 * 줄어드는_비율;
				if (남은시간< 0.3f) 남은시간 = 0.3f;
				StartCoroutine("TimeOut", 남은시간);

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
	}
	IEnumerator TimeOut(float time)
	{
		while ((time -= Time.deltaTime) > 0)
		{
			Timer.text = "남은시간 : " + (Math.Truncate(time*100)/100).ToString();
			yield return null;
		}
		GameOver();
	}
}
