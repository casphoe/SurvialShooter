using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * InitializeOnLoad : 에디터가 초기화 되었을 경우 먼저 실행해라는 역활을 수행
 * 해당 속성이 지정되어 있는  스크립트는 에디터가 초기화가 됨과 동시에 해당 클래스 또는 함수가 실행
 */

[InitializeOnLoad]
public static class AutoSceneManager
{
    /*
     * 정적 생성자 : 해당 클래스를 사용할 때 가장 먼저 호출되는 함수를 의미
     * 
     * EditorApplication.projectChanged 델리게이트는 유니티 프로젝트 상에 새로운 에셋이 추가되거나 기존 에셋이 제거 되었을 경우
     * 호출 되는 함수를 등록하는 것이 가능
     * 이때, 이벤트를 통해서 호출 될 콜백 함수는 할당이 불가능하고 추가 또는 제거만 가능
     */

    //정적 생성자
    static AutoSceneManager()
    {
        EditorApplication.projectChanged -= AutoSceneManager.OnChangeProject;
        EditorApplication.projectChanged += AutoSceneManager.OnChangeProject;
    }

    // 프로젝트가 변경 되었을 경우
    public static void OnChangeProject()
    {
        /*
         * AssestDatabase.FindAssets 함수는 특정 경로에 있는 에셋을 탐색하는 역활을 수행한다.
         * 이때, 해당 함수의 결과는 각 에셋을 식별 할 수 잇는 GUID 배열이 넘어온다.
         * 
         * (즉, 해당 식별자를 통해서 별도로 실제 에셋을 로드해야한다.)
         * 
         */

        var oAssestGUIDs = AssetDatabase.FindAssets("SurivalShooter", new string[]
        {
            "Assets/SurivalShooter/Scene" //Assets/TankGame/Scene 폴더만 검색
        });
        Debug.LogFormat("Assest GUID Count : {0}", oAssestGUIDs.Length);
        var oEditorScenes = new EditorBuildSettingsScene[oAssestGUIDs.Length];

        /*
         * AssetDatabase.GUIDToAssetPath 함수는 GUID를 기반으로 해당 식별자를 지니고 있는 에셋의 경로를 반환하는
         * 역활을 수행
         */

        for (int i = 0; i < oAssestGUIDs.Length; i++)
        {
            string ScenePath = AssetDatabase.GUIDToAssetPath(oAssestGUIDs[i]);

            oEditorScenes[i] = new EditorBuildSettingsScene(ScenePath, true);
        }

        EditorBuildSettings.scenes = oEditorScenes;
    }
}
