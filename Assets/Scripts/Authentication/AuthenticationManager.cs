using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

public class AuthenticationManager
{
    public AuthenticationManager()
    {
        AuthenticationService.Instance.Expired += OnExpiredSession;
        AuthenticationService.Instance.SignedIn += OnSignedIn;
        AuthenticationService.Instance.SignedOut += OnSignedOut;

        ContinueAnonymously();
    }


    ~AuthenticationManager()
    {
        AuthenticationService.Instance.Expired -= OnExpiredSession;
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        AuthenticationService.Instance.SignedOut -= OnSignedOut;
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
        EventSystem.Broadcast(new SignInEvent());
        Debug.Log("Signed in!");
    }

    private void OnSignedOut()
    {
        EventSystem.Broadcast(new SignOutEvent());
        Debug.Log("Signed out!");
    }

    private void OnExpiredSession()
    {
        EventSystem.Broadcast(new SignOutEvent());
        Debug.LogWarning("Session expired");
    }
}
