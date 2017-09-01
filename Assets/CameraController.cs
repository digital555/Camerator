using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Threading;

public class CameraController : MonoBehaviour
{
    public WebCamTexture mCamera = null;
    public GameObject plane;
    public Color[] pix;
    public byte[] bytes;
    public Texture2D snap;
    public Image image;
    public UdpClient __doge1VideoUPDSender;
    public Button videoSendStart;
    public InputField IP;
    private Thread getAndSendThread;
    private bool flag1 = true;
    private bool flag2 = false;




    // Use this for initialization
    void Start()
    {
        __doge1VideoUPDSender = new UdpClient();
 
        Debug.Log("Script has been started");

        mCamera = new WebCamTexture(320, 240, 15);

        plane.GetComponent<Renderer>().material.mainTexture = mCamera;
        mCamera.Play();
        snap = new Texture2D(mCamera.width, mCamera.height);

        videoSendStart.onClick.AddListener(getAndSendStarter);

        IP.text = "10.3.2.133";
    }

    // Update is called once per frame
    void Update()
    {
        getAndSend();
    }

    void getAndSendStarter()
    {
        Debug.Log("C");
        __doge1VideoUPDSender.Connect(IP.text, 11009);
        flag2 = true;
        //__doge1VideoUPDSender.Connect("10.3.2.5", 12010);
        //__doge1VideoUPDSender.Connect("10.3.2.133", 11009);
        //__doge1VideoUPDSender.Connect("192.168.8.100", 12010);
        Debug.Log(IP.text);
    }

    void getAndSend()
    {
        Debug.Log(flag1);

            if (flag2)
            {
                Debug.Log("B");
                pix = mCamera.GetPixels();

                snap.SetPixels(pix);
                snap.Apply();
                bytes = snap.EncodeToJPG();

                if (bytes.Length < 63000)
                    __doge1VideoUPDSender.Send(bytes, bytes.Length);

                Debug.Log(bytes.Length);
            }
        
    }
}

