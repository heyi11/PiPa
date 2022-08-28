#if UNITY_ANDROID && ! UNITY_EDITOR
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkCommunicationSettings : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkCommunicationSettings(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkCommunicationSettings obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkCommunicationSettings() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkCommunicationSettings(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public AkCommunicationSettings() : this(AkSoundEnginePINVOKE.CSharp_new_AkCommunicationSettings(), true) {
  }

  public uint uPoolSize { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uPoolSize_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uPoolSize_get(swigCPtr); } 
  }

  public ushort uDiscoveryBroadcastPort { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uDiscoveryBroadcastPort_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uDiscoveryBroadcastPort_get(swigCPtr); } 
  }

  public ushort uCommandPort { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uCommandPort_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uCommandPort_get(swigCPtr); } 
  }

  public ushort uNotificationPort { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uNotificationPort_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_uNotificationPort_get(swigCPtr); } 
  }

  public AkCommunicationSettings.AkCommSystem commSystem { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_commSystem_set(swigCPtr, (int)value); }  get { return (AkCommunicationSettings.AkCommSystem)AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_commSystem_get(swigCPtr); } 
  }

  public bool bInitSystemLib { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_bInitSystemLib_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_bInitSystemLib_get(swigCPtr); } 
  }

  public string szAppNetworkName { set { AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_szAppNetworkName_set(swigCPtr, value); }  get { return AkSoundEngine.StringFromIntPtrString(AkSoundEnginePINVOKE.CSharp_AkCommunicationSettings_szAppNetworkName_get(swigCPtr)); } 
  }

  public enum AkCommSystem {
    AkCommSystem_Socket,
    AkCommSystem_HTCS
  }

}
#endif // #if UNITY_ANDROID && ! UNITY_EDITOR