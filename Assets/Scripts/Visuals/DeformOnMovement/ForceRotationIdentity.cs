using UnityEngine;

// Forces the sprite to not rotate
public class ForceRotationIdentity : MonoBehaviour
{
	private void Update ()
	{
		transform.rotation = Quaternion.identity;
	}
}