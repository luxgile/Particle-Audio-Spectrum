using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioVisualization))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class AudioVisualizationEditor : Editor
{

    SerializedProperty _AudioVisualizationType;
    SerializedProperty _ParticleEffectType;

    SerializedProperty rotation;
    SerializedProperty rotateSensitivityRange;
    SerializedProperty rotateSensitivity;
    SerializedProperty rotateAmmount;

    SerializedProperty scale;
    SerializedProperty scaleSensitivityRange;
    SerializedProperty scaleSensitivity;
    SerializedProperty scaleAmmount;
    SerializedProperty scaleSmooth;

    SerializedProperty color;
    SerializedProperty minColor;
    SerializedProperty maxColor;
    SerializedProperty inverted_minColor;
    SerializedProperty inverted_maxColor;
    SerializedProperty maxColorAmmount;
    SerializedProperty mulColorAmmount;
    SerializedProperty inverted_maxColorAmmount;
    SerializedProperty inverted_mulColorAmmount;

    SerializedProperty movement;
    SerializedProperty movXAmmount;
    SerializedProperty movXSpeed;
    SerializedProperty movYAmmount;
    SerializedProperty movYSpeed;

    SerializedProperty ps_Visualization;
    SerializedProperty parent_Visualization;

    SerializedProperty objectOffSet;
    SerializedProperty centered;

    SerializedProperty particleColor;
    SerializedProperty particleColorIntensity;
    SerializedProperty audioSensibility;
    SerializedProperty audioMultiplier;
    SerializedProperty audioMax;
    SerializedProperty audioSmooth;

    SerializedProperty duplicateInverted;
    SerializedProperty inverted_particleColor;
    SerializedProperty inverted_particleColorIntensity;
    SerializedProperty inverted_audioSensibility;
    SerializedProperty inverted_audioMultiplier;
    SerializedProperty inverted_audioMax;
    SerializedProperty inverted_audioSmooth;

    private bool showMainParticle = false;

    void OnEnable()
    {
        _AudioVisualizationType = serializedObject.FindProperty("_AudioVisualizationType");
        _ParticleEffectType = serializedObject.FindProperty("_ParticleEffectType");

        rotation = serializedObject.FindProperty("rotation");
        rotateSensitivityRange = serializedObject.FindProperty("rotateSensitivityRange");
        rotateSensitivity = serializedObject.FindProperty("rotateSensitivity");
        rotateAmmount = serializedObject.FindProperty("rotateAmmount");

        scale = serializedObject.FindProperty("scale");
        scaleSensitivityRange = serializedObject.FindProperty("scaleSensitivityRange");
        scaleSensitivity = serializedObject.FindProperty("scaleSensitivity");
        scaleAmmount = serializedObject.FindProperty("scaleAmmount");
        scaleSmooth = serializedObject.FindProperty("scaleSmooth");

        color = serializedObject.FindProperty("color");
        minColor = serializedObject.FindProperty("minColor");
        maxColor = serializedObject.FindProperty("maxColor");
        inverted_minColor = serializedObject.FindProperty("inverted_minColor");
        inverted_maxColor = serializedObject.FindProperty("inverted_maxColor");
        maxColorAmmount = serializedObject.FindProperty("maxColorAmmount");
        mulColorAmmount = serializedObject.FindProperty("mulColorAmmount");
        inverted_maxColorAmmount = serializedObject.FindProperty("inverted_maxColorAmmount");
        inverted_mulColorAmmount = serializedObject.FindProperty("inverted_mulColorAmmount");

        movement = serializedObject.FindProperty("movement");
        movXAmmount = serializedObject.FindProperty("movXAmmount");
        movXSpeed = serializedObject.FindProperty("movXSpeed");
        movYAmmount = serializedObject.FindProperty("movYAmmount");
        movYSpeed = serializedObject.FindProperty("movYSpeed");

        ps_Visualization = serializedObject.FindProperty("ps_Visualization");
        parent_Visualization = serializedObject.FindProperty("parent_Visualization");

        objectOffSet = serializedObject.FindProperty("objectOffSet");
        centered = serializedObject.FindProperty("centered");

        particleColor = serializedObject.FindProperty("particleColor");
        particleColorIntensity = serializedObject.FindProperty("particleColorIntensity");
        audioSensibility = serializedObject.FindProperty("audioSensibility");
        audioMultiplier = serializedObject.FindProperty("audioMultiplier");
        audioMax = serializedObject.FindProperty("audioMax");
        audioSmooth = serializedObject.FindProperty("audioSmooth");

        duplicateInverted = serializedObject.FindProperty("duplicateInverted");
        inverted_particleColor = serializedObject.FindProperty("inverted_particleColor");
        inverted_particleColorIntensity = serializedObject.FindProperty("inverted_particleColorIntensity");
        inverted_audioSensibility = serializedObject.FindProperty("inverted_audioSensibility");
        inverted_audioMultiplier = serializedObject.FindProperty("inverted_audioMultiplier");
        inverted_audioMax = serializedObject.FindProperty("inverted_audioMax");
        inverted_audioSmooth = serializedObject.FindProperty("inverted_audioSmooth");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Instantiate Metod");
        EditorGUILayout.PropertyField(_AudioVisualizationType);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("How music affects particles");
        EditorGUILayout.PropertyField(_ParticleEffectType);

        if (_ParticleEffectType.intValue == 2)
        {
            EditorGUILayout.LabelField("Warning: Don't set multiplier too high when 'Life Time' is enabled");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.PrefixLabel("Particle System prefab");
        EditorGUILayout.PropertyField(ps_Visualization, GUIContent.none);

        EditorGUILayout.PrefixLabel("Parent gameobject");
        EditorGUILayout.PropertyField(parent_Visualization, GUIContent.none);

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();


        EditorGUILayout.PropertyField(rotation, GUIContent.none);
        if (rotation.boolValue)
        {
            EditorGUILayout.LabelField("Rotate parent: YES");

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Size of the sensitivity range (0-512)");
            EditorGUILayout.PropertyField(rotateSensitivityRange, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Minimum value to affect particles");
            EditorGUILayout.PropertyField(rotateSensitivity, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Multiplier value in local space");
            EditorGUILayout.PropertyField(rotateAmmount, GUIContent.none);
        }
        else
        {
            EditorGUILayout.LabelField("Rotate parent: NO");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(scale, GUIContent.none);
        if (scale.boolValue)
        {
            EditorGUILayout.LabelField("Scale parent: YES");

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Size of the sensitivity range (0-512)");
            EditorGUILayout.PropertyField(scaleSensitivityRange, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Minimum value to affect particles");
            EditorGUILayout.PropertyField(scaleSensitivity, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Multiplier value in local space");
            EditorGUILayout.PropertyField(scaleAmmount, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Smooth");
            EditorGUILayout.PropertyField(scaleSmooth, GUIContent.none);
        }
        else
        {
            EditorGUILayout.LabelField("Scale parent: NO");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(color, GUIContent.none);

        if (color.boolValue)
        {
            EditorGUILayout.LabelField("Music affects color: YES");

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Minimum Color");
            EditorGUILayout.PropertyField(minColor, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Maximum Color");
            EditorGUILayout.PropertyField(maxColor, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Multiplier value");
            EditorGUILayout.PropertyField(mulColorAmmount, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Maximum Value (From 1 to 0)");
            EditorGUILayout.PropertyField(maxColorAmmount, GUIContent.none);
            EditorGUILayout.Separator();

            if (duplicateInverted.boolValue)
            {
                EditorGUILayout.LabelField("Effects to inverted particles");
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Minimum Color");
                EditorGUILayout.PropertyField(inverted_minColor, GUIContent.none);
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Maximum Color");
                EditorGUILayout.PropertyField(inverted_maxColor, GUIContent.none);
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Multiplier value");
                EditorGUILayout.PropertyField(inverted_mulColorAmmount, GUIContent.none);
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Maximum Value (From 1 to 0)");
                EditorGUILayout.PropertyField(inverted_maxColorAmmount, GUIContent.none);
                EditorGUILayout.Separator();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Music affects color: NO");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(movement, GUIContent.none);
        if (movement.boolValue)
        {
            EditorGUILayout.LabelField("Move parent: YES");
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Movement multiplier in X axis");
            EditorGUILayout.PropertyField(movXAmmount, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Movement speed in X axis");
            EditorGUILayout.PropertyField(movXSpeed, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Movement multiplier in Y axis");
            EditorGUILayout.PropertyField(movYAmmount, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Movement speed in Y axis");
            EditorGUILayout.PropertyField(movYSpeed, GUIContent.none);
            EditorGUILayout.Separator();
        }
        else
        {
            EditorGUILayout.LabelField("Move parent: NO");
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Distance between particle systems");
        EditorGUILayout.PropertyField(objectOffSet, GUIContent.none);

        EditorGUILayout.PropertyField(centered, GUIContent.none);
        if (centered.boolValue) { EditorGUILayout.LabelField("Particles will be instantiated from the center"); }
        else { EditorGUILayout.LabelField("Particles will be instantiated from the side"); }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Main particles start color");
        EditorGUILayout.PropertyField(particleColor, GUIContent.none);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Minimum value to be affected");
        EditorGUILayout.PropertyField(audioSensibility, GUIContent.none);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Maximum value to be affected");
        EditorGUILayout.PropertyField(audioMax, GUIContent.none);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Music reaction multiplier");
        EditorGUILayout.PropertyField(audioMultiplier, GUIContent.none);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Smooth value");
        EditorGUILayout.PropertyField(audioSmooth, GUIContent.none);

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.PropertyField(duplicateInverted, GUIContent.none);

        if (duplicateInverted.boolValue)
        {
            EditorGUILayout.LabelField("Instantiate inverted particles: YES");

            EditorGUILayout.LabelField("Inverted particles start color");
            EditorGUILayout.PropertyField(inverted_particleColor, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Minimum value to be affected");
            EditorGUILayout.PropertyField(inverted_audioSensibility, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Maximum value to be affected");
            EditorGUILayout.PropertyField(inverted_audioMax, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Music reaction multiplier");
            EditorGUILayout.PropertyField(inverted_audioMultiplier, GUIContent.none);
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Smooth value");
            EditorGUILayout.PropertyField(inverted_audioSmooth, GUIContent.none);
        }
        else
        {
            EditorGUILayout.LabelField("Instantiate inverted particles: NO");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
