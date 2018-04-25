using UnityEngine;

public abstract class Singleton<T> where T : class, new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = new T();

            return instance;
        }
    }
}

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
	private static T instance = null;

	public static T Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<T> ();
				if (instance == null) 
				{
					var obj = new GameObject (typeof(T).ToString());
					instance = obj.AddComponent<T> ();
				}
			}
			return instance;
		}
	}
}