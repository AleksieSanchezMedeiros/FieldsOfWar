using Unity.VisualScripting;
using UnityEngine;

public class RouteLog : MonoBehaviour
{
    public static RouteLog Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    Transform[] topRoute, bottomRoute;

    // 0 advance 1 retreat 
    public Transform getNextWaypointInRoute(bool orderAdvance, Transform current, bool _isPlayer, bool _isOnTopTrack)
    {
        if (_isPlayer)
        {
            if (orderAdvance)
            {
                if (_isOnTopTrack)
                {
                    for (int i = 0; i < topRoute.Length; i++)
                    {
                        if (current == topRoute[i])
                        {
                            return topRoute[i++];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bottomRoute.Length; i++)
                    {
                        if (current == bottomRoute[i])
                        {
                            return bottomRoute[i++];
                        }
                    }
                }
            }
            else
            {
                if (_isOnTopTrack)
                {
                    for (int i = 0; i < topRoute.Length; i++)
                    {
                        if (current == topRoute[i])
                        {
                            return topRoute[i--];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bottomRoute.Length; i++)
                    {
                        if (current == bottomRoute[i])
                        {
                            return bottomRoute[i--];
                        }
                    }
                }
            }
        }
        else
        {
            if (orderAdvance)
            {
                if (_isOnTopTrack)
                {
                    for (int i = 0; i < topRoute.Length; i++)
                    {
                        if (current == topRoute[i])
                        {
                            return topRoute[i--];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bottomRoute.Length; i++)
                    {
                        if (current == bottomRoute[i])
                        {
                            return bottomRoute[i--];
                        }
                    }
                }
            }
            else
            {
                if (_isOnTopTrack)
                {
                    for (int i = 0; i < topRoute.Length; i++)
                    {
                        if (current == topRoute[i])
                        {
                            return topRoute[i++];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bottomRoute.Length; i++)
                    {
                        if (current == bottomRoute[i])
                        {
                            return bottomRoute[i++];
                        }
                    }
                }
            }
        }
        return null;
    }
}
