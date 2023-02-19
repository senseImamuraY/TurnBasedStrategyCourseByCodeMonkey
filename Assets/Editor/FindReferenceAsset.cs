using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FindAssetData
{
    public string AssetName { get; }
    public string AssetPath { get; }
    public string AssetGuid { get; }

    public bool Foltout { get; set; }

    public FindAssetData(string assetName, string assetPath, string assetGuid)
    {
        AssetName = assetName;
        AssetPath = assetPath;
        AssetGuid = assetGuid;
    }
}

public class FindReferenceAsset : EditorWindow
{

    private const string findAssetsType = "t:scene t:prefab t:timelineAsset t:animatorcontroller t:Material";
    private static Dictionary<FindAssetData, List<FindAssetData>> findResult = new Dictionary<FindAssetData, List<FindAssetData>>();

    private Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Assets/�Q�Ƃ�T��", false)]
    public static void FindAsset()
    {

        Object[] selectList = Selection.objects;
        if (selectList == null || selectList.Length == 0)
        {
            Debug.Log("There are no selection objects.");
            return;
        }
        findResult.Clear();

        //�I�������A�Z�b�g�̃p�X��GUID��ǂݍ���
        List<FindAssetData> selectAssets = new List<FindAssetData>();
        foreach (var selectItem in selectList)
        {
            string path = AssetDatabase.GetAssetPath(selectItem);
            string guid = AssetDatabase.AssetPathToGUID(path);
            selectAssets.Add(new FindAssetData(selectItem.name, path, guid));
        }

        //�v���W�F�N�g���ɂ���A�����Ώۂ̃A�Z�b�g�����ׂĎ擾����
        foreach (string assetGuid in AssetDatabase.FindAssets(findAssetsType))
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);

            //�Ώۂ̃A�Z�b�g�ƈˑ��֌W�ɂ���A�Z�b�g�����ׂĎ擾����.
            foreach (string dependAssetPath in AssetDatabase.GetDependencies(assetPath))
            {
                if (assetPath == dependAssetPath) continue;

                string dependGuid = AssetDatabase.AssetPathToGUID(dependAssetPath);
                for (int i = 0; i < selectAssets.Count; i++)
                {

                    if (!findResult.ContainsKey(selectAssets[i]))
                    {
                        findResult.Add(selectAssets[i], new List<FindAssetData>());
                    }

                    //�I�������A�Z�b�g�Ɠ���GUID�ł���΁A�Q�Ƃ���Ă���Ƃ������ƂɂȂ�
                    if (dependGuid == selectAssets[i].AssetGuid)
                    {
                        string hitName = AssetDatabase.LoadMainAssetAtPath(assetPath).name;
                        FindAssetData hit = new FindAssetData(hitName, assetPath, assetGuid);

                        findResult[selectAssets[i]].Add(hit);
                    }
                }
            }
        }

        GetWindow<FindReferenceAsset>();
    }

    private void OnGUI()
    {
        if (findResult == null || findResult.Count == 0) return;

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        foreach (KeyValuePair<FindAssetData, List<FindAssetData>> pair in findResult)
        {
            int referenceCount = pair.Value == null ? 0 : pair.Value.Count;
            if (pair.Key.Foltout = EditorGUILayout.Foldout(pair.Key.Foltout, pair.Key.AssetName + " : " + referenceCount + "��"))
            {
                foreach (var item in pair.Value)
                {

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20);

                    EditorGUIUtility.SetIconSize(Vector2.one * 16);

                    Object target = AssetDatabase.LoadAssetAtPath<Object>(item.AssetPath);
                    //�A�Z�b�g��Prefab�Ȃ̂�Scene�Ȃ̂����ʕt���₷�����邽�߂ɃA�C�R���̕\��
                    GUIContent guiContent = new GUIContent(
                        item.AssetName,
                        EditorGUIUtility.ObjectContent(target, target.GetType()).image
                    );

                    if (GUILayout.Button(guiContent, "Label"))
                    {
                        Selection.objects = new[] { target };
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }
}