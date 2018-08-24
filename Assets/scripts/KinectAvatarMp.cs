using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

// 関節データを管理するクラス
public class JointsDataMp {
    // アバターの関節データ
    public GameObject[] Joints;

    // 関節データのインデックス
    public enum JOINTS_IDX
    {
        REFERENCE,      // 
        LEFT_UP_LEG,    //
        RIGHT_UP_LEG,   //
        LEFT_LEG,       //
        RIGHT_LEG,      //
        SPINE1,         //
        NECK,           //
        HEAD,           //
        LEFT_ARM,       //
        RIGHT_ARM,      //
        LEFT_FORE_ARM,  //
        RIGHT_FORE_ARM, //
        LEFT_HAND,      //
        RIGHT_HAND,     //
        NUM_OF_ELE,     // 要素数
    }

    // コンストラクタ
    public JointsDataMp()
    {
        // 関節データの数だけメモリを確保
        Joints = new GameObject[(int)JOINTS_IDX.NUM_OF_ELE];

        // nullで初期化
        for(int i = 0; i < (int)JOINTS_IDX.NUM_OF_ELE; i++)
        {
            Joints[i] = null;
        }
    }
}


public class AvatarDataMp
{
    // 同時に表示する人数
    public const int MAX_PLAYERS = 3;

    // アバターの種類　※Unityちゃん含む
    public const int KIND_AVATARS = 5;

    // 各アバターの関節データ
    public JointsDataMp[,] jointsData;

    public bool[,] isRigify = new bool[MAX_PLAYERS, KIND_AVATARS];
    
    public AvatarDataMp()
    {

    }
    public bool init()
    {
        readJoints();
        initSpawnPosOfJoints();

        return true;
    }
    private bool readJoints()
    {
        jointsData = new JointsDataMp[MAX_PLAYERS, KIND_AVATARS];

        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            for (int j = 0; j < KIND_AVATARS; j++)
            {
                Debug.Log("i,j:["+i+", "+j+"]");
                if(j == 0)
                {
                    this.isRigify[i,j] = false;
                    string objectRootPath = "unitychan";

                    Debug.Log("read : " + objectRootPath);

                    //Debug.Log("count : " + UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference").gameObject.transform.childCount);
                    //Debug.Log("name  : " + UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference").gameObject.transform.name);

                    jointsData[i, j] = new JointsDataMp();

                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.REFERENCE]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_UP_LEG]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_LeftUpLeg").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_UP_LEG]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_RightUpLeg").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_LEG]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_LeftUpLeg/Character1_LeftLeg").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_LEG]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_RightUpLeg/Character1_RightLeg").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.SPINE1]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.NECK]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.HEAD]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/Character1_Head").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_ARM]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_ARM]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_FORE_ARM]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_FORE_ARM]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_HAND]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm/Character1_LeftHand").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_HAND]
                        = UnityEngine.GameObject.Find(objectRootPath + "/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand").gameObject;
                }
                else
                {
                    GameObject[] avaters = UnityEngine.GameObject.FindGameObjectsWithTag("Character");
                    int k = j-1;

                    Debug.Log("read : " + avaters[k].gameObject.name);
                    this.isRigify[i, j] = true;
                    jointsData[i, j] = new JointsDataMp();

                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.REFERENCE]
                        = avaters[k].gameObject.transform.Find("metarig").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_UP_LEG]
                        = avaters[k].gameObject.transform.Find("metarig/spine/thigh_L").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_UP_LEG]
                        = avaters[k].gameObject.transform.Find("metarig/spine/thigh_R").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_LEG]
                        = avaters[k].gameObject.transform.Find("metarig/spine/thigh_L/shin_L").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_LEG]
                        = avaters[k].gameObject.transform.Find("metarig/spine/thigh_R/shin_R").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.SPINE1]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.NECK]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/spine_004").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.HEAD]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/spine_004/spine_005/spine_006").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_ARM]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/shoulder_L/upper_arm_L").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_ARM]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/shoulder_R/upper_arm_R").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_FORE_ARM]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/shoulder_L/upper_arm_L/forearm_L").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_FORE_ARM]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/shoulder_R/upper_arm_R/forearm_R").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_HAND]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/shoulder_L/upper_arm_L/forearm_L/hand_L").gameObject;
                    jointsData[i, j].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_HAND]
                        = avaters[k].gameObject.transform.Find("metarig/spine/spine_001/spine_002/spine_003/shoulder_R/upper_arm_R/forearm_R/hand_R").gameObject;
                
                }
            }
        }

        return true;
    }

    private bool initSpawnPosOfJoints()
    { 
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            for(int j = 0; j < KIND_AVATARS; j++)
            {
                jointsData[i, j].Joints[0].transform.position = new Vector3(999, 999, 999);
                  
            }
        }
        return true;
    }
    

    

}


