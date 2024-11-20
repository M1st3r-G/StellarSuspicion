[Type]/[Folder]/[Name].png.meta
- guid

```python
TargetFolder: str = r"Assets/Art/2D/Sprites/Monster/"
OutputFolder: str = r"Assets/Resources/"

files = GetAllMetaFiles(TargetFolder)

clearResources() # Dangerous

for path in files:
	pieces = path.split("/")
	partName, folderName, fileName = pieces[-3], pieces[-2], pieces[-1]

	GetGUID(path)
	goodness: int = int(folderName) # Not Castable to int
	partType: int = PartEnumDictionary[partName] # IndexError
	# assert .png.meta
	name: str = fileName[:-len(".png.meta")]

	with open(OutputFolder + r"{partName}/{folderName}/{name}.asset", "w", encoding="utf-8") as newAssetFile:
		newAssetFile.write(GetConentAsString(name, guid, partType, goodness))
	
	print(f"{path} was successful")
```

```yaml
%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 0}
  m_Name: [Name]
  m_EditorClassIdentifier: Assembly-CSharp:Data:CreaturePartAsset
  part:
    sprite: {fileID: 21300000, guid: [guid], type: 3}
    type: [Type]
    isGood: [Folder]
```