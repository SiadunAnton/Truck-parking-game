using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool _loadOnStart = true;
    [SerializeField] private AssetLabelReference _label;
    [SerializeField] private List<IResourceLocation> _locations = new List<IResourceLocation>();

    private AsyncOperationHandle<IList<IResourceLocation>> _lastHandle;
    private int _index = 0;

    private void Start()
    {
        _lastHandle = Addressables.LoadResourceLocationsAsync(_label);
        _lastHandle.Completed += x =>
        {
            if (x.Status == AsyncOperationStatus.Succeeded)
            {
                _locations = x.Result.ToList();
                _lastHandle = x;
            }
            if(_loadOnStart)
                LoadCurrent();
        };
    }

    public void LoadNext()
    {
        _index++;
        if (_index == _locations.Count)
            _index = 0;
        Load(_index);
    }

    private void Load(int index)
    {
        try
        {
            Addressables.LoadSceneAsync(_locations[index], LoadSceneMode.Single);
        }
        catch
        {
            Debug.Log("Can't load scene.");
        }
    }

    public void LoadCurrent()
    {
        Load(_index);
    }

    public void LoadAdditive(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new System.ArgumentException("Address isn't set.");

        Addressables.LoadSceneAsync(address, LoadSceneMode.Additive);
    }

    public void CloseLastScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
    }

    private void OnDestroy()
    {
        Addressables.ReleaseInstance(_lastHandle);
    }
}