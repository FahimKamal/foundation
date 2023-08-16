#if PRIME_TWEEN_DOTWEEN_ADAPTER
// This file is generated by CodeGenerator.cs
using JetBrains.Annotations;
using System;

namespace PrimeTween {
    [PublicAPI]
    public static partial class DOTweenAdapter {

        public static Tween DOShadowStrength([NotNull] this UnityEngine.Light target, Single endValue, float duration) => Tween.LightShadowStrength(target, endValue, duration, defaultDotweenEase);

        public static Tween DOIntensity([NotNull] this UnityEngine.Light target, Single endValue, float duration) => Tween.LightIntensity(target, endValue, duration, defaultDotweenEase);

        public static Tween DOColor([NotNull] this UnityEngine.Light target, UnityEngine.Color endValue, float duration) => Tween.LightColor(target, endValue, duration, defaultDotweenEase);

        public static Tween DOOrthoSize([NotNull] this UnityEngine.Camera target, Single endValue, float duration) => Tween.CameraOrthographicSize(target, endValue, duration, defaultDotweenEase);

        public static Tween DOColor([NotNull] this UnityEngine.Camera target, UnityEngine.Color endValue, float duration) => Tween.CameraBackgroundColor(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAspect([NotNull] this UnityEngine.Camera target, Single endValue, float duration) => Tween.CameraAspect(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFarClipPlane([NotNull] this UnityEngine.Camera target, Single endValue, float duration) => Tween.CameraFarClipPlane(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFieldOfView([NotNull] this UnityEngine.Camera target, Single endValue, float duration) => Tween.CameraFieldOfView(target, endValue, duration, defaultDotweenEase);

        public static Tween DONearClipPlane([NotNull] this UnityEngine.Camera target, Single endValue, float duration) => Tween.CameraNearClipPlane(target, endValue, duration, defaultDotweenEase);

        public static Tween DOPixelRect([NotNull] this UnityEngine.Camera target, UnityEngine.Rect endValue, float duration) => Tween.CameraPixelRect(target, endValue, duration, defaultDotweenEase);

        public static Tween DORect([NotNull] this UnityEngine.Camera target, UnityEngine.Rect endValue, float duration) => Tween.CameraRect(target, endValue, duration, defaultDotweenEase);




        public static Tween DOMove([NotNull] this UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration) => Tween.Position(target, endValue, duration, defaultDotweenEase);

        public static Tween DOMoveX([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.PositionX(target, endValue, duration, defaultDotweenEase);

        public static Tween DOMoveY([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.PositionY(target, endValue, duration, defaultDotweenEase);

        public static Tween DOMoveZ([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.PositionZ(target, endValue, duration, defaultDotweenEase);

        public static Tween DOLocalMove([NotNull] this UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration) => Tween.LocalPosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DOLocalMoveX([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.LocalPositionX(target, endValue, duration, defaultDotweenEase);

        public static Tween DOLocalMoveY([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.LocalPositionY(target, endValue, duration, defaultDotweenEase);

        public static Tween DOLocalMoveZ([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.LocalPositionZ(target, endValue, duration, defaultDotweenEase);

        public static Tween DORotateQuaternion([NotNull] this UnityEngine.Transform target, UnityEngine.Quaternion endValue, float duration) => Tween.Rotation(target, endValue, duration, defaultDotweenEase);

        public static Tween DOLocalRotateQuaternion([NotNull] this UnityEngine.Transform target, UnityEngine.Quaternion endValue, float duration) => Tween.LocalRotation(target, endValue, duration, defaultDotweenEase);

        public static Tween DOScale([NotNull] this UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration) => Tween.LocalScale(target, endValue, duration, defaultDotweenEase);

        public static Tween DOScaleX([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.LocalScaleX(target, endValue, duration, defaultDotweenEase);

        public static Tween DOScaleY([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.LocalScaleY(target, endValue, duration, defaultDotweenEase);

        public static Tween DOScaleZ([NotNull] this UnityEngine.Transform target, Single endValue, float duration) => Tween.LocalScaleZ(target, endValue, duration, defaultDotweenEase);

        public static Tween DOColor([NotNull] this UnityEngine.SpriteRenderer target, UnityEngine.Color endValue, float duration) => Tween.Color(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFade([NotNull] this UnityEngine.SpriteRenderer target, Single endValue, float duration) => Tween.Alpha(target, endValue, duration, defaultDotweenEase);

        #if !UNITY_2019_1_OR_NEWER || UNITY_UGUI_INSTALLED
        public static Tween DOValue([NotNull] this UnityEngine.UI.Slider target, Single endValue, float duration) => Tween.UISliderValue(target, endValue, duration, defaultDotweenEase);

        public static Tween DONormalizedPos([NotNull] this UnityEngine.UI.ScrollRect target, UnityEngine.Vector2 endValue, float duration) => Tween.UINormalizedPosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DOHorizontalNormalizedPos([NotNull] this UnityEngine.UI.ScrollRect target, Single endValue, float duration) => Tween.UIHorizontalNormalizedPosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DOVerticalNormalizedPos([NotNull] this UnityEngine.UI.ScrollRect target, Single endValue, float duration) => Tween.UIVerticalNormalizedPosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DOPivotX([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIPivotX(target, endValue, duration, defaultDotweenEase);

        public static Tween DOPivotY([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIPivotY(target, endValue, duration, defaultDotweenEase);

        public static Tween DOPivot([NotNull] this UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration) => Tween.UIPivot(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorMax([NotNull] this UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration) => Tween.UIAnchorMax(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorMin([NotNull] this UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration) => Tween.UIAnchorMin(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPos3D([NotNull] this UnityEngine.RectTransform target, UnityEngine.Vector3 endValue, float duration) => Tween.UIAnchoredPosition3D(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPos3DX([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIAnchoredPosition3DX(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPos3DY([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIAnchoredPosition3DY(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPos3DZ([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIAnchoredPosition3DZ(target, endValue, duration, defaultDotweenEase);

        public static Tween DOScale([NotNull] this UnityEngine.UI.Shadow target, UnityEngine.Vector2 endValue, float duration) => Tween.UIEffectDistance(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFade([NotNull] this UnityEngine.UI.Shadow target, Single endValue, float duration) => Tween.Alpha(target, endValue, duration, defaultDotweenEase);

        public static Tween DOColor([NotNull] this UnityEngine.UI.Shadow target, UnityEngine.Color endValue, float duration) => Tween.Color(target, endValue, duration, defaultDotweenEase);

        public static Tween DOPreferredSize([NotNull] this UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration) => Tween.UIPreferredSize(target, endValue, duration, defaultDotweenEase);



        public static Tween DOFlexibleSize([NotNull] this UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration) => Tween.UIFlexibleSize(target, endValue, duration, defaultDotweenEase);



        public static Tween DOMinSize([NotNull] this UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration) => Tween.UIMinSize(target, endValue, duration, defaultDotweenEase);



        public static Tween DOColor([NotNull] this UnityEngine.UI.Graphic target, UnityEngine.Color endValue, float duration) => Tween.Color(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPos([NotNull] this UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration) => Tween.UIAnchoredPosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPosX([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIAnchoredPositionX(target, endValue, duration, defaultDotweenEase);

        public static Tween DOAnchorPosY([NotNull] this UnityEngine.RectTransform target, Single endValue, float duration) => Tween.UIAnchoredPositionY(target, endValue, duration, defaultDotweenEase);

        public static Tween DOSizeDelta([NotNull] this UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration) => Tween.UISizeDelta(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFade([NotNull] this UnityEngine.CanvasGroup target, Single endValue, float duration) => Tween.Alpha(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFade([NotNull] this UnityEngine.UI.Graphic target, Single endValue, float duration) => Tween.Alpha(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFillAmount([NotNull] this UnityEngine.UI.Image target, Single endValue, float duration) => Tween.UIFillAmount(target, endValue, duration, defaultDotweenEase);

        #endif
        #if !UNITY_2019_1_OR_NEWER || PHYSICS_MODULE_INSTALLED
        public static Tween DOMove([NotNull] this UnityEngine.Rigidbody target, UnityEngine.Vector3 endValue, float duration) => Tween.RigidbodyMovePosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DORotate([NotNull] this UnityEngine.Rigidbody target, UnityEngine.Quaternion endValue, float duration) => Tween.RigidbodyMoveRotation(target, endValue, duration, defaultDotweenEase);

        #endif
        #if !UNITY_2019_1_OR_NEWER || PHYSICS2D_MODULE_INSTALLED
        public static Tween DOMove([NotNull] this UnityEngine.Rigidbody2D target, UnityEngine.Vector2 endValue, float duration) => Tween.RigidbodyMovePosition(target, endValue, duration, defaultDotweenEase);

        public static Tween DORotate([NotNull] this UnityEngine.Rigidbody2D target, Single endValue, float duration) => Tween.RigidbodyMoveRotation(target, endValue, duration, defaultDotweenEase);

        #endif
        public static Tween DOColor([NotNull] this UnityEngine.Material target, UnityEngine.Color endValue, float duration) => Tween.MaterialColor(target, endValue, duration, defaultDotweenEase);

        public static Tween DOFade([NotNull] this UnityEngine.Material target, Single endValue, float duration) => Tween.MaterialAlpha(target, endValue, duration, defaultDotweenEase);

        public static Tween DOOffset([NotNull] this UnityEngine.Material target, UnityEngine.Vector2 endValue, float duration) => Tween.MaterialMainTextureOffset(target, endValue, duration, defaultDotweenEase);

        public static Tween DOTiling([NotNull] this UnityEngine.Material target, UnityEngine.Vector2 endValue, float duration) => Tween.MaterialMainTextureScale(target, endValue, duration, defaultDotweenEase);

        #if !UNITY_2019_1_OR_NEWER || AUDIO_MODULE_INSTALLED
        public static Tween DOFade([NotNull] this UnityEngine.AudioSource target, Single endValue, float duration) => Tween.AudioVolume(target, endValue, duration, defaultDotweenEase);

        public static Tween DOPitch([NotNull] this UnityEngine.AudioSource target, Single endValue, float duration) => Tween.AudioPitch(target, endValue, duration, defaultDotweenEase);


        #endif
    }
}
#endif