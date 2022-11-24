using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityInjector;

namespace CM3D2.PartsEdit.Plugin
{
    class PartsEdit : MonoBehaviour
    {
        static UIWindow window;

        SettingUI settingUI = new SettingUI();
        TargetSelectModeUI targetSelectModeUI = new TargetSelectModeUI();
        ObjectEditUI objectEditUI = new ObjectEditUI();

        ImportUI importUI = new ImportUI();
        ExportUI exportUI = new ExportUI();
        BoneDisplaySettingUI boneDisplaySettingUI = new BoneDisplaySettingUI();
        GizmoSettingUI gizmoSettingUI = new GizmoSettingUI();

        FinishUI finishUI = null;

        BoneEdit boneEdit;

        Mode mode = Mode.Edit;

        void Start()
        {
            window = gameObject.AddComponent<GearWindowSystem>().GetUIWindow();
            window.SetWindowInfo("PartsEdit", PluginInfo.Version);

            window.AddDrawEvent(Draw);

            finishUI = new FinishUI(window);

            boneEdit = gameObject.AddComponent<BoneEdit>();

            Harmony.CreateAndPatchAll(typeof(PartsEdit));
        }

        /*
        static bool hide_ui_ = false;
        /// <summary>
        /// GizmoRender.UIVisible 를 사요하면 너무 늦음
        /// </summary>
        [HarmonyPatch(typeof(CameraMain), "ScreenShot", new Type[] { typeof(bool) })]
        [HarmonyPrefix]
        private static void ScreenShot(bool f_bNoUI)
        {
            if (hide_ui_ = f_bNoUI)
                UIHide();
        }
        /// <summary>
        /// GizmoRender.UIVisible 를 사요하면 너무 늦음
        /// </summary>
        [HarmonyPatch(typeof(CameraMain), "ScreenShot", new Type[] { typeof(string), typeof(int), typeof(bool) })]
        [HarmonyPrefix]
        private static void ScreenShot(string file_path, int super_size, bool no_ui_mode)
        {
            if (hide_ui_ = no_ui_mode)
                UIHide();
        }
        */


        static bool IsVisibleBak = false;

        /// <summary>
        /// GizmoRender.UIVisible 를 사용하면 너무 늦음
        /// </summary>
        [HarmonyPatch(typeof(CameraMain), "UIHide")]
        [HarmonyPrefix]
        private static void UIHide()
        {
            //hide_ui_ = true;
            if(IsVisibleBak=window.IsVisible)
                window.SetVisible(false);
        }

        [HarmonyPatch(typeof(CameraMain), "UIResume")]
        [HarmonyPostfix]
        private static void UIResume()
        {
            //hide_ui_ = false;
            if (IsVisibleBak)
                window.SetVisible(true);
        }

        void Draw()
        {
            //if (hide_ui_)
            //{
            //    return;
            //}
            if (mode != Setting.mode)
            {
                mode = Setting.mode;
                if (mode == Mode.Import)
                {
                    importUI.Reset();
                }
            }
            switch (Setting.mode)
            {
                case Mode.Edit:
                    settingUI.Draw();

                    GUILayout.Label("");
                    targetSelectModeUI.Draw();

                    //GUILayout.Label("");
                    //objSelectUI.Draw();

                    GUILayout.Label("");
                    objectEditUI.Draw();

                    GUILayout.FlexibleSpace();

                    GUILayout.Label("");
                    finishUI.Draw();
                    break;
                case Mode.Import:
                    importUI.Draw();
                    break;
                case Mode.Export:
                    exportUI.Draw();
                    break;
                case Mode.BoneDisplaySetting:
                    boneDisplaySettingUI.Draw();
                    break;
                case Mode.GizmoSetting:
                    gizmoSettingUI.Draw();
                    break;
            }
        }

    }
}
