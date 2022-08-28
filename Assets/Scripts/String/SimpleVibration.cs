using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVibration : MonoBehaviour
{
    Mesh mesh;
    MeshCollider meshCollider;
    MeshFilter meshFilter;
    Vector3[] vers;
    Vector3[] vers_origin;
    Vector3[] vers_drag;
    float[] vers_z;
    bool isTriggered = false;
    bool isVibrating = false;
    bool isDragging = false;
    bool isRoot = false;
    bool isForcing = false;
    float rootDis;
    float rootPos;
    float currentDis;
    SimpleVibration target;
    List<float>[,] data;

    //vibration params

    float rho=0.000467f;
    [Header("Vibration Params")]
    [Tooltip("弦长l")]
    [Range(0.1f, 100f)]
    public float l=1.5f;
    
    [Tooltip("高度h")]
    [Range(0.001f, 0.1f)]
    public float h=0.001f;
    
    float F = 200f;
    int n = 1;
    [Tooltip("模拟精度Δt")]
    [Range(0.00001f, 0.001f)]
    public float delta=0.0001f;
    [Tooltip("模拟速度s")]
    [Range(1, 10)]
    public int speed=1;
    [Tooltip("摩擦系数ff")]
    [Range(0.01f, 1f)]
    public float fric_fac=0.5f;
    [Tooltip("停止系数sf")]
    [Range(0.01f, 1f)]
    public float stop_fac=0.15f;
    [Tooltip("跳帧ts")]
    [Range(1, 10)]
    public int tick_skip=1;
    float c ;//震动传播速度
    int tick_now=0;
    float t;//时间变量
    [SerializeField]
    [Tooltip("离散时间t")]
    private int t_discrete;//离散时间变量
    [SerializeField]
    [Tooltip("剩余高度h")]
    private float h_friction;//摩擦后的h
    float T;
    [SerializeField]
    [Tooltip("离散时间周期T")]
    private int t_count;
    int[] t_Lens;
    float z_lim;
    float z_last;
    float z_first;
    bool isZLimed=false;

    [Header("DragInteractive Params")]
    [Tooltip("Drag影响范围")]
    [Range(0.05f, 1f)]
    public float dragArea=0.1f;
    [Tooltip("Drag强度")]
    [Range(0.0001f, 0.1f)]
    public float dragIntensity=0.001f;
    [Tooltip("Drag限制")]
    [Range(10f, 50f)]
    public float dragLimit=0.1f;
    [Tooltip("Drag帧数")]
    [Range(1, 60)]
    public int dragFrame=30;
    [Tooltip("Drag速度")]
    [Range(0.1f, 1f)]
    public float dragSpeed=0.5f;
    [Header("振动速度相关 Params")]
    [Tooltip("SL分层程度")]
    [Range(5, 100)]
    public int splitLevel = 15;
    [Tooltip("lI竖直影响程度")]
    [Range(0.1f, 10f)]
    public float levelIntensity = 2f;
    float l_Vibration;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        //meshCollider = GetComponent<MeshCollider>();
        vers =new Vector3[mesh.vertexCount];
        vers_origin=new Vector3[mesh.vertexCount];
        vers_drag=new Vector3[mesh.vertexCount];
        mesh.vertices.CopyTo(vers,0);
        mesh.vertices.CopyTo(vers_origin,0);
        mesh.vertices.CopyTo(vers_drag,0);
        vers_z=new float[vers.Length];
        z_last=vers[0].y;
        z_first=z_last;
        
        float z;
        for(int i=0;i<vers.Length;i++)
        {
            z=vers[i].y;
            if(z_last>z)
            {
                z_last=z;
            }
            if(z_first<z)
            {
                z_first = z;
            }
            //vers_z.SetValue(z+l/2, i);
        }
        //Debug.Log("z_first"+z_first);
        //Debug.Log("z_last"+z_last);
        for (int i = 0; i < vers.Length; i++)
        {
            z = vers[i].y;
            vers_z.SetValue(z - z_last, i);
        }
        // Debug.Log(z_last);
        z_lim=Mathf.NegativeInfinity;
        c = Mathf.Sqrt(F / rho);
        h_friction=h;
        t=0;
        data = new List<float>[vers.Length, splitLevel];
        t_Lens = new int[splitLevel];
        for (int i = 0; i < splitLevel; i++)
        {
            T = 2 * Mathf.PI / (3 * Mathf.PI * c / ((i + 1) * l / splitLevel));
            t_count = (int)(T / delta);
            t_Lens[i] = t_count;
            Debug.Log($"T:{t_count}");
        }
        l_Vibration = l;
        PreSimulation();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered){    
            // Debug.Log("Start Vibrating!");
            isVibrating=true;
            h_friction=h;
            isTriggered=false;
        }
        if(isVibrating){
            if(h_friction<stop_fac * h){
                // Debug.Log("Vibration Over!");
                isVibrating=false;
                StopVibration();
            }else{
                if(tick_now!=0){
                }
                else
                {
                    if(isZLimed){
                        Vibration(z_lim);
                    }
                    else
                    {
                        Vibration();
                    }
                    // Debug.Log("Vibrating!");
                }
                h_friction=Mathf.Lerp(h_friction,0,fric_fac * Time.deltaTime);
                t_discrete+=1*speed;
                t_discrete=t_discrete%t_count;
                
                tick_now=(++tick_now)%tick_skip;
            }
            
        }
    }

    public void StopVibration()
    {
        h_friction=0;
        t_discrete=0;
        mesh.vertices=vers_origin;
        mesh.RecalculateBounds();
    }

    void Vibration(){
        vers_drag.CopyTo(vers, 0);
        level = getLLevel();
        for (int j=0;j<vers.Length;j++){
            vers[j].z+= h_friction / h * data[j, level][t_discrete % t_Lens[level]];
        }
        mesh.vertices=vers;
        mesh.RecalculateBounds();
    }
    void Vibration(float limz){
        vers_drag.CopyTo(vers, 0);
        level = getLLevel();
        Debug.Log($"level:{level}");
        for (int j=0;j<vers.Length;j++){
            if(vers[j].y>limz){
                vers[j].z+= h_friction / h * data[j, level][t_discrete % t_Lens[level]];
            }
            
        }
        mesh.vertices=vers;
        mesh.RecalculateBounds();
    }
    float VibrationSimulator(float x, float ll)
    {
        float yn_xt = 0;
        for (int i = 0; i < n; i++)
        {
            yn_xt += 32 * h_friction / Mathf.Pow(Mathf.PI, 3f) / Mathf.Pow(2 * i + 1, 3) * Mathf.Cos((2 * i + 1) * Mathf.PI * c * t / ll) * Mathf.Sin((2 * i + 1) * Mathf.PI * x / ll);
        }
        return yn_xt;

    }

    void PreSimulation(){
        List<float> temp;
        for(int i=0;i<vers_origin.Length;i++){
            for(int j=0;j< splitLevel; j++)
            {
                t = 0;
                temp = new List<float>(t_Lens[j]);
                for (int p = 0; p < t_Lens[j]; p++){ 
                    temp.Add(VibrationSimulator(vers_z[i],(j+1)*l/splitLevel));
                    t += delta;
                }
                data[i, j] = temp;
            }
        }
    }


    /// <summary>  
    /// 触发振动  
    /// </summary>  
    public void Trigger(){
        isTriggered=true;
    }
    /// <summary>  
    /// 触发振动，限制z轴
    /// </summary>
    public void TriggerLeave(){
        isZLimed=false;
    }
    public void TriggerLimZSet(float limit_z){
        isZLimed=true;
        z_lim = limit_z;
        // Debug.Log("z_lim:"+z_lim);
    }
    // float VibrationBase(int n ,float x)
    // {
    //     // generate basic vibration model
    //     return Mathf.Cos((2 * n + 1) * Mathf.PI * c * t / l) * Mathf.Sin((2 * n + 1) * Mathf.PI * x / l);
    // }
    /// <summary>  
    /// 提供距离和位置进行弦拖动
    /// </summary>
    public void OnStringDragging(float dis,float pos){
        isDragging = true;
        vers_origin.CopyTo(vers_drag, 0);
        currentDis = dis;
        rootPos = pos;
        l_Vibration = Mathf.Sqrt(Mathf.Pow(dragIntensity * levelIntensity * dis, 2) + Mathf.Pow(z_first - pos, 2));
        float k1 = dis/(z_first-pos);
        float k2 =dis/(pos-z_last);
        //float k1 = 50f;
        //float k2 = 20f;
        //Debug.Log($"z_f:{z_first}");
        //Debug.Log($"z_l:{z_last}");
        //Debug.Log($"pos:{pos}");
        // Debug.Log($"dis:{dis}");
        // Debug.Log($"k1:{k1}");
        // Debug.Log($"k2:{k2}");
        for (int j=0;j<vers.Length;j++){
            // if(Mathf.Abs(vers[j].y-pos)<l*0.5f*dragArea){
                // vers_drag[j].z+=Mathf.Max(
                //     Mathf.Min(
                //     dis*dragIntensity*Mathf.Cos(2*Mathf.PI/(l*2f*dragArea)*(vers[j].y-pos)),
                //     dragLimit*h)
                //     ,-dragLimit*h
                // );
            // }
            //Debug.Log($"vers_origin[j]:{vers_origin[j].y}");
            
            if(vers[j].y>=pos)
            {
                vers_drag[j].z += Mathf.Max(
                    Mathf.Min(
                    dragIntensity * k1 * (z_first - vers[j].y),
                    dragLimit * h)
                    , -dragLimit * h
                );
            }
            else
            {
                vers_drag[j].z += Mathf.Max(
                    Mathf.Min(
                    dragIntensity * k2 * (vers[j].y - z_last),
                    dragLimit * h)
                    , -dragLimit * h
                );
            }
        }
        mesh.vertices=vers_drag;
        mesh.RecalculateBounds();
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
    }
    public IEnumerator OnStringDragEnd(){
        int n =dragFrame;
        isDragging = false;
        // vers_origin.CopyTo(vers,0);
        while (n>0){
            for (int j=0;j<vers.Length;j++){
                vers_drag[j].z=Mathf.Lerp(vers_origin[j].z,vers_drag[j].z,dragSpeed);
            }
            mesh.vertices=vers_drag;
            mesh.RecalculateBounds();
            n-=1;
            yield return null;
        }
        mesh.vertices=vers_origin;
        mesh.RecalculateBounds();
        yield break;
    }
    int getLLevel()
    {
        if (isDragging) return Mathf.Min((int)(l_Vibration / l * splitLevel), splitLevel - 1);
        return splitLevel - 1;
    }
    public void setRoot()
    {
        isRoot = true;
    }
    public void leaveRoot()
    {
        isRoot = false;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //    if (other.GetComponent<SimpleVibration>() == null) return;
    //    SimpleVibration o = other.GetComponent<SimpleVibration>();
    //    if (o.isRoot) return;
    //    if (o.isForcing) return;
    //    target = o;
    //    target.isForcing = true;
    //    rootDis = currentDis;
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (target == null) return;
    //    if (isRoot) return;
    //    target.OnStringDragging(currentDis - rootDis, rootPos);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (target == null) return;
    //    target.StartCoroutine(OnStringDragEnd());
    //    target.isForcing = false;
    //    target = null;
    //}
}
