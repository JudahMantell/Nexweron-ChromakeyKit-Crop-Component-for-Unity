namespace Nexweron.Core.MSK
{
	using System.Collections.Generic;
	using UnityEngine;

	public class CropAlpha : MSKComponentBase
	{
		[SerializeField, Range(0, 1)]
		private float _top = 0.0f;
		public float top {
			get { return _top; }
			set { if (SetStruct(ref _top, value)) _shaderMaterial.SetFloat("_Top", value); }
		}

		[SerializeField, Range(0, 1)]
		private float _bottom = 1.0f;
		public float bottom {
			get { return _bottom; }
			set { if (SetStruct(ref _bottom, value)) _shaderMaterial.SetFloat("_Bottom", value); }
		}

		[SerializeField, Range(0, 1)]
		private float _left = 0.0f;
		public float left {
			get { return _left; }
			set { if (SetStruct(ref _left, value)) _shaderMaterial.SetFloat("_Left", value); }
		}

		[SerializeField, Range(0, 1)]
		private float _right = 1.0f;
		public float right {
			get { return _right; }
			set { if (SetStruct(ref _right, value)) _shaderMaterial.SetFloat("_Right", value); }
		}

		private RenderTexture _rtM; //render texture Mask

		private List<string> _availableShaders = new List<string>() { @"MSK/Crop/BlendOff/CropAlpha" };
		public override List<string> GetAvailableShaders() {
			return _availableShaders;
		}

		public override void UpdateShaderProperties() {
			_shaderMaterial.SetFloat("_Top", _top);
			_shaderMaterial.SetFloat("_Bottom", _bottom);
			_shaderMaterial.SetFloat("_Left", _left);
			_shaderMaterial.SetFloat("_Right", _right);
		}

		public override void UpdateSourceTexture() {
			RenderTextureUtils.SetRenderTextureSize(ref _rtM, _mskController.sourceTexture.width, _mskController.sourceTexture.height);
		}

		public override RenderTexture GetRender(Texture src) {
			//_shaderMaterial.SetTexture("_MainTex", src);
			_rtM.DiscardContents();
			Graphics.Blit(src, _rtM, _shaderMaterial);
			return _rtM;
		}

		protected override void OnDestroy() {
			base.OnDestroy();
			if (_rtM != null) {
				DestroyImmediate(_rtM);
			}
		}
	}
}