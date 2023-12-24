﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CustomEffectMatGUI : ShaderGUI {
    public enum CustomBlendMode
    {
        Opaque,
        BlendNormal,
        BlendAdd,
        Cutout,
        BlendNormalUI,
        BlendAddUI
    }
    private static class Styles
    {
        public static GUIContent TwoSideText = new GUIContent("2-Side");
        public static GUIContent FogModeText = new GUIContent("FogMode");

        public static GUIContent AlphaCutoutText = new GUIContent("AlphaCutout");
        public static GUIContent DiffuseText = new GUIContent("第一层贴图");
        public static GUIContent DiffuseRoText = new GUIContent("旋转");
        public static GUIContent DiffuseAngText = new GUIContent("旋转角度");
        public static GUIContent ColorText = new GUIContent("颜色");
        public static GUIContent BrightnessText = new GUIContent("亮度（0-10）");
        public static GUIContent UspeedText = new GUIContent("Uspeed");
        public static GUIContent VspeedText = new GUIContent("Vspeed");

        public static GUIContent DiffuseMaskBlockText = new GUIContent("第一层遮罩");
        public static GUIContent DiffuseMaskText = new GUIContent("遮罩贴图");
        public static GUIContent DiffuseMaskRoText = new GUIContent("旋转");
        public static GUIContent DiffuseMaskAngText = new GUIContent("旋转角度");
        public static GUIContent USpeedDiffusemText = new GUIContent("USpeed");
        public static GUIContent VSpeedDiffusemText = new GUIContent("VSpeed");

        public static GUIContent SLayerBlockText = new GUIContent("第二层");
        public static GUIContent SLayerText = new GUIContent("第二层贴图");
        public static GUIContent SLayerColorText = new GUIContent("颜色");
        public static GUIContent SLayerBrightnessText = new GUIContent("亮度(0-10)");
        public static GUIContent SLayerRoText = new GUIContent("旋转");
        public static GUIContent SLayerAngText = new GUIContent("旋转角度");
        public static GUIContent UspeedSLayerText = new GUIContent("Uspeed");
        public static GUIContent VspeedSLayerText = new GUIContent("Vspeed");

        public static GUIContent SlayerMaskBlockText = new GUIContent("第二层遮罩");
        public static GUIContent SlayerMaskTexText = new GUIContent("遮罩贴图");
        public static GUIContent SlayerMaskRoText = new GUIContent("旋转");
        public static GUIContent SlayerMaskAngText = new GUIContent("旋转角度");
        public static GUIContent UspeedSlayerMaskText = new GUIContent("USpeed");
        public static GUIContent VspeedSlayerMaskText = new GUIContent("VSpeed");

        public static GUIContent DissolveBlockText = new GUIContent("溶解");
       // public static GUIContent DissolveVertexText = new GUIContent("顶点颜色控制溶解");
        public static GUIContent DissolveTexText = new GUIContent("溶解贴图");
        public static GUIContent DissolveColorText = new GUIContent("溶解勾边颜色");
        public static GUIContent DissolveText = new GUIContent("溶解度");
        public static GUIContent DissolveSizeText = new GUIContent("溶解勾边大小");
        public static GUIContent DissolveBrightnessText = new GUIContent("溶解勾边亮度(0-10)");

        public static GUIContent DissolveMaskText = new GUIContent("溶解遮罩");
        public static GUIContent DissolveMaskTexText = new GUIContent("遮罩贴图");

        public static GUIContent DistortBlockText = new GUIContent("扭曲");
        public static GUIContent DistortTexText = new GUIContent("扭曲贴图");
        public static GUIContent ForceXText = new GUIContent("强度 X(0 1)");
        public static GUIContent ForceYText = new GUIContent("强度 Y(0 1)");
        public static GUIContent DistortUSpeedText = new GUIContent("USpeed");
        public static GUIContent DistortVSpeedText = new GUIContent("VSpeed");

        public static GUIContent DistortMaskText = new GUIContent("扭曲遮罩");
        public static GUIContent DistortMaskTexText = new GUIContent("遮罩贴图");
        public static GUIContent advancedOptionsText = new GUIContent("");


        //public static GUIContent DiffuseMaskTex = new GUIContent();
    }
    static readonly string[] sOptions = new string[] { "Opaque", "BlendNormal", "BlendAdd","Cutout", "BlendNormal[UI]", "BlendAdd[UI]" };
    static readonly string sOptionName = "_BlendSet";
    static readonly string sSrcRGBName = "_SrcRGBMode";
    static readonly string sDestRGBName = "_DestRGBMode";
    static readonly string sSrcAlphaName = "_SrcAlphaMode";
    static readonly string sDestAlphaName = "_DestAlphaMode";
    static readonly string sOldName = "_BlendMode";
    static readonly string ZWriteName = "_ZWrite";
    static readonly string TwoSideName = "_TwoSide";
    static readonly string FogModeName = "_FogMode";

    static readonly string AlphaCutoutName = "_AlphaCutout";
    static readonly string DiffuseTexName = "_Diffuse";
    static readonly string DiffuseRoName = "_DiffuseRo";
    static readonly string DiffuseAngName = "_DiffuseAng";
    static readonly string DiffuseColorName = "_Color";
    static readonly string DiffuseBrightnessName = "_Brightness";
    static readonly string DiffuseUspeedName = "_Uspeed";
    static readonly string DiffuseVspeedName = "_Vspeed";

    static readonly string DiffuseMaskName = "_DiffuseMask";
    static readonly string DiffuseMaskShownName = "_DiffuseMaskShown";
    static readonly string DiffuseMaskTexName = "_DiffuseMaskTex";
    static readonly string DiffuseMaskRoName = "_DiffuseMaskRo";
    static readonly string DiffuseMaskAngName = "_DiffuseMaskAng";
    static readonly string DiffuseMaskUspeedName = "_USpeed_diffusem";
    static readonly string DiffuseMaskVspeedName = "_VSpeed_diffusem";

    static readonly string SLayerName = "_SecondLayerBlock";
    static readonly string SLayerShownName = "_SecondLayerShown";
    static readonly string SLayerTexName = "_SecondLayerTex";
    static readonly string SLayerRoName = "_SecondLayerRo";
    static readonly string SLayerAngName = "_SecondLayerAng";
    static readonly string SLayerColorName = "_SecondLayerColor";
    static readonly string SLayerBrightnessName = "_SecondLayerBrightness";
    static readonly string SLayerUspeedName = "_Uspeed_second";
    static readonly string SLayerVspeedName = "_Vspeed_second";

    static readonly string SLayerMaskName = "_SecondLayerMask";
    static readonly string SLayerMaskShownName = "_SecondLayerMaskShown";
    static readonly string SLayerMaskTexName = "_SecondLayerMaskTex";
    static readonly string SLayerMaskRoName = "_SecondLayerMaskRo";
    static readonly string SLayerMaskAngName = "_SecondLayerMaskAng";
    static readonly string SLayerMaskUspeedName = "_Uspeed_secondm";
    static readonly string SLayerMaskVspeedName = "_Vspeed_secondm";

    static readonly string DissolveBlockName = "_DissolveBlock";
    static readonly string DissolveShownName = "_DissolveShown";
    //static readonly string DissolveVertexName = "_DissolveVertex";
    static readonly string DissolveTexName = "_DissolveTex";
    static readonly string DissolveColorName = "_DissolveColor";
    static readonly string DissolveName = "_Dissolve";
    static readonly string DissolveSizeName = "_DissolveSize";
    static readonly string DissolveBrightnessName = "_DissolveBrightness";

    static readonly string DissolveMaskName = "_DissolveMask";
    static readonly string DissolveMaskShownName = "_DissolveMaskShown";
    static readonly string DissolveMaskTexName = "_DissolveMaskTex";

    static readonly string DistortBlockName = "_DistortBlock";
    static readonly string DistortShownName = "_DistortShown";
    static readonly string DistortTexName = "_DistortTex";
    static readonly string ForceXName = "_ForceX";
    static readonly string ForceYName = "_ForceY";
    static readonly string DistortUSpeedName = "_USpeed_distort";
    static readonly string DistortVSpeedName = "_VSpeed_distort";

    static readonly string DistortMaskName = "_DistortMask";
    static readonly string DistortMaskShownName = "_DistortMaskShown";
    static readonly string DistortMaskTexName = "_DistortMaskTex";


    MaterialProperty option = null;
    
    //MaterialProperty srcRGBBlendMode = null;
    //MaterialProperty srcAlphaBlendMode = null;
    //MaterialProperty dstRGBBlendMode = null;
    //MaterialProperty dstAlphaBlendMode = null;
    //MaterialProperty oldBlendMode = null;
    //MaterialProperty _ZWrite = null;

    MaterialProperty _TwoSide = null;
    MaterialProperty _FogMode = null;

    MaterialProperty _AlphaCutout = null;
    MaterialProperty _Diffuse = null;
    MaterialProperty _DiffuseRo = null;
    MaterialProperty _DiffuseAng = null;
    MaterialProperty _Color = null;
    MaterialProperty _Brightness = null;
    MaterialProperty _Uspeed = null;
    MaterialProperty _Vspeed = null;

    MaterialProperty _DiffuseMask = null;
    MaterialProperty _DiffuseMaskShown = null;
    MaterialProperty _DiffuseMaskTex = null;
    MaterialProperty _DiffuseMaskRo = null;
    MaterialProperty _DiffuseMaskAng = null;
    MaterialProperty _USpeed_diffusem = null;
    MaterialProperty _VSpeed_diffusem = null;

    MaterialProperty _SLayer = null;
    MaterialProperty _SLayerShown = null;
    MaterialProperty _SLayerTex = null;
    MaterialProperty _SLayerRo = null;
    MaterialProperty _SLayerAng = null;
    MaterialProperty _SLayerColor = null;
    MaterialProperty _SLayerBrightness = null;
    MaterialProperty _SLayerUspeed = null;
    MaterialProperty _SLayerVspeed = null;

    MaterialProperty _SLayerMask = null;
    MaterialProperty _SLayerMaskShown = null;
    MaterialProperty _SLayerMaskTex = null;
    MaterialProperty _SLayerMaskRo = null;
    MaterialProperty _SLayerMaskAng = null;
    MaterialProperty _SLayerMaskUspeed = null;
    MaterialProperty _SLayerMaskVspeed = null;

    MaterialProperty _DissolveBlock = null;
    MaterialProperty _DissolveShown = null;
    //MaterialProperty _DissolveVertex = null;
    MaterialProperty _DissolveTex = null;
    MaterialProperty _DissolveColor = null;
    MaterialProperty _Dissolve = null;
    MaterialProperty _DissolveSize = null;
    MaterialProperty _DissolveBrightness = null;

    MaterialProperty _DissolveMask = null;
    MaterialProperty _DissolveMaskShown = null;
    MaterialProperty _DissolveMaskTex = null;

    MaterialProperty _DistortBlock = null;
    MaterialProperty _DistortShown = null;
    MaterialProperty _DistortTex = null;
    MaterialProperty _ForceX = null;
    MaterialProperty _ForceY = null;
    MaterialProperty _USpeed_distort = null;
    MaterialProperty _VSpeed_distort = null;

    MaterialProperty _DistortMask = null;
    MaterialProperty _DistortMaskShown = null;
    MaterialProperty _DistortMaskTex = null;

    

    MaterialEditor matEditor;
   
    bool firstTimeApply = true;
    CustomBlendMode blendMode;
    
    public void FindProperties(MaterialProperty[] props)
    {
        option = FindProperty(sOptionName, props);
        //srcRGBBlendMode = FindProperty(sSrcRGBName, props);
        //srcAlphaBlendMode = FindProperty(sSrcAlphaName, props);
        //dstRGBBlendMode = FindProperty(sDestAlphaName, props);
        //dstAlphaBlendMode = FindProperty(sDestRGBName, props);
        //oldBlendMode = FindProperty(sOldName, props, false);
        //_ZWrite = FindProperty(ZWriteName, props);
        _TwoSide = FindProperty(TwoSideName, props);
        _FogMode = FindProperty(FogModeName, props);

        _AlphaCutout = FindProperty(AlphaCutoutName, props);
        _Diffuse = FindProperty(DiffuseTexName, props);
        _DiffuseRo = FindProperty(DiffuseRoName, props);
        _DiffuseAng = FindProperty(DiffuseAngName, props);
        _Color = FindProperty(DiffuseColorName, props);
        _Brightness = FindProperty(DiffuseBrightnessName, props);
        _Uspeed = FindProperty(DiffuseUspeedName, props);
        _Vspeed = FindProperty(DiffuseVspeedName, props);

        _DiffuseMask = FindProperty(DiffuseMaskName, props);
        _DiffuseMaskShown = FindProperty(DiffuseMaskShownName, props);
        _DiffuseMaskTex = FindProperty(DiffuseMaskTexName, props);
        _DiffuseMaskRo = FindProperty(DiffuseMaskRoName, props);
        _DiffuseMaskAng = FindProperty(DiffuseMaskAngName, props);
        _USpeed_diffusem = FindProperty(DiffuseMaskUspeedName, props);
        _VSpeed_diffusem = FindProperty(DiffuseMaskVspeedName, props);

        _SLayer = FindProperty(SLayerName, props);
        _SLayerShown = FindProperty(SLayerShownName, props);
        _SLayerTex = FindProperty(SLayerTexName, props);
        _SLayerRo = FindProperty(SLayerRoName, props);
        _SLayerAng = FindProperty(SLayerAngName, props);
        _SLayerColor = FindProperty(SLayerColorName, props);
        _SLayerBrightness = FindProperty(SLayerBrightnessName, props);
        _SLayerUspeed = FindProperty(SLayerUspeedName, props);
        _SLayerVspeed = FindProperty(SLayerVspeedName, props);

        _SLayerMask = FindProperty(SLayerMaskName, props);
        _SLayerMaskShown = FindProperty(SLayerMaskShownName, props);
        _SLayerMaskTex = FindProperty(SLayerMaskTexName, props);
        _SLayerMaskRo = FindProperty(SLayerMaskRoName, props);
        _SLayerMaskAng = FindProperty(SLayerMaskAngName, props);
        _SLayerMaskUspeed = FindProperty(SLayerMaskUspeedName, props);
        _SLayerMaskVspeed = FindProperty(SLayerMaskVspeedName, props);

        _DissolveBlock = FindProperty(DissolveBlockName, props);
        _DissolveShown = FindProperty(DissolveShownName, props);
        //_DissolveVertex = FindProperty(DissolveVertexName, props);
        _DissolveTex = FindProperty(DissolveTexName, props);
        _DissolveColor = FindProperty(DissolveColorName, props);
        _Dissolve = FindProperty(DissolveName, props);
        _DissolveSize = FindProperty(DissolveSizeName, props);
        _DissolveBrightness = FindProperty(DissolveBrightnessName, props);

        _DissolveMask = FindProperty(DissolveMaskName, props);
        _DissolveMaskShown = FindProperty(DissolveMaskShownName, props);
        _DissolveMaskTex = FindProperty(DissolveMaskTexName, props);

        _DistortBlock = FindProperty(DistortBlockName, props);
        _DistortShown = FindProperty(DistortShownName, props);
        _DistortTex = FindProperty(DistortTexName, props);
        _ForceX = FindProperty(ForceXName, props);
        _ForceY = FindProperty(ForceYName, props);
        _USpeed_distort = FindProperty(DistortUSpeedName, props);
        _VSpeed_distort = FindProperty(DistortVSpeedName, props);

        _DistortMask = FindProperty(DistortMaskName, props);
        _DistortMaskShown = FindProperty(DistortMaskShownName, props);
        _DistortMaskTex = FindProperty(DistortMaskTexName, props);


    }
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties);
        matEditor = materialEditor;
        
        Material mat = materialEditor.target as Material;
        if (firstTimeApply)
        {
            int renderQueue = mat.renderQueue;

            
          
            blendMode = CheckBlendMode(mat);
            SetupMaterialWithBlendMode(mat, blendMode);
            mat.renderQueue = renderQueue;
            firstTimeApply = false;
        }
        ShaderPropertiesGUI(mat, properties);
        GUILayout.Label(Styles.advancedOptionsText, EditorStyles.boldLabel);
        materialEditor.RenderQueueField();
      
        EditorGUILayout.Space();
      



        //base.OnGUI(materialEditor, properties);
    }
    public void ShaderPropertiesGUI(Material material,MaterialProperty[] props)
    {
        EditorGUIUtility.labelWidth = 0f;
        EditorGUI.BeginChangeCheck();
        {
            BlendModePopup();
            if (((CustomBlendMode)material.GetFloat(sOptionName) == CustomBlendMode.Cutout))
            {
                matEditor.ShaderProperty(_AlphaCutout, Styles.AlphaCutoutText.text, MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
            }
            matEditor.ShaderProperty(_TwoSide, Styles.TwoSideText.text);
            matEditor.ShaderProperty(_FogMode, Styles.FogModeText.text);
            matEditor.TextureProperty(_Diffuse, Styles.DiffuseText.text);
            matEditor.ShaderProperty(_DiffuseRo, Styles.DiffuseRoText.text);
            matEditor.ShaderProperty(_DiffuseAng, Styles.DiffuseAngText.text);
            matEditor.ShaderProperty(_Color, Styles.ColorText.text);
            matEditor.ShaderProperty(_Brightness, Styles.BrightnessText.text);
            matEditor.ShaderProperty(_Uspeed, Styles.UspeedText.text);
            matEditor.ShaderProperty(_Vspeed, Styles.VspeedText.text);

            //diffusemask
            
            EditorGUILayout.BeginVertical("Button");
            
            {
                float nval;
                EditorGUI.BeginChangeCheck();

                nval = EditorGUILayout.ToggleLeft(Styles.DiffuseMaskBlockText, _DiffuseMask.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                if (EditorGUI.EndChangeCheck())
                {
                    _DiffuseMask.floatValue = nval;

                }
            }
            if (_DiffuseMask.floatValue == 1)
            {
                material.EnableKeyword("_DIFFUSEMASK_ON");
                
                Rect rect = GUILayoutUtility.GetLastRect();
                rect.x += EditorGUIUtility.currentViewWidth - 47;
                EditorGUI.BeginChangeCheck();
                float nval = EditorGUI.Foldout(rect, _DiffuseMaskShown.floatValue == 1, "") ? 1 : 0;
                if (EditorGUI.EndChangeCheck())
                {
                    _DiffuseMaskShown.floatValue = nval;
                }
                
            }
            else
            {
                material.DisableKeyword("_DIFFUSEMASK_ON");
                material.SetTexture("_DiffuseMaskTex", null);
            }
            if (_DiffuseMask.floatValue == 1 && _DiffuseMaskShown.floatValue == 1)
            {

                matEditor.TextureProperty(_DiffuseMaskTex, Styles.DiffuseMaskText.text);
                matEditor.ShaderProperty(_DiffuseMaskRo, Styles.DiffuseRoText.text);
                matEditor.ShaderProperty(_DiffuseMaskAng, Styles.DiffuseAngText.text);
                matEditor.ShaderProperty(_USpeed_diffusem, Styles.UspeedSLayerText.text);
                matEditor.ShaderProperty(_VSpeed_diffusem, Styles.VspeedSLayerText.text);



            }
            EditorGUILayout.EndVertical();

            //2layer
            EditorGUILayout.BeginVertical("Button");
            {
                float nval;
                EditorGUI.BeginChangeCheck();

                nval = EditorGUILayout.ToggleLeft(Styles.SLayerBlockText, _SLayer.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                if (EditorGUI.EndChangeCheck())
                {
                    _SLayer.floatValue = nval;
                    
                }
                
            }
            if (_SLayer.floatValue == 1)
            {
                material.EnableKeyword("_SECONDLAYERBLOCK_ON");
                Rect rect = GUILayoutUtility.GetLastRect();
                rect.x += EditorGUIUtility.currentViewWidth - 47;

                EditorGUI.BeginChangeCheck();
                float nval = EditorGUI.Foldout(rect, _SLayerShown.floatValue == 1, "") ? 1 : 0;
                if (EditorGUI.EndChangeCheck())
                {
                    _SLayerShown.floatValue = nval;
                }
                if (_SLayerShown .floatValue== 1)
                {
                    matEditor.TextureProperty(_SLayerTex, Styles.SLayerText.text); 
                    matEditor.ShaderProperty(_SLayerRo, Styles.SLayerRoText.text);
                    matEditor.ShaderProperty(_SLayerAng, Styles.SLayerAngText.text);
                    matEditor.ShaderProperty(_SLayerColor, Styles.SLayerColorText.text);
                    matEditor.ShaderProperty(_SLayerBrightness, Styles.SLayerBrightnessText.text);
                    matEditor.ShaderProperty(_SLayerUspeed, Styles.UspeedText.text);
                    matEditor.ShaderProperty(_SLayerVspeed, Styles.VspeedText.text);

                    //2layermask
                    EditorGUILayout.BeginVertical("Button");
                    {
                        float m_nval;
                        EditorGUI.BeginChangeCheck();

                        m_nval = EditorGUILayout.ToggleLeft(Styles.SlayerMaskBlockText, _SLayerMask.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                        if (EditorGUI.EndChangeCheck())
                        {
                            _SLayerMask.floatValue = m_nval;

                        }
                    }
                    if (_SLayerMask.floatValue == 1)
                    {
                        material.EnableKeyword("_SECONDLAYERMASK_ON");
                        Rect m_rect = GUILayoutUtility.GetLastRect();
                        m_rect.x += EditorGUIUtility.currentViewWidth - 47;
                        EditorGUI.BeginChangeCheck();
                        float m_nval = EditorGUI.Foldout(m_rect, _SLayerMaskShown.floatValue == 1, "") ? 1 : 0;
                        if (EditorGUI.EndChangeCheck())
                        {
                            _SLayerMaskShown.floatValue = m_nval;
                        }
                        if (_SLayerMaskShown.floatValue == 1)
                        {
                            matEditor.TextureProperty(_SLayerMaskTex, Styles.SlayerMaskTexText.text);
                            matEditor.ShaderProperty(_SLayerMaskRo, Styles.SlayerMaskRoText.text);
                            matEditor.ShaderProperty(_SLayerMaskAng, Styles.SlayerMaskAngText.text);
                            matEditor.ShaderProperty(_SLayerMaskUspeed, Styles.UspeedSlayerMaskText.text);
                            matEditor.ShaderProperty(_SLayerMaskVspeed, Styles.VspeedSlayerMaskText.text);
                        }
                    }
                    else
                    {
                        material.DisableKeyword("_SECONDLAYERMASK_ON");
                        material.SetTexture("_SecondLayerMaskTex", null);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                material.DisableKeyword("_SECONDLAYERBLOCK_ON");
                material.SetTexture("_SecondLayerTex", null);
            }
            
            EditorGUILayout.EndVertical();

            
            

            //Dissove
            EditorGUILayout.BeginVertical("Button");
            {
                float nval;
                EditorGUI.BeginChangeCheck();

                nval = EditorGUILayout.ToggleLeft(Styles.DissolveBlockText, _DissolveBlock.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                if (EditorGUI.EndChangeCheck())
                {
                    _DissolveBlock.floatValue = nval;
                   
                }
            }
            if (_DissolveBlock.floatValue == 1)
            {
                material.EnableKeyword("_DISSOLVEBLOCK_ON");
                Rect rect = GUILayoutUtility.GetLastRect();
                rect.x += EditorGUIUtility.currentViewWidth - 47;
                EditorGUI.BeginChangeCheck();
                float nval = EditorGUI.Foldout(rect, _DissolveShown.floatValue == 1, "") ? 1 : 0;
                if (EditorGUI.EndChangeCheck())
                {
                    _DissolveShown.floatValue = nval;
                }
                if (_DissolveShown.floatValue == 1)
                {
                    //matEditor.ShaderProperty(_DissolveVertex, Styles.DissolveVertexText.text);
                    matEditor.TextureProperty(_DissolveTex, Styles.DissolveTexText.text);
                    matEditor.ShaderProperty(_DissolveColor, Styles.DissolveColorText.text);
                    matEditor.ShaderProperty(_Dissolve, Styles.DissolveText.text);
                    matEditor.ShaderProperty(_DissolveSize, Styles.DissolveSizeText.text);
                    matEditor.ShaderProperty(_DissolveBrightness, Styles.DissolveBrightnessText.text);

                    //dissolve mask
                    EditorGUILayout.BeginVertical("Button");
                    {
                        float m_nval;
                        EditorGUI.BeginChangeCheck();

                        m_nval = EditorGUILayout.ToggleLeft(Styles.DissolveMaskText, _DissolveMask.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                        if (EditorGUI.EndChangeCheck())
                        {
                            _DissolveMask.floatValue = m_nval;

                        }
                    }
                    if (_DissolveMask.floatValue == 1)
                    {
                        material.EnableKeyword("_DISSOLVEMASK_ON");
                        Rect m_rect = GUILayoutUtility.GetLastRect();
                        m_rect.x += EditorGUIUtility.currentViewWidth - 47;
                        EditorGUI.BeginChangeCheck();
                        float m_nval = EditorGUI.Foldout(m_rect, _DissolveMaskShown.floatValue == 1, "") ? 1 : 0;
                        if (EditorGUI.EndChangeCheck())
                        {
                            _DissolveMaskShown.floatValue = m_nval;
                        }
                        if (_DissolveMaskShown.floatValue == 1)
                        {
                            matEditor.TextureProperty(_DissolveMaskTex, Styles.DissolveMaskTexText.text);
                        }
                    }
                    else
                    {
                        material.DisableKeyword("_DISSOLVEMASK_ON");
                        material.SetTexture("_DissolveMaskTex", null);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                material.DisableKeyword("_DISSOLVEBLOCK_ON");
                material.SetTexture("_DissolveTex", null);
            }
            EditorGUILayout.EndVertical();

            
            

            //distort
            EditorGUILayout.BeginVertical("Button");
            {
                float nval;
                EditorGUI.BeginChangeCheck();

                nval = EditorGUILayout.ToggleLeft(Styles.DistortBlockText, _DistortBlock.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                if (EditorGUI.EndChangeCheck())
                {
                    _DistortBlock.floatValue = nval;

                }
            }
            if (_DistortBlock.floatValue == 1)
            {
                material.EnableKeyword("_DISTORTBLOCK_ON");
                Rect rect = GUILayoutUtility.GetLastRect();
                rect.x += EditorGUIUtility.currentViewWidth - 47;
                EditorGUI.BeginChangeCheck();
                float nval = EditorGUI.Foldout(rect, _DistortShown.floatValue == 1, "") ? 1 : 0;
                if (EditorGUI.EndChangeCheck())
                {
                    _DistortShown.floatValue = nval;
                }
                if (_DistortShown.floatValue == 1)
                {
                    matEditor.TextureProperty(_DistortTex, Styles.DistortTexText.text);
                    matEditor.ShaderProperty(_ForceX, Styles.ForceXText.text);
                    matEditor.ShaderProperty(_ForceY, Styles.ForceYText.text);
                    matEditor.ShaderProperty(_USpeed_distort, Styles.DistortUSpeedText.text);
                    matEditor.ShaderProperty(_VSpeed_distort, Styles.DistortVSpeedText.text);

                    //distort mask
                    EditorGUILayout.BeginVertical("Button");
                    {
                        float m_nval;
                        EditorGUI.BeginChangeCheck();

                        m_nval = EditorGUILayout.ToggleLeft(Styles.DistortMaskText, _DistortMask.floatValue == 1, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 60)) ? 1 : 0;

                        if (EditorGUI.EndChangeCheck())
                        {
                            _DistortMask.floatValue = m_nval;

                        }
                    }
                    if (_DistortMask.floatValue == 1)
                    {
                        material.EnableKeyword("_DISTORTMASK_ON");
                        Rect m_rect = GUILayoutUtility.GetLastRect();
                        m_rect.x += EditorGUIUtility.currentViewWidth - 47;
                        EditorGUI.BeginChangeCheck();
                        float m_nval = EditorGUI.Foldout(m_rect, _DistortMaskShown.floatValue == 1, "") ? 1 : 0;
                        if (EditorGUI.EndChangeCheck())
                        {
                            _DistortMaskShown.floatValue = m_nval;
                        }
                        if (_DistortMaskShown.floatValue == 1)
                        {
                            matEditor.TextureProperty(_DistortMaskTex, Styles.DistortMaskTexText.text);

                        }
                    }
                    else
                    {
                        material.DisableKeyword("_DISTORTMASK_ON");
                        material.SetTexture("_DistortMaskTex", null);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                material.DisableKeyword("_DISTORTBLOCK_ON");
                material.SetTexture("_DistortTex", null);
            }
            EditorGUILayout.EndVertical();

            
           

           

        }
        if (EditorGUI.EndChangeCheck())
        {
            foreach (var obj in option.targets)
            {
                SetupMaterialWithBlendMode((Material)obj, (CustomBlendMode)option.floatValue);
            }
        }
        EditorGUILayout.Space();
        
    }
    public void DoDiffuseMask(Material material,Color bCol)
    {
        
    }
    public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
    {
        base.AssignNewShaderToMaterial(material, oldShader, newShader);

        CustomBlendMode _bmMain = CheckBlendMode(material);

        SetupMaterialWithBlendMode(material, _bmMain);
    }

    CustomBlendMode CheckBlendMode(Material material)
    {
        CustomBlendMode _bmMain = CustomBlendMode.BlendNormal;
        if (material.HasProperty(sOptionName))
        {
            _bmMain = (CustomBlendMode)material.GetInt(sOptionName);
        }
        if (material.HasProperty(sOldName))
        {
            CustomBlendMode _bmGuess = CustomBlendMode.BlendNormal;
            int _dstRGBBlendMode = material.GetInt(sOldName);
            if (_dstRGBBlendMode == (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha)
            {
                _bmGuess = CustomBlendMode.BlendNormal;
            }
            if (_dstRGBBlendMode == (int)UnityEngine.Rendering.BlendMode.One)
            {
                _bmGuess = CustomBlendMode.BlendAdd;
            }
            if (_bmGuess != _bmMain && (_bmMain == CustomBlendMode.BlendAdd || _bmMain == CustomBlendMode.BlendNormal)) //no set yet!
            {
                _bmMain = _bmGuess;
            }
        }
        return _bmMain;
    }
    public void BlendModePopup()
    {
        EditorGUI.showMixedValue = option.hasMixedValue;
        var mode = (CustomBlendMode)option.floatValue;

        EditorGUI.BeginChangeCheck();
        mode = (CustomBlendMode)EditorGUILayout.Popup("Blend Set", (int)mode, sOptions);
        if (EditorGUI.EndChangeCheck())
        {
            matEditor.RegisterPropertyChangeUndo("Blend set");
            option.floatValue = (float)mode;
        }
        EditorGUI.showMixedValue = false;

    }

    public static void SetupMaterialWithBlendMode(Material material, CustomBlendMode blendMode)
    {
        switch (blendMode)
        {
            case CustomBlendMode.Opaque:
                material.SetOverrideTag("RenderType", "");
                material.SetInt(sOptionName, (int)blendMode);
                material.SetInt(sSrcRGBName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(sSrcAlphaName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(sDestRGBName, (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt(sDestAlphaName, (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt(ZWriteName, 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case CustomBlendMode.BlendNormal:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt(sOptionName, (int)blendMode);
                material.SetInt(sSrcRGBName, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt(sSrcAlphaName, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt(sDestRGBName, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt(sDestAlphaName, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt(ZWriteName, 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                if (material.HasProperty(sOldName))
                {
                    material.SetInt(sOldName, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                }
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case CustomBlendMode.BlendAdd:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt(sOptionName, (int)blendMode);
                material.SetInt(sSrcRGBName, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt(sSrcAlphaName, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt(sDestRGBName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(sDestAlphaName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(ZWriteName, 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                if (material.HasProperty(sOldName))
                {
                    material.SetInt(sOldName, (int)UnityEngine.Rendering.BlendMode.One);
                }
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case CustomBlendMode.Cutout:
                material.SetOverrideTag("RenderType", "TransparentCutout");
                material.SetInt(sSrcRGBName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(sSrcAlphaName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(sDestRGBName, (int)UnityEngine.Rendering.BlendMode.Zero);              
                material.SetInt(sDestAlphaName, (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt(ZWriteName, 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                break;
            case CustomBlendMode.BlendNormalUI:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt(sOptionName, (int)blendMode);
                material.SetInt(sSrcRGBName, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt(sSrcAlphaName, (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt(sDestRGBName, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt(sDestAlphaName, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt(ZWriteName, 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                if (material.HasProperty(sOldName))
                {
                    material.SetInt(sOldName, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                }
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case CustomBlendMode.BlendAddUI:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt(sOptionName, (int)blendMode);
                material.SetInt(sSrcRGBName, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt(sSrcAlphaName, (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt(sDestRGBName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(sDestAlphaName, (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt(ZWriteName, 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                if (material.HasProperty(sOldName))
                {
                    material.SetInt(sOldName, (int)UnityEngine.Rendering.BlendMode.One);
                }
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
        }
    }

}
