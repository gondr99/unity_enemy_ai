using System;
using System.Collections.Generic;
using System.IO;
using GondrLib.ObjectPool.Editor;
using GondrLib.ObjectPool.RunTime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using PoolItemUI = GondrLib.ObjectPool.Editor.PoolItemUI;

public class PoolManagerEditor : EditorWindow
{
    [SerializeField] private VisualTreeAsset visualTreeAsset = default;
    [SerializeField] private PoolManagerSO poolManager = default;
    [SerializeField] private VisualTreeAsset itemAsset = default;

    private string _rootFolder;
    private Button _createBtn;
    private ScrollView _itemView;

    private List<PoolItemUI> _itemList;
    private PoolItemUI _selectedItem;

    private UnityEditor.Editor _cacheEditor;
    private VisualElement _inspectorView;
    
    [MenuItem("Tools/PoolManagerEditor")]
    public static void ShowWindow()
    {
        PoolManagerEditor wnd = GetWindow<PoolManagerEditor>();
        wnd.titleContent = new GUIContent("PoolManagerEditor");
        wnd.minSize = new Vector2(600, 480);
    }

    
    public void CreateGUI()
    {
        InitializeRootFolder();
        VisualElement root = rootVisualElement;
        visualTreeAsset.CloneTree(root);
        
        GetElements(root);
        GeneratePoolingItems();
    }

    private void GetElements(VisualElement root)
    {
        _createBtn = root.Q<Button>("CreateBtn");
        _createBtn.clicked += HandleCreateItem;
        _itemView = root.Q<ScrollView>("ItemView");
        _inspectorView = root.Q<VisualElement>("InspectorView");
        
        _itemList = new List<PoolItemUI>();
    }

    private void HandleCreateItem()
    {
        string itemName = Guid.NewGuid().ToString();
        PoolItemSO newItem = ScriptableObject.CreateInstance<PoolItemSO>();
        newItem.poolingName = itemName;

        if (Directory.Exists($"{_rootFolder}/Items") == false)
        {
            Directory.CreateDirectory($"{_rootFolder}/Items");
        }
        
        AssetDatabase.CreateAsset(newItem, $"{_rootFolder}/Items/{itemName}.asset");
        poolManager.itemList.Add(newItem);
        EditorUtility.SetDirty(poolManager);
        AssetDatabase.SaveAssets();
        
        GeneratePoolingItems();
    }

    private void GeneratePoolingItems()
    {
        _itemView.Clear();
        _itemList.Clear();

        foreach (PoolItemSO item in poolManager.itemList)
        {
            TemplateContainer itemUI = itemAsset.Instantiate(); //생성해준다.
            PoolItemUI poolItemUI = new PoolItemUI(itemUI, item);
            _itemView.Add(itemUI); //UI엘레멘트도 화면에 추가해주고
            _itemList.Add(poolItemUI);  //리스트에도 추가해준다.

            if (_selectedItem != null && _selectedItem.poolItem == item)
            {
                HandleSelectEvent(poolItemUI);
            }
            
            poolItemUI.Name = item.poolingName;

            poolItemUI.OnSelectEvent += HandleSelectEvent;
            poolItemUI.OnDeleteEvent += HandleDeleteEvent;
        }
    }

    private void HandleDeleteEvent(PoolItemUI item)
    {
        if (EditorUtility.DisplayDialog("Delete", "Do you want to delete this item?", "Yes", "No") == false)
        {
            return;
        }
        
        poolManager.itemList.Remove(item.poolItem); //리스트에서 제거
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item.poolItem)); //실제 파일 삭제
        EditorUtility.SetDirty(poolManager); //스크립터블 오브젝트에 변경사항 저장
        AssetDatabase.SaveAssets(); //에셋 저장
        
        if(item == _selectedItem)
        {
            _selectedItem = null;
        }
        
        GeneratePoolingItems();
    }

    private void HandleSelectEvent(PoolItemUI item)
    {
        if (_selectedItem != null)
            _selectedItem.IsActive = false;
        _selectedItem = item;
        _selectedItem.IsActive = true;
        
        _inspectorView.Clear();
        UnityEditor.Editor.CreateCachedEditor(_selectedItem.poolItem, null, ref _cacheEditor);
        //_cachedEditor 변수를 재활용해서 에디터가 만들어질때마다 힙에 할당되는 걸 막는다.
        VisualElement inspector = _cacheEditor.CreateInspectorGUI(); //만들어진 에디터 GUI를 UI툴킷으로 변환해서 가져온다.
        
        SerializedObject serializedObject = new SerializedObject(_selectedItem.poolItem); //직렬화 오브젝트 만들어주고
        inspector.Bind(serializedObject); //해당 오브젝트와 툴킷을 묵어준다.
        inspector.TrackSerializedObjectValue(serializedObject, so =>
        {
            _selectedItem.Name = so.FindProperty("poolingName").stringValue; //이름을 바꿔준다.
        });
        _inspectorView.Add(inspector); //인스펙터 뷰에 추가해준다.
    }


    private void InitializeRootFolder()
    {
        //이 스크립트를 에셋으로서 가져온다.
        MonoScript monoScript = MonoScript.FromScriptableObject(this);
        string scriptPath = AssetDatabase.GetAssetPath(monoScript);
        _rootFolder = Path.GetDirectoryName( Path.GetDirectoryName(scriptPath)).Replace("\\", "/");

        if (visualTreeAsset == null)
        {
            string loadPath = $"{_rootFolder}/Editor/PoolManagerEditor.uxml";
            visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(loadPath);
            Debug.Assert(visualTreeAsset != null, $"Load Failed : {loadPath}");
        }

        if (poolManager == null)
        {
            string filePath = $"{_rootFolder}/PoolManager.asset";
            poolManager = AssetDatabase.LoadAssetAtPath<PoolManagerSO>(filePath);
            if (poolManager == null)
            {
                Debug.LogWarning("Pool manager scriptable object is not exist, create new one");
                poolManager = ScriptableObject.CreateInstance<PoolManagerSO>();
                AssetDatabase.CreateAsset(poolManager, filePath);
            }
        }

        if (itemAsset == null)
        {
            string loadPath = $"{_rootFolder}/Editor/PoolItemUI.uxml";
            itemAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(loadPath);
            Debug.Assert(itemAsset != null, $"Load Failed : {loadPath}");
        }
    }
    
}
