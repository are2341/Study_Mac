Shader "Etc/E01/EtcE01Example_220" {
	Properties {
		[MainColor] _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		[MainTexture] _MainTexture("MainTexture", 2D) = "white" { }
	}
	SubShader {
		Tags {
			"RenderType" = "Opaque" "RederQueue" = "Geometry+1" "RenderPipeline" = "UniversalPipeline"
		}
		Pass {
			HLSLPROGRAM
			#pragma vertex VSMain
			#pragma fragment PSMain

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			/** 입력 */
			struct STInput {
				float3 m_stPos: POSITION;
			};

			/** 출력 */
			struct STOutput {
				float4 m_stPos: SV_POSITION;
			};

			/** 정점 쉐이더 */
			STOutput VSMain(STInput a_stInput) {
				STOutput stOutput = (STOutput)0;
				stOutput.m_stPos = TransformObjectToHClip(a_stInput.m_stPos);

				return stOutput;
			}

			/** 픽셀 쉐이더 */
			float4 PSMain(STOutput a_stInput) : SV_TARGET {
				return float4(1.0, 0.0, 0.0, 1.0);
			}
			ENDHLSL
		}
	}
}
