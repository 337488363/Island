using System.Collections;
using UnityEngine;
using Ximmerse.RhinoX;
using PolyEngine.Reflection;
using System.Reflection;
using Common;
using UnityEngine.Events;

namespace PoliceTrainingEditor.InputSystem
{
    public enum GunButtons
    {
        Trigger = 0x0020,

        Function = 0x00010,

        Power = 0x0008,

        Load = 0x0004,

        Release = 0x0002,

        Push = 0x0001,
    }

    [System.Serializable]
    public class GunButtonEvent : UnityEvent<GunButtons>
    {

    }
#if UNITY_EDITOR || UNITY_ANDROID
    /// <summary>
    /// Gun input.
    /// Override the RX input UI system.
    /// </summary>
    public class GunInput : MonoSingleton<GunInput>, I_ExternalInputProvider
    {
        public bool printButtonLog = true;

        public bool OverrideRaycaster = true;
        public GunButtonEvent OnClickFunction = new GunButtonEvent();
        public GunButtonEvent OnDoubleClickFunction = new GunButtonEvent();
        public GunButtonEvent OnLongHeldFunction = new GunButtonEvent();
        public GunButtonEvent OnClickTrigger = new GunButtonEvent();

        public GunButtonEvent OnClickPower = new GunButtonEvent();
        public GunButtonEvent OnClickLoad = new GunButtonEvent();
        public GunButtonEvent OnClickRelease = new GunButtonEvent();
        public GunButtonEvent OnClickPush = new GunButtonEvent();

        public void tTrigger()
        {
            OnClickTrigger?.Invoke( GunButtons.Trigger );
        }
        public GunButtonEvent OnDoubleClickTrigger = new GunButtonEvent();
        public GunButtonEvent OnLongHeldTrigger = new GunButtonEvent();

        GunButtons[] allButtons = null;

        System.Type m_TypeXimmerseInputSystem;
        System.Type typeXimmerseInputSystem
        {
            get
            {
                if (m_TypeXimmerseInputSystem == null)
                {
                    m_TypeXimmerseInputSystem = PEReflectionUtility.SearchTypeByName("Ximmerse.RhinoX.XimmerseControllerSystem");
                }
                return m_TypeXimmerseInputSystem;
            }
        }

        System.Object m_ControllerWrapper = null;

