using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

public class AuthenticationManager : Singleton<AuthenticationManager> 
{
    protected async override void OnAwake()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.Expired += OnExpiredSession;
        AuthenticationService.Instance.SignedIn += OnSignedIn;
        AuthenticationService.Instance.SignedOut += OnSignedOut;
    }

    private void OnDestroy()
    {
        AuthenticationService.Instance.Expired -= OnExpiredSession;
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        AuthenticationService.Instance.SignedOut -= OnSignedOut;
    }


    private void Start()
    {
        ContinueAnonymously();
    }

    public async void ContinueAnonymously()
    {
        try
        {
            Debug.Log("Singing in anonymously...");
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
        }
    }

    public void SignOut()
    {
        try
        {
           AuthenticationService.Instance.SignOut();
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
        }
    }

    private void OnSignedIn()
    {
        SceneManager.LoadScene(SceneNames.MENU_SCENE);
        Debug.Log("Signed in!");
    }

    private void OnSignedOut()
    {
        Debug.Log("Signed out!");
    }

    private void OnExpiredSession()
    {
        SceneManager.LoadScene(SceneNames.AUTHORIZATION_SCENE);
        Debug.LogWarning("Session expired");
    }
}
