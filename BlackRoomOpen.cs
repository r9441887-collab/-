using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using VolFx;

// Token: 0x02000029 RID: 41
public class BlackRoomOpen : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x00022B7C File Offset: 0x00020D7C
	private void Update()
	{
		this.Distance = Vector3.Distance(base.transform.position, this.Player.transform.position);
		if (!this.startHorror && this.Distance < this.StartHorrorDistance)
		{
			this.startHorror = true;
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.CreakingSound, false, 1f, 1f);
		}
		if (this.startHorror && !this.endHorror)
		{
			this.Distance = Mathf.Clamp(this.Distance, this.EndHorrorDistance, this.StartHorrorDistance);
			if (this.minDistance < this.Distance)
			{
				this.Distance = this.minDistance;
			}
			this.minDistance = this.Distance;
			float num = Mathf.InverseLerp(this.StartHorrorDistance, this.EndHorrorDistance, this.Distance);
			float num2 = Mathf.Lerp(0.2f, 0.5f, num);
			this.alphaValue = num;
			this.alphaValue2 = num2;
			SoundManager.Instance.BGM_ChangeVolume(num2);
			if (this.Distance <= this.EndHorrorDistance)
			{
				this.endHorror = true;
				num = 0f;
				WindowBugMovement component = SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.GetComponent<WindowBugMovement>();
				SingletoneBehaviour<SparrowSound>.Instance.Stop_SfxSound();
				component.gameObject.SetActive(false);
				component.GetPositions(1);
			}
			Color32 color;
			color..ctor(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(num * 255f));
			this.horrorFace.color = color;
		}
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00022CF4 File Offset: 0x00020EF4
	public void Reset()
	{
		this.readyOpenSecondDoor = false;
		this.startHorror = false;
		this.endTween = false;
		this.endHorror = false;
		this.secondDoorOpen = false;
		this.minDistance = this.StartHorrorDistance + 1f;
		this.firstDoor.isLocked = false;
		this.firstDoor.Opening(false, false);
		this.secondDoor.isLocked = false;
		this.secondDoor.Closing(false, false);
		this.secondDoor.isLocked = true;
		this.volumeProfile = this.MainVolume.profile;
		this.pixelation = this.GetPixelationVolume();
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000EE58 File Offset: 0x0000D058
	private void Start()
	{
		this.readyOpenSecondDoor = false;
		this.secondDoor.isLocked = true;
		this.volumeProfile = this.MainVolume.profile;
		this.pixelation = this.GetPixelationVolume();
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000EE8A File Offset: 0x0000D08A
	public void OpenFirstDoor()
	{
		this.firstDoor.OpenDoor();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00022D94 File Offset: 0x00020F94
	public void OpenSecondDoor()
	{
		if (!this.readyOpenSecondDoor)
		{
			return;
		}
		this.secondDoor.OpenDoor();
		if (!this.secondDoorOpen)
		{
			this.secondDoorOpen = true;
			SingletoneBehaviour<HorrorSceneManager>.Instance.SecondBlackDoor.gameObject.SetActive(true);
			SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.GetComponent<WindowBugMovement>().StartMove();
			SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.GetComponent<WindowBugMovement>().GetPositions(1);
			SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.GetComponent<WindowBugMovement>().ResetArrive = true;
			SingletoneBehaviour<HorrorSceneManager>.Instance.Eyes.GetComponent<EyesOnMe>().canEyeOpen = true;
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.MachineMovement, false, 1f, 1f);
			AudioSource crowSound = SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.CrowSound, false, 0.7f, 1f);
			DOVirtual.Float(10f, 0f, 10f, delegate(float value)
			{
				float num = Mathf.Clamp(value, 0f, 0.5f);
				crowSound.volume = num;
			});
			SoundManager.Instance.Play_SfxSound_2(SoundManager.SfxSound_2.BlackSkyMusic, true, 0.8f, 1f);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00022EA4 File Offset: 0x000210A4
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			base.StartCoroutine("SetBlackSky");
			DOVirtual.Float(0.91f, 0.85f, 0.5f, delegate(float f)
			{
				this.pixelation.m_Scale.value = f;
			});
			SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.SetActive(true);
			SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.GetComponent<WindowBugMovement>().StopMove();
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000EE97 File Offset: 0x0000D097
	public PixelationVol GetPixelationVolume()
	{
		if (!this.volumeProfile)
		{
			throw new NullReferenceException("VolumeProfile");
		}
		if (!this.volumeProfile.TryGet<PixelationVol>(ref this.pixelation))
		{
			throw new NullReferenceException("pixelation");
		}
		return this.pixelation;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000EED5 File Offset: 0x0000D0D5
	private IEnumerator SetBlackSky()
	{
		this.readyOpenSecondDoor = true;
		this.firstDoor.Closing(false, false);
		this.firstDoor.isLocked = true;
		yield return TweenExtensions.WaitForCompletion(this.firstDoor.tween);
		Debug.Log("First Door Closed");
		this.secondDoor.isLocked = false;
		SingletoneBehaviour<SkyBoxManager>.Instance.DarkSky();
		SingletoneBehaviour<HorrorSceneManager>.Instance.Enemy.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.secondDoor.isLocked = true;
		}
	}

	// Token: 0x0400016F RID: 367
	public DoorInteraction firstDoor;

	// Token: 0x04000170 RID: 368
	public DoorInteraction secondDoor;

	// Token: 0x04000171 RID: 369
	public GameObject Player;

	// Token: 0x04000172 RID: 370
	public float Distance;

	// Token: 0x04000173 RID: 371
	public SpriteRenderer horrorFace;

	// Token: 0x04000174 RID: 372
	public bool startHorror;

	// Token: 0x04000175 RID: 373
	public bool endTween;

	// Token: 0x04000176 RID: 374
	public bool endHorror;

	// Token: 0x04000177 RID: 375
	public bool secondDoorOpen;

	// Token: 0x04000178 RID: 376
	public bool readyOpenSecondDoor;

	// Token: 0x04000179 RID: 377
	public float minDistance;

	// Token: 0x0400017A RID: 378
	public float StartHorrorDistance = 50f;

	// Token: 0x0400017B RID: 379
	public float EndHorrorDistance = 40f;

	// Token: 0x0400017C RID: 380
	public float alphaValue;

	// Token: 0x0400017D RID: 381
	public float alphaValue2;

	// Token: 0x0400017E RID: 382
	[SerializeField]
	private Volume MainVolume;

	// Token: 0x0400017F RID: 383
	private VolumeProfile volumeProfile;

	// Token: 0x04000180 RID: 384
	private PixelationVol pixelation;
}
