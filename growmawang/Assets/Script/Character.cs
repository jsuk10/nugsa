using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private AudioSource audio;
    public Tile currentTile;
    [SerializeField] float moveSpeed;
	[SerializeField] Animator anim;
    [SerializeField] AudioClip playerSound;


    private void Start()
	{
		transform.position = currentTile.transform.position;
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.playerSound;
        this.audio.loop = false;
    }
	public IEnumerator Move(Tile End)
	{
		Tile Start = currentTile;
		//이동
		float time = 0f;
		Vector3 startPos = Start.transform.position;//; + Vector3.back;
		Vector3 endPos = End.transform.position;// + Vector3.back;
		currentTile = End;

		//Slerp관련 식 계산
		Vector3 center = (startPos + endPos) * 0.5f - Vector3.up * 0.1f;
		startPos -= center;
		endPos -= center;

		while (time <= 1f)
		{
			time += Time.deltaTime * moveSpeed;
			//transform.position = Vector3.LerpUnclamped(startPos, endPos, curve.Evaluate(time));
			transform.position = Vector3.Slerp(startPos, endPos, time);
			transform.position += center;
			yield return null;
		}
		currentTile = End;
		transform.position = End.transform.position;
	}
	public void SetTrigger(string trigger)
	{
		anim.SetTrigger(trigger);
	}
}
