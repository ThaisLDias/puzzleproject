using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class PageCurl : MonoBehaviour
{
	protected Vector3 _cornerP;
	protected bool _initialized = false;
	protected Vector3 _pageOrigin;
	public Vector2 _pageSize;
	public bool animT = true;
	protected Vector3 apex = new Vector3(0, 0, -3);
	public bool autoMode = true;
	protected float deltaT = 0f;
	protected float kT = 1f;
	public bool lockRho = true;
	public bool lockTheta = true;
	public int[] newTriangles;
	public Vector3[] newVertices;
	public float PI2 = 3.141593f * 2;
	public float RAD = 0.5f;
	public int resH = 6;
	public int resW = 4;
	protected float rho = 0;
	protected float theta = 0.0f;
	public float timeStep = 0.01f;
	public Vector3[] v0;
	
	public void calcAuto(float t)
	{
		float num = ((float) 90) / RAD;
		if (t == 0f)
		{
			rho = 0;
			theta = num;
			apex.z = -25;
		}
		else
		{
			float num2;
			float num3;
			float num4;
			if (t <= 0.15f)
			{
				num2 = t / 0.15f;
				num3 = Mathf.Sin((3.141593f * Mathf.Pow(num2, 0.05f)) / 2f);
				num4 = Mathf.Sin((3.141593f * Mathf.Pow(num2, 0.5f)) / 2f);
				rho = t * 180f;
				theta = funcLinear(num3, 90f / RAD, 8f / RAD);
				apex.z = funcLinear(num4, 25f * -1, 2.5f * -1);
			}
			else if (t <= 0.4f)
			{
				num2 = (t - 0.15f) / 0.25f;
				rho = t * 180f;
				theta = funcLinear(num2, 8f / RAD, 6f / RAD);
				apex.z = funcLinear(num2, 2.5f * -1, 3.5f * -1);
			}
			else if (t <= 1f)
			{
				num2 = (t - 0.4f) / 0.6f;
				rho = t * 180f;
				num3 = Mathf.Sin((3.141593f * Mathf.Pow(num2, (float) 10)) / 2f);
				num4 = Mathf.Sin((3.141593f * Mathf.Pow(num2, 2f)) / 2f);
				theta = funcLinear(num3, 6f / RAD, 90f / RAD);
				apex.z = funcLinear(num4, 3.5f * -1, 15f * -1);
			}
		}
	}
	
	public float calcTheta(float _rho)
	{
		int num = 0;
		float num2 = 1f;
		float num3 = 0.05f;
		float num4 = ((float) 90) / RAD;
		float num5 = (num2 - num3) * num4;
		float num6 = _rho / 180f;
		if (num6 < 0.25f)
		{
			num = (int) (num6 / 0.25f);
		}
		else if (num6 < 0.5f)
		{
			num = (int) 1f;
		}
		else if (num6 <= 1f)
		{
			num = (int) ((1 - num6) * 0.5f);
		}
		return (num4 - (num * num5));
	}
	
	public float calcTheta2(float t)
	{
		float num = 0.1f;
		float num2 = ((float) 0x2d) / RAD;
		float num3 = Mathf.Abs((float) (1 - (t * 2)));
		return ((num * num2) + (num3 * num2));
	}
	
	public Vector3 curlTurn(Vector3 p)
	{
		float rhs = Mathf.Sqrt((p.x * p.x) + Mathf.Pow((p.z - apex.z),  2.0f));
		float num2 = rhs * Mathf.Sin(theta);
		float f = Mathf.Asin(p.x/rhs) / Mathf.Sin(theta);
		p.x = num2 * Mathf.Sin(f);
		p.z = (rhs + apex.z) - ((num2 * (1 - Mathf.Cos(f))) * Mathf.Sin(theta));
		p.y = (num2 * (1 - Mathf.Cos(f))) * Mathf.Cos(theta);
		return p;
	}
	
	public Vector3 flatTurn(Vector3 p)
	{
		theta = (deltaT * 3.141593f) * 2;
		float rhs = p.x/_pageSize.x;
		p.x = Mathf.Cos(theta) * rhs * _pageSize.x;
		p.y = Mathf.Sin(theta) * rhs * _pageSize.x;
		return p;
	}
	
	public float funcLinear(float ft, float f0, float f1)
	{
		return (f0 + ((f1 - f0) * ft));
	}
	
	public float funcQuad(float ft, float f0, float f1, float p)
	{
		return (f0 + ((f1 - f0) * Mathf.Pow(ft, p)));
	}
	
	public void initialize()
	{
		RAD = ((float) 360) / PI2;
		theta = 15f / RAD;
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		v0 = mesh.vertices;
		if (v0.Length > 0)
		{
			Vector3 vector = v0[0];
			Vector3 vector2 = v0[v0.Length - 1];
			float x = vector2.x;
			float z = vector2.z;
			_pageSize = new Vector2(x, z);
			_initialized = true;
			Debug.Log("mesh=" + mesh + " v0=" + v0 + " length=" + v0.Length + " first=" + vector + " last=" + vector2 + " _pageSize=" + _pageSize);
		}
	}
	
	public void LateUpdate()
	{
		if (!_initialized)
		{
			initialize();
		}
		else
		{
			if (animT)
			{
				deltaT = (kT * Time.time) % 1f;
			}
			if (autoMode)
			{
				calcAuto(deltaT);
			}
			if (v0.Length > 0)
			{
				renderMesh();
			}
		}
	}
	
	public void Main()
	{
	}
	
	public void OnGUI()
	{
		if (this._initialized)
		{
			GUI.BeginGroup(new Rect((float) 0x19, (float) 0x19, (float) (Screen.width - 50), (float) 80));
            this.deltaT = GUI.HorizontalSlider(new Rect((float) 100, (float) 5, (float) 300, (float) 30), this.deltaT, 1f, (float) 0);
            GUI.Label(new Rect((float) 0, (float) 0, (float) 100, (float) 20), "t = " + deltaT.ToString());
            this.animT = GUI.Toggle(new Rect((float) 420, (float) 0, (float) 80, (float) 20), this.animT, " Animate");
            if (this.animT)
            {
                this.autoMode = true;
            }
            this.autoMode = GUI.Toggle(new Rect((float) 500, (float) 0, (float) 80, (float) 20), this.autoMode, " Auto mode");
            if (!this.autoMode)
            {
                this.animT = false;
            }
            this.kT = GUI.HorizontalSlider(new Rect((float) 100, (float) 30, (float) 300, (float) 30), this.kT, 0.1f, 10f);
            GUI.Label(new Rect((float) 0, (float) 0x19, (float) 100, (float) 20), "k = " + kT.ToString());
            GUI.Label(new Rect((float) 420, (float) 0x19, (float) 200, (float) 20), "time scale (flips per second)");
            GUI.Label(new Rect((float) 0, (float) 50, (float) 100, (float) 20), "fps = " + (1f / Time.deltaTime).ToString());
            GUI.EndGroup();
            GUI.BeginGroup(new Rect((float) 0x19, (float) ((Screen.height / 2) - 200), (float) 120, (float) 400));
            this.apex.z = GUI.VerticalSlider(new Rect((float) 0, (float) 0, (float) 30, (float) 340), this.apex.z, (float) 0, (float) (-20));
            GUI.Label(new Rect((float) 0, (float) 350, (float) 100, (float) 20), "A = " + apex.z.ToString());
            GUI.Label(new Rect((float) 0, (float) 370, (float) 100, (float) 20), "cone apex");
            GUI.EndGroup();
            GUI.BeginGroup(new Rect((float) 0x19, (float) (Screen.height - 120), (float) (Screen.width - 50), (float) 100));
            this.theta = GUI.HorizontalSlider(new Rect((float) 120, (float) 5, (float) 280, (float) 30), this.theta, ((float) 1) / this.RAD, ((float) 90) / this.RAD);
            GUI.Label(new Rect((float) 0, (float) 0, (float) 180, (float) 20), "theta = \t" + (this.theta * this.RAD).ToString());
            GUI.Label(new Rect((float) 420, (float) 0, (float) 200, (float) 20), "cone angle");
            this.rho = GUI.HorizontalSlider(new Rect((float) 120, (float) 30, (float) 280, (float) 30), this.rho, (float) 180, (float) 0);
            GUI.Label(new Rect((float) 0, (float) 0x19, (float) 180, (float) 20), "rho = \t\t" + rho.ToString());
            GUI.Label(new Rect((float) 420, (float) 0x19, (float) 200, (float) 20), "page rotation");
            GUI.EndGroup();
		}
	}
	
	public void renderMesh()
	{
		float num2;
		Vector3 obj3;
		Vector3[] a = new Vector3[v0.Length];
		for (int i = 0; i < a.Length; i++)
		{
			Vector3 p = v0[i];
			p = curlTurn(p);
			a[i] = p;
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = a;
		mesh.RecalculateNormals();
		_cornerP = a[a.Length - 1];
		num2 = rho;
		Transform obj2 = GetComponent<Transform>();
		obj3 = obj2.eulerAngles;
		obj3.z = num2;
		//UnityRuntimeServices.ValueTypeChange[] changes = new UnityRuntimeServices.ValueTypeChange[] { new UnityRuntimeServices.MemberValueTypeChange(obj2, "eulerAngles", obj3) };
		//UnityRuntimeServices.PropagateValueTypeChanges(changes);
	}
	
	public Vector3 rise(int pointIndex)
	{
		Vector3 vector = v0[pointIndex];
		vector.y += Time.time;
		return vector;
	}
	
	public void Start()
	{
	}
	public Vector3 turn45(Vector3 p)
	{
		p.y = p.x;
		return p;
	}
}