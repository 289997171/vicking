
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectItem))]
public class EffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EffectItem effectItem = (EffectItem) target;

        EffectItem parentEffectItem = effectItem.transform.parent.GetComponent<EffectItem>();
        if (parentEffectItem == null)
        {
            effectItem.isRootEffect = true;

            effectItem.follow = EditorGUILayout.Toggle("Follow", effectItem.follow);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Toggle("IsRootEffect", effectItem.isRootEffect);
            //            if (!effectItem.follow)
            //            {
            //                effectItem.initNotFollow();
            //                EffectItem[] effectItems = effectItem.transform.GetComponentsInChildren<EffectItem>();
            //                foreach (EffectItem item in effectItems)
            //                {
            //                    item.initNotFollow();
            //                    if (effectItem.lastTime < item.lastTime)
            //                    {
            //                        effectItem.lastTime = item.lastTime;
            //                    }
            //                }
            //                EditorGUILayout.LabelField("LastTime:" + effectItem.lastTime);
            //            }
            EditorGUI.EndDisabledGroup();
        }
        else
        {
            effectItem.isRootEffect = false;
            effectItem.follow = parentEffectItem.follow;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Toggle("Follow", effectItem.follow);
            EditorGUILayout.Toggle("IsRootEffect", effectItem.isRootEffect);
            EditorGUI.EndDisabledGroup();
        }
    }
}
