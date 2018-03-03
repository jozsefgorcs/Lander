using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// This component allows the definition of a level that can then be accessed and loaded. Used mostly in the level map scene.
	/// </summary>
	public class LevelSelector : MonoBehaviour
	{
		/// the exact name of the target level
	    public string LevelName;

		/// <summary>
		/// Loads the level specified in the inspector
		/// </summary>
	    public virtual void GoToLevel()
	    {
	        LevelManager.Instance.GotoLevel(LevelName);
	    }

		/// <summary>
		/// Restarts the current level
		/// </summary>
	    public virtual void RestartLevel()
	    {
	        GameManager.Instance.UnPause();
			LoadingSceneManager.LoadScene(SceneManager.GetActiveScene().name);
	    }
		
	}
}