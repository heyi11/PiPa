using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType
{
    private string path;
    private string name;

    // ��ȡui�����·��������
    public string Path { get => path; }
    public string Name { get => name; }

    public UIType(string ui_path, string ui_name)
    {
        path = ui_path;
        name = ui_name;
    }
}