        System.Object ControllerWrapper
        {
            get
            {
                if (m_ControllerWrapper == null)
                {
                    var ControllerWrapObj = typeXimmerseInputSystem.GetMethod("GetController", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Invoke(null, new object[] { ControllerIndex.Controller01 });

                    if (ControllerWrapObj != null)
                    {
                        m_ControllerWrapper = ControllerWrapObj;
                        
                    }
                    else
                    {
                        
                    }
                }

                return m_ControllerWrapper;
            }
        }
        MethodInfo m_MethodGetControllerButtonState;
        MethodInfo methodGetControllerButtonState
        {
            get
            {
                if (m_MethodGetControllerButtonState == null)
                {
                    if (ControllerWrapper != null)
                    {
                        m_MethodGetControllerButtonState = ControllerWrapper.GetType().GetMethod("GetButtonState", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                return m_MethodGetControllerButtonState;
            }
        }

        MethodInfo m_MethodUnpairAll;
        MethodInfo methodUnpairAll
        {
            get
            {
                if (m_MethodUnpairAll == null)
                {
                    m_MethodUnpairAll = typeXimmerseInputSystem.GetMethod("UnpairAll", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                }
                return m_MethodUnpairAll;
            }
        }


        MethodInfo m_MethodStartPairingByRFID;
        MethodInfo methodStartPairingByRFID
        {
            get
            {
                if (m_MethodStartPairingByRFID == null)
                {
                    m_MethodStartPairingByRFID = typeXimmerseInputSystem.GetMethod("StartPairingByRFID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                }
                return m_MethodStartPairingByRFID;
            }
        }

        FieldInfo fieldIsUp = null, fieldIsDown = null, fieldIsPressed = null, fieldIsTap = null, fieldIsDoubleTap = null, fieldIsLongHeld = null, fieldTriggerValue = null;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is trigger down.
        /// </summary>
        /// <value><c>true</c> if is trigger down; otherwise, <c>false</c>.</value>
        public bool IsTriggerDown
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is trigger up.
        /// </summary>
        /// <value><c>true</c> if is trigger up; otherwise, <c>false</c>.</value>
        public bool IsTriggerUp
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is trigger pressing.
        /// </summary>
        /// <value><c>true</c> if is trigger pressing; otherwise, <c>false</c>.</value>
        public bool IsTriggerPressing
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is trigger click.
        /// </summary>
        /// <value><c>true</c> if is trigger click; otherwise, <c>false</c>.</value>
        public bool IsTriggerClick
        {
            get; private set;
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is function key down.
        /// </summary>
        /// <value><c>true</c> if is trigger down; otherwise, <c>false</c>.</value>
        public bool IsFunctionKeyDown
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is function key up.
        /// </summary>
        /// <value><c>true</c> if is trigger up; otherwise, <c>false</c>.</value>
        public bool IsFunctionKeyUp
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is function key pressing.
        /// </summary>
        /// <value><c>true</c> if is trigger pressing; otherwise, <c>false</c>.</value>
        public bool IsFunctionKeyPressing
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:PolyEngine.IPSC.GunInput"/> is function key click.
        /// </summary>
        /// <value><c>true</c> if is trigger click; otherwise, <c>false</c>.</value>
        public bool IsFunctionKeyClick
        {
            get; private set;
        }

        public bool IsPowerKeyDown { get; private set; }
        public bool IsPowerKeyUp { get; private set; }
        public bool IsPowerKeyPressing { get; private set; }
        public bool IsPowerKeyClick { get; private set; }

        public bool IsLoadKeyDown { get; private set; }
        public bool IsLoadKeyUp { get; private set; }
        public bool IsLoadKeyPressing { get; private set; }
        public bool IsLoadKeyClick { get; private set; }

        public bool IsReleaseKeyDown { get; private set; }
        public bool IsReleaseKeyUp { get; private set; }
        public bool IsReleaseKeyPressing { get; private set; }
        public bool IsReleaseKeyClick { get; private set; }

        public bool IsPushKeyDown { get; private set; }
        public bool IsPushKeyUp { get; private set; }
        public bool IsPushKeyPressing { get; private set; }
        public bool IsPushKeyClick { get; private set; }


        public bool InputDown => IsTriggerDown;

        public bool InputUp => IsTriggerUp;

        public bool InputClick => IsTriggerClick;

        public bool Input => IsTriggerPressing;

        private bool IsTap(Ximmerse.RhinoX.Internal.ControllerButton rawBtn)
        {
            if (ControllerWrapper != null && methodGetControllerButtonState != null)
            {
                var buttonState = methodGetControllerButtonState.Invoke(ControllerWrapper, new object[] { rawBtn });
                if (fieldIsTap == null)
                {
                    fieldIsTap = buttonState.GetType().GetField("isTap", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
                var isTap = (bool)fieldIsTap.GetValue(buttonState);
                return isTap;
            }
            else
            {
                return false;
            }
        }

        private bool IsDown(Ximmerse.RhinoX.Internal.ControllerButton rawBtn)
        {
            if (ControllerWrapper != null && methodGetControllerButtonState != null)
            {
                var buttonState = methodGetControllerButtonState.Invoke(ControllerWrapper, new object[] { rawBtn });
                if (fieldIsDown == null)
                {
                    fieldIsDown = buttonState.GetType().GetField("isDown", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
                var isDown = (bool)fieldIsDown.GetValue(buttonState);
                return isDown;
            }
            else
            {
                return false;
            }
        }

        private bool IsUp(Ximmerse.RhinoX.Internal.ControllerButton rawBtn)
        {
            if (ControllerWrapper != null && methodGetControllerButtonState != null)
            {
                var buttonState = methodGetControllerButtonState.Invoke(ControllerWrapper, new object[] { rawBtn });
                if (fieldIsUp == null)
                {
                    fieldIsUp = buttonState.GetType().GetField("isUp", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
                var isUp = (bool)fieldIsUp.GetValue(buttonState);
                return isUp;
            }
            else
            {
                return false;
            }
        }

        private bool IsPressed(Ximmerse.RhinoX.Internal.ControllerButton rawBtn)
        {
            if (ControllerWrapper != null && methodGetControllerButtonState != null)
            {
                var buttonState = methodGetControllerButtonState.Invoke(ControllerWrapper, new object[] { rawBtn });
                if (fieldIsPressed == null)
                {
                    fieldIsPressed = buttonState.GetType().GetField("isPressed", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
                var isPressed = (bool)fieldIsPressed.GetValue(buttonState);
                return isPressed;
            }
            else
            {
                return false;
            }
        }

        private bool IsDoubleClick (Ximmerse.RhinoX.Internal.ControllerButton rawBtn)
        {
            if (ControllerWrapper != null && methodGetControllerButtonState != null)
            {
                var buttonState = methodGetControllerButtonState.Invoke(ControllerWrapper, new object[] { rawBtn });
                if (fieldIsDoubleTap == null)
                {
                    fieldIsDoubleTap = buttonState.GetType().GetField("isDoubleTap", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
                var isDoubleTap = (bool)fieldIsDoubleTap.GetValue(buttonState);
                return isDoubleTap;
            }
            else
            {
                return false;
            }
        }

        private bool IsLongHeld(Ximmerse.RhinoX.Internal.ControllerButton rawBtn)
        {
            if (ControllerWrapper != null && methodGetControllerButtonState != null)
            {
                var buttonState = methodGetControllerButtonState.Invoke(ControllerWrapper, new object[] { rawBtn });
                if (fieldIsLongHeld == null)
                {
                    fieldIsLongHeld = buttonState.GetType().GetField("isLongHeldDown", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
                var isLongHeld = fieldIsLongHeld != null && (bool)fieldIsLongHeld.GetValue(buttonState);
                return isLongHeld;
            }
            else
            {
                return false;
            }
        }

        MethodInfo methodGetTriggerValue;
        private int GetTriggerValue()
        {
            if (ControllerWrapper != null)
            {
                if (methodGetTriggerValue == null)
                {
                    //fieldTriggerValue = ControllerWrapper.GetType().GetField("Trigger", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    methodGetTriggerValue = ControllerWrapper.GetType().GetMethod("GetTriggerValue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    //Debug.Log($"===========methodGetTriggerValue: {methodGetTriggerValue == null}===========");
                }
                if (methodGetTriggerValue != null)
                {
                    var value = (int)methodGetTriggerValue.Invoke(ControllerWrapper, null);
                    return value;
                }
            }
            return 0; 
        }

        private void OnApplicationPause(bool focus)
        {
            if (focus)
            {
                Debug.Log("===========Application Pause Return==============");//Edited by jinli
                m_ControllerWrapper = null;
                methodGetTriggerValue = null;
            }
            else
            {
                Debug.Log("===========Application Pause==============");
            }
        }

        protected override void Init()
        {
            base.Init();
            allButtons = new GunButtons[]
            {
                GunButtons.Trigger,
                GunButtons.Function,
                GunButtons.Load,
                GunButtons.Power,
                GunButtons.Push,
                GunButtons.Release
            };
        }

        void OnEnable()
        {
            StartCoroutine(InputDaemon());
            if(OverrideRaycaster)
            {
                var RXInputModule = FindObjectOfType<RXInputModule>();
                if (RXInputModule)
                {
                    RXInputModule.externalInputProvider = this;
                    Debug.Log("RX Input module override !");
                }
                else
                {
                    Debug.Log("RX Input module not found !");
                }
            }
        }


        IEnumerator InputDaemon()
        {
            WaitForEndOfFrame eof = new WaitForEndOfFrame();
            while (this.enabled && gameObject.activeInHierarchy)
            {
                yield return eof;
                IsTriggerDown = IsTriggerUp = IsTriggerPressing = IsTriggerClick = IsFunctionKeyUp = IsFunctionKeyDown = IsFunctionKeyClick = IsFunctionKeyPressing = false;
                IsPowerKeyDown = IsPowerKeyUp = IsPowerKeyPressing = IsPushKeyClick = IsLoadKeyUp = IsLoadKeyDown = IsLoadKeyClick = IsLoadKeyPressing = false;
                IsReleaseKeyDown = IsReleaseKeyUp = IsReleaseKeyPressing = IsReleaseKeyClick = IsPushKeyUp = IsPushKeyDown = IsPushKeyClick = IsPushKeyPressing = false;
            }
        }

        public void UpdateInput()
        {
            if(gameObject.activeInHierarchy)
               ProcessInput();
        }

        private bool IsTriggerReleased = true;
        private bool IsTriggerPressed;
        private void ProcessInput()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (ControllerWrapper != null)
                {
                    foreach (var btn in allButtons)
                    {
                        if (IsUp((Ximmerse.RhinoX.Internal.ControllerButton)((int)btn)))
                        {
                            //if (btn == GunButtons.Trigger)
                            //{
                            //    IsTriggerUp = true;
                            //}

                            if (btn == GunButtons.Function)
                            {
                                IsFunctionKeyUp = true;
                            }

                            if (btn == GunButtons.Power)
                            {
                                IsPowerKeyUp = true;
                            }

                            if (btn == GunButtons.Load)
                            {
                                IsLoadKeyUp = true;
                            }

                            if (btn == GunButtons.Release)
                            {
                                IsReleaseKeyUp = true;
                            }

                            if (btn == GunButtons.Push)
                            {
                                IsPushKeyUp = true;
                            }
                        }

                        if (IsDown((Ximmerse.RhinoX.Internal.ControllerButton)((int)btn)))
                        {
                            //if (btn == GunButtons.Trigger)
                            //{
                            //    IsTriggerDown = true;
                            //}

                            if (btn == GunButtons.Function)
                            {
                                IsFunctionKeyDown = true;
                            }

                            if (btn == GunButtons.Power)
                            {
                                IsPowerKeyDown = true;
                            }

                            if (btn == GunButtons.Load)
                            {
                                IsLoadKeyDown = true;
                            }

                            if (btn == GunButtons.Release)
                            {
                                IsReleaseKeyDown = true;
                            }

                            if (btn == GunButtons.Push)
                            {
                                IsPushKeyDown = true;
                            }
                        }

                        if (IsPressed((Ximmerse.RhinoX.Internal.ControllerButton)((int)btn)))
                        {
                            //if (btn == GunButtons.Trigger)
                            //{
                            //    IsTriggerPressing = true;
                            //}

                            if (btn == GunButtons.Function)
                            {
                                IsFunctionKeyPressing = true;
                            }

                            if (btn == GunButtons.Power)
                            {
                                IsPowerKeyPressing = true;
                            }

                            if (btn == GunButtons.Load)
                            {
                                IsLoadKeyPressing = true;
                            }

                            if (btn == GunButtons.Release)
                            {
                                IsReleaseKeyPressing = true;
                            }

                            if (btn == GunButtons.Push)
                            {
                                IsPushKeyPressing = true;
                            }
                        }

                        if (IsTap((Ximmerse.RhinoX.Internal.ControllerButton)((int)btn)))
                        {
                            if (printButtonLog)
                                Debug.LogFormat("Tap on gun button : {0}", btn);

                            //if (btn == GunButtons.Trigger)
                            //{
                            //    IsTriggerClick = true;
                            //    OnClickTrigger.Invoke(btn);
                            //}

                            if (btn == GunButtons.Function)
                            {
                                IsFunctionKeyClick = true;
                                OnClickFunction.Invoke(btn);
                            }

                            if (btn == GunButtons.Power)
                            {
                                IsPowerKeyClick = true;
                                OnClickPower.Invoke(btn);
                            }

                            if (btn == GunButtons.Load)
                            {
                                IsLoadKeyClick = true;
                                OnClickLoad.Invoke(btn);
                            }

                            if (btn == GunButtons.Release)
                            {
                                IsReleaseKeyClick = true;
                                OnClickRelease.Invoke(btn);
                            }

                            if (btn == GunButtons.Push)
                            {
                                IsPushKeyClick = true;
                                OnClickPush.Invoke(btn);
                            }
                        }

                        if (IsDoubleClick((Ximmerse.RhinoX.Internal.ControllerButton)((int)btn)))
                        {
                            if (printButtonLog)
                                Debug.LogFormat("Double-Tap on gun button : {0}", btn);

                            if (btn == GunButtons.Trigger)
                            {
                                OnDoubleClickTrigger.Invoke(btn);
                            }

                            if (btn == GunButtons.Function)
                            {
                                OnDoubleClickFunction.Invoke(btn);
                            }
                        }

                        if (IsLongHeld((Ximmerse.RhinoX.Internal.ControllerButton)((int)btn)))
                        {
                            if (printButtonLog)
                                Debug.LogFormat("Long-Held on gun button : {0}", btn);

                            if (btn == GunButtons.Trigger)
                            {
                                OnLongHeldTrigger.Invoke(btn);
                            }

                            if (btn == GunButtons.Function)
                            {
                                OnLongHeldFunction.Invoke(btn);
                            }
                        }
                    }

                    var triggerValue = GetTriggerValue();
                    //if(triggerValue > 0)
                        //Debug.Log($"===========Trigger value: {triggerValue}===========");
                    if(triggerValue < 30)
                    {
                        if (IsTriggerPressed)
                        {
                            IsTriggerUp = true;
                            IsTriggerClick = true;
                            IsTriggerReleased = true;
                            IsTriggerPressed = false;
                            OnClickTrigger?.Invoke(GunButtons.Trigger);
                            
                        }
                    }
                    else if(triggerValue >= 30 && triggerValue < 200)
                    {
                        //IsTriggerReleased = false;
                        IsTriggerPressing = true;
                    }
                    else
                    {
                        if (IsTriggerReleased && !IsTriggerPressed)
                        {
                            IsTriggerDown = true;
                            IsTriggerReleased = false;
                        }
                        IsTriggerPressed = IsTriggerPressing = true;
                    }
                }
            }
        }

        Ximmerse.RhinoX.Internal.XDevicePlugin.XController internalController = null;

        /// <summary>
        /// Gets controlelr 6dof from LLSDK
        /// </summary>
        /// <param name="SixDof"></param>
        /// <returns></returns>
        public bool GetController6Dof(out Ximmerse.RhinoX.Internal.XDevicePlugin.XAttr6DofInfo SixDof)
        {
            SixDof = default;
            if (internalController == null)
            {
                if (ControllerWrapper != null)
                {
                    internalController = ControllerWrapper.GetFieldValue<Ximmerse.RhinoX.Internal.XDevicePlugin.XController>("ctrlControllerHandle");
                }
            }

            if (internalController == null)
            {
                return false;
            }
            var errCode = internalController.GetSixDof(ref SixDof);
            return true;
        }
    }

#endif

}
