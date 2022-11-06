using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField] private float ParallaxStrenght;
    Vector3 TargerPosition;

    private void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        TargerPosition = followingTarget.position;
    }

    //плавна зміна фону
    private void LateUpdate()
    {
        var deltaVector3 = followingTarget.position - TargerPosition;

        TargerPosition = followingTarget.position;

        transform.position += deltaVector3 * ParallaxStrenght;
    }
}
