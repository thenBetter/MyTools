﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// 竖排显示文字
/// </summary>
[ExecuteInEditMode]
public class VerticalText : BaseMeshEffect
{

    [Tooltip("字和字之间的间距")]
    public float spacing = 1f;
    private float lineSpacing = 1;
    private float textSpacing = 1;
    private float xOffset = 0;
    private float yOffset = 0;

    public override void ModifyMesh(VertexHelper helper)
    {
        if (!IsActive()) return;

        List<UIVertex> verts = new List<UIVertex>();
        helper.GetUIVertexStream(verts);
        Text text = GetComponent<Text>();
        TextGenerator tg = text.cachedTextGenerator;
        lineSpacing = text.fontSize * text.lineSpacing;
        textSpacing = text.fontSize * spacing;

        xOffset = text.rectTransform.sizeDelta.x / 2f - text.fontSize / 2f;
        yOffset = text.rectTransform.sizeDelta.y / 2f - text.fontSize / 2f;
        List<UILineInfo> lines = new List<UILineInfo>();
        tg.GetLines(lines);
        for (int i = 0; i < lines.Count; i++)
        {
            UILineInfo line = lines[i];
            int step = i;
            if (i + 1 < lines.Count)
            {
                UILineInfo line2 = lines[i + 1];
                int current = 0;
                for (int j = line.startCharIdx; j < line2.startCharIdx - 1; j++)
                {
                    ModifyText(helper, j, current++, step);
                }
            }
            else if (i + 1 == lines.Count)
            {
                int current = 0;
                for (int j = line.startCharIdx; j < tg.characterCountVisible; j++)
                {
                    ModifyText(helper, j, current++, step);
                }
            }
        }
    }

    private void ModifyText(VertexHelper helper, int i, int charYpos, int charXPos)
    {
        UIVertex lb = new UIVertex();
        helper.PopulateUIVertex(ref lb, i * 4);

        UIVertex lt = new UIVertex();
        helper.PopulateUIVertex(ref lt, i * 4 + 1);

        UIVertex rt = new UIVertex();
        helper.PopulateUIVertex(ref rt, i * 4 + 2);

        UIVertex rb = new UIVertex();
        helper.PopulateUIVertex(ref rb, i * 4 + 3);

        Vector3 center = Vector3.Lerp(lb.position, rt.position, 0.5f);
        Matrix4x4 mov = Matrix4x4.TRS(-center, Quaternion.identity, Vector3.one);  //位置转换

        float x = -charXPos * lineSpacing + xOffset;
        float y = -charYpos * textSpacing + yOffset;

        Vector3 pos = new Vector3(x, y, 0);
        Matrix4x4 place = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
        Matrix4x4 transform = place * mov;                        //从水平到垂直

        lb.position = transform.MultiplyPoint(lb.position);
        lt.position = transform.MultiplyPoint(lt.position);
        rt.position = transform.MultiplyPoint(rt.position);
        rb.position = transform.MultiplyPoint(rb.position);

        helper.SetUIVertex(lb, i * 4);
        helper.SetUIVertex(lt, i * 4 + 1);
        helper.SetUIVertex(rt, i * 4 + 2);
        helper.SetUIVertex(rb, i * 4 + 3);
    }
}
