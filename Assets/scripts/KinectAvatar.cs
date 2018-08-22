using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

public class KinectAvatar : MonoBehaviour
{
    [SerializeField]
    BodySourceManager bodySourceManager;

    //自分の関節とUnityちゃんのボーンを入れるよう
    [SerializeField] GameObject Ref;

    [SerializeField] GameObject GoHead;
    [SerializeField] GameObject GoNeck;

    [SerializeField] GameObject LeftUpLeg;
    [SerializeField] GameObject LeftLeg;
    [SerializeField] GameObject RightUpLeg;
    [SerializeField] GameObject RightLeg;
    [SerializeField] GameObject Spine1;
    [SerializeField] GameObject LeftArm;
    [SerializeField] GameObject LeftForeArm;
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject RightArm;
    [SerializeField] GameObject RightForeArm;
    [SerializeField] GameObject RightHand;
    [SerializeField] int PosScale;

    [SerializeField] bool IsMirror;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //最初に追尾している人のBodyデータを取得する
        Body body = bodySourceManager.GetData().FirstOrDefault(b => b.IsTracked);

        // Kinectを斜めに置いてもまっすぐにするようにする
        var floorPlane = bodySourceManager.FloorClipPlane;
        Quaternion comp = Quaternion.FromToRotation(
            new Vector3(-floorPlane.X, floorPlane.Y, floorPlane.Z), Vector3.up);

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

        Quaternion q;
        Quaternion comp2;
        CameraSpacePoint pos;

        Quaternion debug = new Quaternion();

