using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// ���ӹ�̨
public class PlatesCounter : BaseCounter
{
    // ����������Ҫʱ��
    private float spawnPlateTimer;
    // ����������Ҫ���ʱ��
    private float spawnPlateTimerMax = 4f;
    // ��������
    private int platesSpawnAmount;
    // ���������������
    private int platesSpawnAmountMax = 4;

    // ����SO
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


    // ���������¼�
    public event EventHandler OnPlateSpawned;
    // ���������¼�
    public event EventHandler OnPlateRemoved;

    private void Update() {
        spawnPlateTimer+= Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0;
            if (platesSpawnAmount < platesSpawnAmountMax) {
                platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                // KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
            }
            
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // ���ٻ���һ������
            if (platesSpawnAmount>0) {
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
