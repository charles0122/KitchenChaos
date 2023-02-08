using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 盘子柜台
public class PlatesCounter : BaseCounter
{
    // 生成盘子需要时间
    private float spawnPlateTimer;
    // 生成盘子需要最大时间
    private float spawnPlateTimerMax = 4f;
    // 盘子数量
    private int platesSpawnAmount;
    // 盘子生成最大数量
    private int platesSpawnAmountMax = 4;

    // 盘子SO
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


    // 盘子生成事件
    public event EventHandler OnPlateSpawned;
    // 盘子移走事件
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
            // 至少还有一个盘子
            if (platesSpawnAmount>0) {
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