        // 関節の回転を取得する
        if (body != null)
        {
            var joints = body.JointOrientations;

            //Kinectの関節回転情報をUnityのクォータニオンに変換
            if (IsMirror)
            {
                

                SpineBase =
                  joints[JointType.SpineBase].Orientation.ToMirror().ToQuaternion(comp);
                SpineMid =
                  joints[JointType.SpineMid].Orientation.ToMirror().ToQuaternion(comp);

                Head =
                  joints[JointType.Head].Orientation.ToMirror().ToQuaternion(comp);
                  //joints[JointType.ElbowLeft].Orientation.ToMirror().ToQuaternion(comp);
                Neck =
                  //joints[JointType.HandLeft].Orientation.ToMirror().ToQuaternion(comp);
                  joints[JointType.Neck].Orientation.ToMirror().ToQuaternion(comp);

                SpineShoulder =
                  joints[JointType.SpineShoulder].Orientation.ToMirror().ToQuaternion(comp);
                ShoulderLeft =
                  joints[JointType.ShoulderRight].Orientation.ToMirror().ToQuaternion(comp);
                ShoulderRight =
                  joints[JointType.ShoulderLeft].Orientation.ToMirror().ToQuaternion(comp);
                ElbowLeft =
                  joints[JointType.ElbowRight].Orientation.ToMirror().ToQuaternion(comp);
                WristLeft =
                  joints[JointType.WristRight].Orientation.ToMirror().ToQuaternion(comp);
                HandLeft =
                  joints[JointType.HandRight].Orientation.ToMirror().ToQuaternion(comp);
                ElbowRight =
                  joints[JointType.ElbowLeft].Orientation.ToMirror().ToQuaternion(comp);
                WristRight =
                  joints[JointType.WristLeft].Orientation.ToMirror().ToQuaternion(comp);
                HandRight =
                  joints[JointType.HandLeft].Orientation.ToMirror().ToQuaternion(comp);
                KneeLeft =
                  joints[JointType.KneeRight].Orientation.ToMirror().ToQuaternion(comp);
                AnkleLeft =
                  joints[JointType.AnkleRight].Orientation.ToMirror().ToQuaternion(comp);
                KneeRight =
                  joints[JointType.KneeLeft].Orientation.ToMirror().ToQuaternion(comp);
                AnkleRight =
                  joints[JointType.AnkleLeft].Orientation.ToMirror().ToQuaternion(comp);
                
            }
            // そのまま
            else
            {
                SpineBase =
                  joints[JointType.SpineBase].Orientation.ToQuaternion(comp);
                SpineMid =
                  joints[JointType.SpineMid].Orientation.ToQuaternion(comp);

                Head =
                  joints[JointType.Head].Orientation.ToQuaternion(comp);
                Neck =
                  joints[JointType.Neck].Orientation.ToQuaternion(comp);

                SpineShoulder =
                  joints[JointType.SpineShoulder].Orientation.ToQuaternion(comp);
                ShoulderLeft =
                  joints[JointType.ShoulderLeft].Orientation.ToQuaternion(comp);
                ShoulderRight =
                  joints[JointType.ShoulderRight].Orientation.ToQuaternion(comp);
                ElbowLeft =
                  joints[JointType.ElbowLeft].Orientation.ToQuaternion(comp);
                WristLeft =
                  joints[JointType.WristLeft].Orientation.ToQuaternion(comp);
                HandLeft =
                  joints[JointType.HandLeft].Orientation.ToQuaternion(comp);
                ElbowRight =
                  joints[JointType.ElbowRight].Orientation.ToQuaternion(comp);
                WristRight =
                  joints[JointType.WristRight].Orientation.ToQuaternion(comp);
                HandRight =
                  joints[JointType.HandRight].Orientation.ToQuaternion(comp);
                KneeLeft =
                  joints[JointType.KneeLeft].Orientation.ToQuaternion(comp);
                AnkleLeft =
                  joints[JointType.AnkleLeft].Orientation.ToQuaternion(comp);
                KneeRight =
                  joints[JointType.KneeRight].Orientation.ToQuaternion(comp);
                AnkleRight =
                  joints[JointType.AnkleRight].Orientation.ToQuaternion(comp);
                  
            }

            //debug----------------------------------------------------------
            //SpineBase = debug;
            //SpineMid = debug;

            //Head = debug;
            //Neck = debug;

            //SpineShoulder = debug;

            //ShoulderLeft = debug;
            //ShoulderRight = debug;

            //ElbowLeft = debug;
            //WristLeft = debug;
            //HandLeft = debug;

            //ElbowRight = debug;
            //WristRight = debug;
            //HandRight = debug;

            //KneeLeft = debug;
            //AnkleLeft = debug;

            //KneeRight = debug;
            //AnkleRight = debug;
            //_debug---------------------------------------------------------

            print("ElbowLeft x:" + ElbowLeft.x + " y" + ElbowLeft.y + " z:" + ElbowLeft.z + " w:" + ElbowLeft.w);
            print("Neck x:" + Neck.x + " y" + Neck.y + " z:" + Neck.z + " w:" + Neck.w);

            // 関節の回転を計算する 
            q = transform.rotation;
            transform.rotation = Quaternion.identity;

            comp2 = Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) *
                             Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            Spine1.transform.rotation = SpineMid * comp2;

            RightArm.transform.rotation = ElbowRight * comp2;
            RightForeArm.transform.rotation = WristRight * comp2;
            RightHand.transform.rotation = HandRight * comp2;

            LeftArm.transform.rotation = ElbowLeft * comp2;
            LeftForeArm.transform.rotation = WristLeft * comp2;
            LeftHand.transform.rotation = HandLeft * comp2;

            RightUpLeg.transform.rotation = KneeRight * comp2;
            RightLeg.transform.rotation = AnkleRight * comp2;

            LeftUpLeg.transform.rotation = KneeLeft *
                            Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            LeftLeg.transform.rotation = AnkleLeft *
                            Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            GoHead.transform.rotation = Head * comp2;
            GoNeck.transform.rotation = Neck * comp2;
            // モデルの回転を設定する
            transform.rotation = q;

            // モデルの位置を移動する
            pos = body.Joints[JointType.SpineMid].Position;
            print("(" + pos.X + "," + pos.Y + "," + pos.Z + ")");
            Ref.transform.position = new Vector3(-pos.X*PosScale, pos.Y*PosScale, -pos.Z*PosScale);
            
        }
    }
}