public class KinectAvatarMp : MonoBehaviour
{
    AvatarDataMp avatarData;

    [SerializeField] const int NUM_OF_MAX_AVATARS = 6;       // 最大検出人数
    Body[] bodys;

    [SerializeField] BodySourceManager bodySourceManager;

    //自分の関節とUnityちゃんのボーンを入れるよう
    [SerializeField] int PosScale;

    [SerializeField] bool IsMirror;


    Quaternion SpineBase;
    Quaternion SpineMid;
    Quaternion SpineShoulder;

    Quaternion Head;
    Quaternion Neck;

    Quaternion ShoulderLeft;
    Quaternion ShoulderRight;
    Quaternion ElbowLeft;
    Quaternion WristLeft;
    Quaternion HandLeft;
    Quaternion ElbowRight;
    Quaternion WristRight;
    Quaternion HandRight;
    Quaternion KneeLeft;
    Quaternion AnkleLeft;
    Quaternion KneeRight;
    Quaternion AnkleRight;


    int[] avatarTypeOfPlayer = new int[AvatarDataMp.MAX_PLAYERS];

    // Kinectの関節回転データをUnity用のクォータニオンへ変換する
    private bool kinectJointRotToUnityQuat(
        JointOrientation joint,                         // Kinectで取得した関節データ
        ref Quaternion planeQuat,                           // Kinectの傾きを表すクォータニオン
        ref Quaternion quat,                                // Unity用のクォータニオン        
        bool isMirror = false                               // 鏡合わせにするかのフラグ
    )
    {
        if (isMirror)
        {
            quat = joint.Orientation.ToMirror().ToQuaternion(planeQuat);
        }
        else
        {
            quat = joint.Orientation.ToQuaternion(planeQuat);
        }
        return true;
    }


    // [説明]
    //      Kinectから画面に映っている人のボディデータを取得する
    // [戻り値：bool型]
    //      ・true：１人以上のデータ取得に成功
    //      ・false：データの取得に失敗
    private bool readBodyFromKinect(ref Body[] bodys)
    {
        // 現在の各ボディデータを取得する
        Body[] currentBodyArray = bodySourceManager.GetData().ToArray();



        int cnt = 0;
        for(int i = 0; i < 6; i++)
        {
            if (currentBodyArray[i].IsTracked)
            {
                cnt++;
            }
        }

        Debug.Log("Detected Human : " + cnt);


        if (cnt == 0)
        {
            bodys = null;
            return false;
        }

        // 検出された人数分のボディデータを取得する
        cnt = 0;
        bodys = new Body[currentBodyArray.Length];
        for (int i = 0; i < NUM_OF_MAX_AVATARS; i++)
        {
            if (currentBodyArray[i].IsTracked)
            {
                bodys[cnt] = currentBodyArray[i];
                cnt++;
            }
        }
        


        return true;
    }

    static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    // Use this for initialization
    void Start()
    {
        avatarData = new AvatarDataMp();
        avatarData.init();
    }



