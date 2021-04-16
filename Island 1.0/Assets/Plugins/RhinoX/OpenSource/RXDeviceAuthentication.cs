using UnityEngine;
using System.Collections;
using Ximmerse.RhinoX.Internal;

namespace Ximmerse.RhinoX
{
    /// <summary>
    /// RhinoX device authentication interface. 
    /// How to use :
    /// 1. Put a RXDeviceAuthentication component at any game object in your scene.
    /// 2. Input your AppID, developer Id, developer Key.
    /// 3. Call GetSdkAuthorizationAvailableSeconds() to get your app's remain time.
    /// </summary>
    public class RXDeviceAuthentication : MonoBehaviour, I_Authentication
    {
        [SerializeField]
        string m_AppID = "10";

        [SerializeField]
        string m_DeveloperID = ""; 

        [SerializeField]
        string m_DeveloperKey = "";

        /// <summary>
        /// Gets or sets the config app identifier.
        /// </summary>
        /// <value>The config app identifier.</value>
        public string ConfigAppID { get { return m_AppID; } set { m_AppID = value; } }

        /// <summary>
        /// Gets or sets the config developer identifier.
        /// </summary>
        /// <value>The config developer identifier.</value>
        public string ConfigDeveloperID { get { return m_DeveloperID; } set { m_DeveloperID = value; } }

        /// <summary>
        /// Gets or sets the config developer key.
        /// </summary>
        /// <value>The config developer key.</value>
        public string ConfigDeveloperKey { get { return m_DeveloperKey; } set { m_DeveloperKey = value; } }


        private void Awake()
        {
            typeof(RhinoXSystem)
            .GetProperty("authenticationImpl", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
            .SetMethod.Invoke(null, new object[] { this });
        }

        IEnumerator Start()
        {
            while(!ARCamera.Instance.IsARBegan)
            {
                yield return null;
            }
            int time = Authorize();
            Debug.LogFormat("Authroize() return time: {0}", time);
        }

        public int Authorize()
        {
            int time = XDevicePlugin.Authorize();
            //Debug.LogFormat("Authorize return time : {0}", availableTime01);
            return time;
        }

        public int GetSdkAuthorizationAvailableSeconds()
        {
            int remain = XDevicePlugin.GetSdkAuthorizationAvailableSeconds();
            //Debug.LogFormat("SdkAuthorizationAvailableSeconds : {0}", remain);
            return remain;
        }

    }
}