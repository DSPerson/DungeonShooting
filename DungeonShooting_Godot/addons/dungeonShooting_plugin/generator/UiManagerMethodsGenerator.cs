﻿#if TOOLS

using System;
using System.IO;

namespace Generator;

/// <summary>
/// 生成 UiManager 中打开Ui相关的函数代码
/// </summary>
public static class UiManagerMethodsGenerator
{
    private static string savePath = "src/game/manager/UiManager_Methods.cs";

    /// <summary>
    /// 执行生成操作, 并返回执行结果
    /// </summary>
    public static bool Generate()
    {
        //扫描所有ui
        if (!Directory.Exists(GameConfig.UiPrefabDir))
        {
            return true;
        }

        try
        {
            var directoryInfo = new DirectoryInfo(GameConfig.UiPrefabDir);
            var fileInfos = directoryInfo.GetFiles();

            var code = $"\n// 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失\n" +
                       $"public static partial class UiManager\n" +
                       $"{{\n" +
                       $"\n";

            var uiNameClass = $"    public static class UiNames\n" +
                              $"    {{\n";
            var methodClass = "";

            foreach (var fileInfo in fileInfos)
            {
                if (fileInfo.Extension == ".tscn")
                {
                    var uiName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 5);
                    uiNameClass += $"        public const string {uiName} = \"{uiName}\";\n";
                    
                    methodClass += $"    /// <summary>\n" +
                                   $"    /// 创建 {uiName}, 并返回UI实例, 该函数不会打开 Ui\n" +
                                   $"    /// </summary>\n" +
                                   $"    public static UI.{uiName}.{uiName}Panel Create_{uiName}()\n" +
                                   $"    {{\n" +
                                   $"        return CreateUi<UI.{uiName}.{uiName}Panel>(UiNames.{uiName});\n" +
                                   $"    }}\n" +
                                   $"\n" +
                                   $"    /// <summary>\n" +
                                   $"    /// 打开 {uiName}, 并返回UI实例\n" +
                                   $"    /// </summary>\n" +
                                   $"    public static UI.{uiName}.{uiName}Panel Open_{uiName}()\n" +
                                   $"    {{\n" +
                                   $"        return OpenUi<UI.{uiName}.{uiName}Panel>(UiNames.{uiName});\n" +
                                   $"    }}\n" +
                                   $"\n" +
                                   $"    /// <summary>\n" +
                                   $"    /// 隐藏 {uiName} 的所有实例\n" +
                                   $"    /// </summary>\n" +
                                   $"    public static void Hide_{uiName}()\n" +
                                   $"    {{\n" +
                                   $"        var uiInstance = Get_{uiName}_Instance();\n" +
                                   $"        foreach (var uiPanel in uiInstance)\n" +
                                   $"        {{\n" +
                                   $"            uiPanel.HideUi();\n" +
                                   $"        }}\n" +
                                   $"    }}\n" +
                                   $"\n" +
                                   $"    /// <summary>\n" +
                                   $"    /// 销毁 {uiName} 的所有实例\n" +
                                   $"    /// </summary>\n" +
                                   $"    public static void Destroy_{uiName}()\n" +
                                   $"    {{\n" +
                                   $"        var uiInstance = Get_{uiName}_Instance();\n" +
                                   $"        foreach (var uiPanel in uiInstance)\n" +
                                   $"        {{\n" +
                                   $"            uiPanel.Destroy();\n" +
                                   $"        }}\n" +
                                   $"    }}\n" +
                                   $"\n" +
                                   $"    /// <summary>\n" +
                                   $"    /// 获取所有 {uiName} 的实例, 如果没有实例, 则返回一个空数组\n" +
                                   $"    /// </summary>\n" +
                                   $"    public static UI.{uiName}.{uiName}Panel[] Get_{uiName}_Instance()\n" +
                                   $"    {{\n" +
                                   $"        return GetUiInstance<UI.{uiName}.{uiName}Panel>(nameof(UI.{uiName}.{uiName}));\n" +
                                   $"    }}\n" +
                                   $"\n";
                }
            }
            uiNameClass += $"    }}\n\n";

            code += uiNameClass;
            code += methodClass;
            code += $"}}\n";

            File.WriteAllText(savePath, code);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return false;
        }

        return true;
    }
}

#endif