using GondrLib.ObjectPool.RunTime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GondrLib.ObjectPool.Editor
{
    [CustomEditor(typeof(PoolItemSO))]
    public class PoolItemSOEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset visualTreeAsset = default;
        private PoolItemSO _targetItem;
        
        public override VisualElement CreateInspectorGUI()
        {
            _targetItem = target as PoolItemSO;
            
            VisualElement root = new VisualElement();

            visualTreeAsset.CloneTree(root);
            
            TextField nameField = root.Q<TextField>("PoolingNameField");
            nameField.RegisterValueChangedCallback(HandleAssetNameChange);
            return root;
        }

        private void BackToOldName(TextField nameField, string oldName)
        {
            nameField.SetValueWithoutNotify(oldName); //이전 값으로 돌려놓는다.
            _targetItem.poolingName = oldName;
            EditorUtility.SetDirty(_targetItem);
            AssetDatabase.SaveAssets();
        }
        
        private void HandleAssetNameChange(ChangeEvent<string> evt)
        {
            if (string.IsNullOrEmpty(evt.newValue))
            {
                BackToOldName(evt.target as TextField, evt.previousValue);
                EditorUtility.DisplayDialog("Error", "Name cannot be empty", "OK");
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(target); //현재 에셋의 경로를 찾아온다.
            
            //이름을 변경해주는 함수로, 성공시에는 message에 null이 들어간다. 실패시에는 실패 메시지가 들어온다.
            string message = AssetDatabase.RenameAsset(assetPath, evt.newValue);
            if (string.IsNullOrEmpty(message))
            {
                target.name = evt.newValue;
            }
            else
            {
                BackToOldName(evt.target as TextField, evt.previousValue);
                EditorUtility.DisplayDialog("Error", message, "OK");
            }
        }
    }
}