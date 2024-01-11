using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClipsSO audioClipsSO;

    private void Start() {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnObjectPickup += Player_OnObjectPickup;
        BaseCounter.OnObjectPlaced += BaseCounter_OnObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        var counter = (TrashCounter)sender;
        var position = counter.transform.position;
        PlaySound(audioClipsSO.trash, position);
    }

    private void BaseCounter_OnObjectPlaced(object sender, System.EventArgs e) {
        var counter  = (BaseCounter)sender;
        var position = counter.transform.position;
        PlaySound(audioClipsSO.objectDrop, position);
    }

    private void Player_OnObjectPickup(object sender, System.EventArgs e) {
        var position = Player.Instance.transform.position;
        PlaySound(audioClipsSO.objectPickup, position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        var counter  = (CuttingCounter)sender;
        var position = counter.transform.position;
        PlaySound(audioClipsSO.chop, position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        var position = DeliveryManager.Instance.transform.position;
        PlaySound(audioClipsSO.deliveryFail, position);
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
        var position = DeliveryManager.Instance.transform.position;
        PlaySound(audioClipsSO.deliverSuccess, position);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1f) {
        int v = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[v] , position, volume);
    }
}
