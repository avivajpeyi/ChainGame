using UnityEngine;

using UnityEngine.PostProcessing;

public class VignettePulse : MonoBehaviour
{ 
    PostProcessingProfile ppProfile;
    VignetteModel.Settings m_Vignette;
    BloomModel.Settings m_Bloom;
    
    void Start()
    {
        ppProfile = GetComponent<PostProcessingBehaviour>().profile;
        
        m_Vignette = ppProfile.vignette.settings;
        m_Vignette.intensity = 1f;

        m_Bloom.bloom.radius = 5;
    }

    void Update()
    {
        m_Vignette.intensity = Mathf.Sin(Time.realtimeSinceStartup);
        m_Bloom.bloom.radius =  Mathf.Sin(Time.realtimeSinceStartup) * 5;
    }

}


 
