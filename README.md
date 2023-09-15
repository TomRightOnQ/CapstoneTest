# Project Capstone
> This lib contains scripts prepared for the ICS169 Capstone project.

**@atomREaktoR**

## Contents:
0.Naming and structure

1.Pooling System

2.Effect Player

3.Auto Enum and Const Script

## 0. Naming and Structure
Public members: StartWithCapital
private members: useLowerCases
booleans: bIsEnabled -> start with a "b"
Derived class: Start with parent first letter, such as (MonoBehavior) -> MObject -> MEntity -> EMonster, or (MonoBehavior) -> Object -> MEntity -> EProjectile -> EBullet

Under the Assets/Designs you may see the csv/excel files and the python scripts to make C# data.
If a C# data table contains plain data only wihtout any reference of objects, then it should be located in the Assets/Scripts/Contents/General; those csv files are under Assets/Designs/General. You can simply use ....Data.GetData(id) to acquire info.

Inside Assets/Scripts/Contents, ....Data.cs indicates a file of data table loaded from csv file, and ....Config.cs indicates a scriptable object located in Resources folder that is maintained in the inspector.

### Persisitent Game Manager
This Manager is a DontDestroyOnLoad singleton controls the initialization of managers and configs.

**Managers** are singleton objects that controls the behavior of a module.
**Configs** are scriptable singletons that carry the data structure that a module needs.
>For example, PrefabConfig defines a struct named PrefabData to hold the customized metadata of a prefab, while PrefabManager controls all behavior.

### File Structure
Scripts\Contents: C# tables and scriptable singleton's source code
Scripts\FrameWork: 
CoreComponents: Core base classes such as ScriptableSingleton and MObject
FrameSystem: BGM, Effect and Pooling system

# 1. Pooling:
**Pooling.cs**: Scriptable Object for the pooling itself
**ObjPoolBase.cs**: Wrapper class for the generic pools
**ObjPool.cs**: Generic class for each pool
**PrefabConfig.cs** Scriptable Object holds references of prefabs; not recommended to call any public method from it
**PrefabManager.cs**: Singleton Object contacts with **Pooling.cs and PrefabConfig.cs**. Acts as a mediator between the Pooling and PrefabConfig classes. It interfaces with PrefabConfig to get prefab data and then uses this data to communicate with the Pooling class to initialize pools for these prefabs.

PrefabData:
    **"name"**: string name of the prefab
    **"TypeName"**: string name of the component attached to the prefab as its type
    **"Count"**: int only works for poolable object -> indicating the inited amount in pooling system
    "**PrefabPath"**: string path of the prefab 
   **"IsExpandable"**: bool only works for poolable object -> determines if the pool will expand when run out
    **"ExpRatio"**: float only works for poolable object -> determines how much the pool expand when run out
    **"bPoolable"**: bool determines if the prefab is managed by the pooling system
    **"bPoolByDefault"**: bool determines if the prefab is inited in the pool regardless the current scene

>------These are now private, do not use-----------
Pooling.Instance.GetObj(string TypeName); 
Get the GameObject form of an inited object in the pool, object will be set to active

>Pooling.Instance.GetObj<T>();
Return the component T of an instance from the pool

>Pooling.Instance.ReturnObj<T>(T prefab);
Return an object to the pooling system by its type component registered in the json file

>Pooling.Instance.ReturnObj(GameObject prefab);
Return an object to the pooling; reconmmended to let each object use this.gameObject as the parameter

**PrefabManager.Instance.Instantiate(string prefabName, Vector3 position, Quaternion rotation);**
Overloading of the native method, work with pooling system.

**PrefabManager.Instance.Destroy(GameObject gameObject);**
Overloading of the native method, work with pooling system.

**PrefabManager.Instance.GetReferenceType(string name);
PrefabManager.Instance.GetReferenceType(GameObject prefab);**
Get the string name of the type component based on the string name or the reference of an instance of a prefab

**PrefabManager.Instance.SetPoolUp(string typeName, GameObject prefabReference, int count, bool isExpandable, float expandableRatio);**
Init independent pooling of a prefab -> this function should not be called unless intended
This function goes to PrefabConfig to retrieve the data of a prefab, then goes back to itself to contact with the pooling system
 
 # 2. Effect Player:                                  
**GameEffect.cs**: Abstract class with interface class ISetup for objects carrying a visual or audio effect
**SFXObject.cs**: Child class for audio effect
**AnimObject.cs**: Child class for 2D anim clips

**GameEffectConfig.cs**: Scriptable Object generic base class for configurations of effect references -> Do not create object for it
**AudioConfig.cs**: Child class for audio clips
**AnimConfig.cs**: Child class for animation clips

**GameEffectManager.cs**: Singleton manages the play of an effect
**AnimManager.cs and AudioManager.cs**: Not working currently, may be deleted.

All effects are saved in an array supporting change in the inspectors: drag the corresponding clip with a string name

**GameEffectManager.Instance.PlayEffect<T>(string _name, Vector3 pos);**
Play an effect based on the type, getting specific clip based on the string name.
Audio: T -> SFXObject
Animation: T -> AnimObject

**GameEffectManager.Instance.PlaySound(string _name, Vector3 pos);**
**GameEffectManager.Instance.PlayAnim(string _name, Vector3 pos, Vector3 scale);**
Non-Generic versions of PlayEffect<T>
