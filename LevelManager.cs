using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPosition;
     public void Awake() {
        
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }
    public void Start()
    {
        GenerateInitialBlocks();
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void AddLevelBlock()
    {
    int randomIdx = Random.Range(0, allTheLevelBlocks.Count);

    LevelBlock block;

    Vector3 spawnPosition = Vector3.zero;

    if(currentLevelBlocks.Count == 0){
        block = Instantiate(allTheLevelBlocks[3]);
        spawnPosition = levelStartPosition.position;
    }else{
        block = Instantiate(allTheLevelBlocks[randomIdx]);
        spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;

    }
    block.transform.SetParent(this.transform,false);

    Vector3 correction = new Vector3(spawnPosition.x-block.startPoint.position.x,
                                     spawnPosition.y-block.startPoint.position.y,
    0);
    block.transform.position = correction;
    currentLevelBlocks.Add(block);
    
    }

    public void RemoveLevelBlock() 
    {
        
    }

    public void RemoveAllLevelBlocks() 
    {
        
    }

    public void  GenerateInitialBlocks() {
        for (int i = 0; i < 2; i++){
        AddLevelBlock();
    }
    }
}