    // Update is called once per frame
    void Update()
    {
        //最初に追尾している人のBodyデータを取得する
        Body body = bodySourceManager.GetData().FirstOrDefault(b => b.IsTracked);

        readBodyFromKinect(ref bodys);

        // Kinectを斜めに置いてもまっすぐにするようにする
        var floorPlane = bodySourceManager.FloorClipPlane;
        Quaternion comp = Quaternion.FromToRotation(
            new Vector3(-floorPlane.X, floorPlane.Y, floorPlane.Z), Vector3.up);

        Quaternion q;
        Quaternion comp2;
        Quaternion compNeck;
        Quaternion compBody;
        Quaternion compRLeg;
        Quaternion compRArm;
        Quaternion compLArm;
        CameraSpacePoint pos;


        // ボディデータの取得に失敗した場合
        if (bodys.Length == 0)
        {
            // [デバッグメッセージ] ボディデータの取得に失敗
            Debug.Log(" ******************** Failed to Detect Any Body ******************** ");

            // 何もせずに終了
            return;
        }

        

        for (int playerNum = 0; playerNum < bodys.Length; playerNum++)
        {
            // 各関節の回転データを取得する
            var joints = bodys[playerNum].JointOrientations;


            {
                // 腰　中央
                kinectJointRotToUnityQuat(
                    joints[JointType.SpineBase],
                    ref comp,
                    ref SpineBase,
                    IsMirror
                    );

                // 腹　中央
                kinectJointRotToUnityQuat(
                    joints[JointType.SpineMid],
                    ref comp,
                    ref SpineMid,
                    IsMirror
                    );

                // 頭
                kinectJointRotToUnityQuat(
                    joints[JointType.Head],
                    ref comp,
                    ref Head,
                    IsMirror
                    );

                // 首
                kinectJointRotToUnityQuat(
                    joints[JointType.Neck],
                    ref comp,
                    ref Neck,
                    IsMirror
                    );

                // 肩　中心？
                kinectJointRotToUnityQuat(
                    joints[JointType.SpineShoulder],
                    ref comp,
                    ref SpineShoulder,
                    IsMirror
                    );

                // 肩　左
                kinectJointRotToUnityQuat(
                    joints[JointType.ShoulderLeft],
                    ref comp,
                    ref ShoulderLeft,
                    IsMirror
                    );

                // 肩　右
                kinectJointRotToUnityQuat(
                    joints[JointType.ShoulderRight],
                    ref comp,
                    ref ShoulderRight,
                    IsMirror
                    );

                // 肘　左
                kinectJointRotToUnityQuat(
                    joints[JointType.ElbowLeft],
                    ref comp,
                    ref ElbowLeft,
                    IsMirror
                    );

                // 肘　右
                kinectJointRotToUnityQuat(
                    joints[JointType.ElbowRight],
                    ref comp,
                    ref ElbowRight,
                    IsMirror
                    );

                // 手首？　左
                kinectJointRotToUnityQuat(
                    joints[JointType.WristLeft],
                    ref comp,
                    ref WristLeft,
                    IsMirror
                    );

                // 手首？　右
                kinectJointRotToUnityQuat(
                    joints[JointType.WristRight],
                    ref comp,
                    ref WristRight,
                    IsMirror
                    );

                // 掌　左
                kinectJointRotToUnityQuat(
                    joints[JointType.HandLeft],
                    ref comp,
                    ref HandLeft,
                    IsMirror
                    );

                // 掌　右
                kinectJointRotToUnityQuat(
                    joints[JointType.HandRight],
                    ref comp,
                    ref HandRight,
                    IsMirror
                    );

                // 膝　左
                kinectJointRotToUnityQuat(
                    joints[JointType.KneeLeft],
                    ref comp,
                    ref KneeLeft,
                    IsMirror
                    );

                // 膝　右
                kinectJointRotToUnityQuat(
                    joints[JointType.KneeRight],
                    ref comp,
                    ref KneeRight,
                    IsMirror
                    );

                // 足首　左
                kinectJointRotToUnityQuat(
                    joints[JointType.AnkleLeft],
                    ref comp,
                    ref AnkleLeft,
                    IsMirror
                    );

                // 足首　右
                kinectJointRotToUnityQuat(
                    joints[JointType.AnkleRight],
                    ref comp,
                    ref AnkleRight,
                    IsMirror
                    );

                // ミラーリングする場合は事前に関節の左右の位置情報を入れ替える。
                // そうしないと回転が正しくても動いた際にめり込んだり等、
                // 各部位がカオスなことになる。
                // 尚、この処理はキャラの向いている方向が判るなら不要となる（たぶん）。
                
                if (IsMirror)
                {
                    Swap<Quaternion>(ref ShoulderLeft, ref ShoulderRight);  // 肩
                    Swap<Quaternion>(ref ElbowLeft, ref ElbowRight);        // 肘
                    Swap<Quaternion>(ref WristLeft, ref WristRight);        // 手首
                    Swap<Quaternion>(ref HandLeft, ref HandLeft);           // 掌
                    Swap<Quaternion>(ref KneeLeft, ref KneeRight);          // 膝
                    Swap<Quaternion>(ref AnkleLeft, ref AnkleRight);        // 足首
                }
                
            }


            if (Input.GetKeyDown(KeyCode.Return))
            {
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.REFERENCE].transform.position = new Vector3(9999, 9999, 9999);
                avatarTypeOfPlayer[playerNum]++;
                avatarTypeOfPlayer[playerNum] %= AvatarDataMp.KIND_AVATARS;
            }

            // 関節の回転を計算する 
            q = transform.rotation;
            transform.rotation = Quaternion.identity;

            comp2 = Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) *
                                Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            compNeck = Quaternion.AngleAxis(180, new Vector3(0, 1, 0))
                        * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            compBody = Quaternion.AngleAxis(180, new Vector3(0, 1, 0))
                        * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            compRLeg = Quaternion.AngleAxis(-180, new Vector3(1, 0, 0));
            compRArm = Quaternion.AngleAxis(-90, new Vector3(1, 0, 0));
            compLArm = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));


            if (avatarData.isRigify[playerNum, avatarTypeOfPlayer[playerNum]] == false)
            {
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.SPINE1].transform.rotation = SpineMid * comp2;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_ARM].transform.rotation = ElbowRight * comp2;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_FORE_ARM].transform.rotation = WristRight * comp2;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_HAND].transform.rotation = HandRight * comp2;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_ARM].transform.rotation = ElbowLeft * comp2;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_FORE_ARM].transform.rotation = WristLeft * comp2;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_HAND].transform.rotation = HandLeft * comp2;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_UP_LEG].transform.rotation = KneeRight * comp2;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_LEG].transform.rotation = AnkleRight * comp2;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_UP_LEG].transform.rotation = KneeLeft * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_LEG].transform.rotation = AnkleLeft * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.HEAD].transform.rotation = Head * comp2;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.NECK].transform.rotation = Neck * comp2;

            }
            else
            {
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.SPINE1].transform.rotation = SpineMid * compBody;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_ARM].transform.rotation = ElbowRight * comp2 * compRArm;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_FORE_ARM].transform.rotation = WristRight * comp2 * compRArm;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_HAND].transform.rotation = HandRight * comp2 * compRArm;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_ARM].transform.rotation = ElbowLeft * comp2 * compLArm;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_FORE_ARM].transform.rotation = WristLeft * comp2 * compLArm;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_HAND].transform.rotation = HandLeft * comp2 * compLArm;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_UP_LEG].transform.rotation = KneeRight * comp2 * compRLeg;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.RIGHT_LEG].transform.rotation = AnkleRight * comp2 * compRLeg;

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_UP_LEG].transform.rotation = KneeLeft * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.LEFT_LEG].transform.rotation = AnkleLeft * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.HEAD].transform.rotation = Head * compNeck;
                avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.NECK].transform.rotation = Neck * compNeck;

            }

            // モデルの回転を設定する
            transform.rotation = q;

            // モデルの位置を移動する
            pos = bodys[playerNum].Joints[JointType.SpineMid].Position;
            avatarData.jointsData[playerNum, avatarTypeOfPlayer[playerNum]].Joints[(int)JointsDataMp.JOINTS_IDX.REFERENCE].transform.position = new Vector3(-pos.X * PosScale, pos.Y * PosScale, -pos.Z * PosScale);

        }
    }

}



