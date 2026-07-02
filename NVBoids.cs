using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000D RID: 13
[HelpURL("https://nvjob.github.io/unity/nvjob-boids")]
[AddComponentMenu("#NVJOB/Boids/Simple Boids")]
public class NVBoids : MonoBehaviour
{
	// Token: 0x06000039 RID: 57 RVA: 0x0000E4D2 File Offset: 0x0000C6D2
	private void Awake()
	{
		this.thisTransform = base.transform;
		this.CreateFlock();
		this.CreateBird();
		base.StartCoroutine(this.BehavioralChange());
		base.StartCoroutine(this.Danger());
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000E506 File Offset: 0x0000C706
	private void OnDisable()
	{
		this.resetPosition = true;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0001EE28 File Offset: 0x0001D028
	private void OnEnable()
	{
		for (int i = 0; i < this.birdsNum; i++)
		{
			this.birdsTransform[i].localPosition = this.rdTargetPos[i];
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000E50F File Offset: 0x0000C70F
	private void LateUpdate()
	{
		this.FlocksMove();
		this.BirdsMove();
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000E51D File Offset: 0x0000C71D
	private void FollowPlayer()
	{
		base.transform.position = SingletoneBehaviour<HorrorSceneManager>.Instance.Player.transform.position;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x0001EE60 File Offset: 0x0001D060
	private void FlocksMove()
	{
		for (int i = 0; i < this.flockNum; i++)
		{
			this.flocksTransform[i].localPosition = Vector3.SmoothDamp(this.flocksTransform[i].localPosition, this.flockPos[i], ref this.velFlocks[i], this.smoothChFrequency);
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x0001EEBC File Offset: 0x0001D0BC
	private void BirdsMove()
	{
		float deltaTime = Time.deltaTime;
		this.timeTime += deltaTime;
		Vector3 vector = Vector3.forward * this.birdSpeed * this.dangerSpeedCh * deltaTime;
		Vector3 vector2 = Vector3.up * (this.verticalWawe * 0.5f - Mathf.PingPong(this.timeTime * 0.5f, this.verticalWawe));
		float num = this.soaring * this.dangerSoaring * deltaTime;
		for (int i = 0; i < this.birdsNum; i++)
		{
			if (this.birdsSpeedCur[i] != this.birdsSpeed[i])
			{
				this.birdsSpeedCur[i] = Mathf.SmoothDamp(this.birdsSpeedCur[i], this.birdsSpeed[i], ref this.spVelocity[i], 0.5f);
			}
			this.birdsTransform[i].Translate(vector * this.birdsSpeed[i]);
			Vector3 vector3 = this.flocksTransform[this.curentFlock[i]].position + this.rdTargetPos[i] + vector2 - this.birdsTransform[i].position;
			Quaternion quaternion = Quaternion.LookRotation(Vector3.RotateTowards(this.birdsTransform[i].forward, vector3, num, 0f));
			if (!this.rotationClamp)
			{
				this.birdsTransform[i].rotation = quaternion;
			}
			else
			{
				this.birdsTransform[i].localRotation = NVBoids.BirdsRotationClamp(quaternion, this.rotationClampValue);
			}
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000E53E File Offset: 0x0000C73E
	private IEnumerator Danger()
	{
		if (!this.danger)
		{
			this.dangerSpeedCh = (this.dangerSoaringCh = 1f);
			yield break;
		}
		NVBoids.delay0 = new WaitForSeconds(1f);
		for (;;)
		{
			if (Random.value > 0.9f)
			{
				this.dangerBird = Random.Range(0, this.birdsNum);
			}
			this.dangerTransform.localPosition = this.birdsTransform[this.dangerBird].localPosition;
			if (Physics.CheckSphere(this.dangerTransform.position, this.dangerRadius, this.dangerLayer))
			{
				this.dangerSpeedCh = this.dangerSpeed;
				this.dangerSoaringCh = this.dangerSoaring;
				yield return NVBoids.delay0;
			}
			else
			{
				this.dangerSpeedCh = (this.dangerSoaringCh = 1f);
			}
			yield return NVBoids.delay0;
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000E54D File Offset: 0x0000C74D
	private IEnumerator BehavioralChange()
	{
		for (;;)
		{
			yield return new WaitForSeconds(Random.Range(this.behavioralCh.x, this.behavioralCh.y));
			for (int i = 0; i < this.flockNum; i++)
			{
				if (Random.value < this.posChangeFrequency)
				{
					Vector3 vector = Random.insideUnitSphere * (float)this.fragmentedFlock;
					this.flockPos[i] = new Vector3(vector.x, Mathf.Abs(vector.y * this.fragmentedFlockYLimit), vector.z);
				}
			}
			for (int j = 0; j < this.birdsNum; j++)
			{
				this.birdsSpeed[j] = Random.Range(3f, 7f);
				Vector3 vector2 = Random.insideUnitSphere * (float)this.fragmentedBirds;
				this.rdTargetPos[j] = new Vector3(vector2.x, vector2.y * this.fragmentedBirdsYLimit, vector2.z);
				if (Random.value < this.migrationFrequency)
				{
					this.curentFlock[j] = Random.Range(0, this.flockNum);
				}
			}
		}
		yield break;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0001F058 File Offset: 0x0001D258
	private void CreateFlock()
	{
		this.flocksTransform = new Transform[this.flockNum];
		this.flockPos = new Vector3[this.flockNum];
		this.velFlocks = new Vector3[this.flockNum];
		this.curentFlock = new int[this.birdsNum];
		for (int i = 0; i < this.flockNum; i++)
		{
			GameObject gameObject = GameObject.CreatePrimitive(0);
			gameObject.SetActive(this.debug);
			this.flocksTransform[i] = gameObject.transform;
			Vector3 vector = Random.onUnitSphere * (float)this.fragmentedFlock;
			this.flocksTransform[i].position = this.thisTransform.position;
			this.flockPos[i] = new Vector3(vector.x, Mathf.Abs(vector.y * this.fragmentedFlockYLimit), vector.z);
			this.flocksTransform[i].parent = this.thisTransform;
		}
		if (this.danger)
		{
			GameObject gameObject2 = GameObject.CreatePrimitive(0);
			gameObject2.GetComponent<MeshRenderer>().enabled = this.debug;
			gameObject2.layer = base.gameObject.layer;
			this.dangerTransform = gameObject2.transform;
			this.dangerTransform.parent = this.thisTransform;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0001F19C File Offset: 0x0001D39C
	private void CreateBird()
	{
		this.birdsTransform = new Transform[this.birdsNum];
		this.birdsSpeed = new float[this.birdsNum];
		this.birdsSpeedCur = new float[this.birdsNum];
		this.rdTargetPos = new Vector3[this.birdsNum];
		this.spVelocity = new float[this.birdsNum];
		for (int i = 0; i < this.birdsNum; i++)
		{
			this.birdsTransform[i] = Object.Instantiate<GameObject>(this.birdPref, this.thisTransform).transform;
			this.birdsTransform[i].gameObject.layer = LayerMask.NameToLayer("Horror3D");
			Vector3 vector = Random.insideUnitSphere * (float)this.fragmentedBirds;
			this.birdsTransform[i].localPosition = (this.rdTargetPos[i] = new Vector3(vector.x, vector.y * this.fragmentedBirdsYLimit, vector.z));
			this.birdsTransform[i].localScale = Vector3.one * Random.Range(this.scaleRandom.x, this.scaleRandom.y);
			this.birdsTransform[i].localRotation = Quaternion.Euler(0f, Random.value * 360f, 0f);
			this.curentFlock[i] = Random.Range(0, this.flockNum);
			this.birdsSpeed[i] = Random.Range(3f, 7f);
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0001F320 File Offset: 0x0001D520
	private static Quaternion BirdsRotationClamp(Quaternion rotationCur, float rotationClampValue)
	{
		Vector3 eulerAngles = rotationCur.eulerAngles;
		rotationCur.eulerAngles = new Vector3(Mathf.Clamp((eulerAngles.x > 180f) ? (eulerAngles.x - 360f) : eulerAngles.x, -rotationClampValue, rotationClampValue), eulerAngles.y, 0f);
		return rotationCur;
	}

	// Token: 0x04000062 RID: 98
	[Header("General Settings")]
	public Vector2 behavioralCh = new Vector2(2f, 6f);

	// Token: 0x04000063 RID: 99
	public bool debug;

	// Token: 0x04000064 RID: 100
	[Header("Flock Settings")]
	[Range(1f, 150f)]
	public int flockNum = 2;

	// Token: 0x04000065 RID: 101
	[Range(0f, 5000f)]
	public int fragmentedFlock = 30;

	// Token: 0x04000066 RID: 102
	[Range(0f, 1f)]
	public float fragmentedFlockYLimit = 0.5f;

	// Token: 0x04000067 RID: 103
	[Range(0f, 1f)]
	public float migrationFrequency = 0.1f;

	// Token: 0x04000068 RID: 104
	[Range(0f, 1f)]
	public float posChangeFrequency = 0.5f;

	// Token: 0x04000069 RID: 105
	[Range(0f, 100f)]
	public float smoothChFrequency = 0.5f;

	// Token: 0x0400006A RID: 106
	[Header("Bird Settings")]
	public GameObject birdPref;

	// Token: 0x0400006B RID: 107
	[Range(1f, 9999f)]
	public int birdsNum = 10;

	// Token: 0x0400006C RID: 108
	[Range(0f, 150f)]
	public float birdSpeed = 1f;

	// Token: 0x0400006D RID: 109
	[Range(0f, 100f)]
	public int fragmentedBirds = 10;

	// Token: 0x0400006E RID: 110
	[Range(0f, 1f)]
	public float fragmentedBirdsYLimit = 1f;

	// Token: 0x0400006F RID: 111
	[Range(0f, 10f)]
	public float soaring = 0.5f;

	// Token: 0x04000070 RID: 112
	[Range(0.01f, 500f)]
	public float verticalWawe = 20f;

	// Token: 0x04000071 RID: 113
	public bool rotationClamp;

	// Token: 0x04000072 RID: 114
	[Range(0f, 360f)]
	public float rotationClampValue = 50f;

	// Token: 0x04000073 RID: 115
	public Vector2 scaleRandom = new Vector2(1f, 1.5f);

	// Token: 0x04000074 RID: 116
	[Header("Danger Settings (one flock)")]
	public bool danger;

	// Token: 0x04000075 RID: 117
	public float dangerRadius = 15f;

	// Token: 0x04000076 RID: 118
	public float dangerSpeed = 1.5f;

	// Token: 0x04000077 RID: 119
	public float dangerSoaring = 0.5f;

	// Token: 0x04000078 RID: 120
	public LayerMask dangerLayer;

	// Token: 0x04000079 RID: 121
	[Header("Information")]
	public string HelpURL = "nvjob.github.io/unity/nvjob-boids";

	// Token: 0x0400007A RID: 122
	public string ReportAProblem = "nvjob.github.io/support";

	// Token: 0x0400007B RID: 123
	public string Patrons = "nvjob.github.io/patrons";

	// Token: 0x0400007C RID: 124
	private Transform thisTransform;

	// Token: 0x0400007D RID: 125
	private Transform dangerTransform;

	// Token: 0x0400007E RID: 126
	private int dangerBird;

	// Token: 0x0400007F RID: 127
	private Transform[] birdsTransform;

	// Token: 0x04000080 RID: 128
	private Transform[] flocksTransform;

	// Token: 0x04000081 RID: 129
	private Vector3[] rdTargetPos;

	// Token: 0x04000082 RID: 130
	private Vector3[] flockPos;

	// Token: 0x04000083 RID: 131
	private Vector3[] velFlocks;

	// Token: 0x04000084 RID: 132
	private float[] birdsSpeed;

	// Token: 0x04000085 RID: 133
	private float[] birdsSpeedCur;

	// Token: 0x04000086 RID: 134
	private float[] spVelocity;

	// Token: 0x04000087 RID: 135
	private int[] curentFlock;

	// Token: 0x04000088 RID: 136
	private float dangerSpeedCh;

	// Token: 0x04000089 RID: 137
	private float dangerSoaringCh;

	// Token: 0x0400008A RID: 138
	private float timeTime;

	// Token: 0x0400008B RID: 139
	private static WaitForSeconds delay0;

	// Token: 0x0400008C RID: 140
	public bool resetPosition;
}
