// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33176,y:32603,varname:node_3138,prsc:2|emission-2050-OUT,alpha-8729-R;n:type:ShaderForge.SFN_Tex2d,id:6386,x:32484,y:32464,ptovrint:False,ptlb:node_6386,ptin:_node_6386,varname:node_6386,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4826-UVOUT;n:type:ShaderForge.SFN_Panner,id:4826,x:32282,y:32489,varname:node_4826,prsc:2,spu:-0.5,spv:1|UVIN-9923-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9923,x:32070,y:32489,varname:node_9923,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:7113,x:32322,y:33027,ptovrint:False,ptlb:node_7113,ptin:_node_7113,varname:node_7113,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8004-UVOUT;n:type:ShaderForge.SFN_Panner,id:8004,x:32118,y:32877,varname:node_8004,prsc:2,spu:0.2,spv:0.5|UVIN-7518-UVOUT,DIST-8321-R;n:type:ShaderForge.SFN_TexCoord,id:7518,x:31660,y:32647,varname:node_7518,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:8321,x:31883,y:33056,ptovrint:False,ptlb:node_8321,ptin:_node_8321,varname:node_8321,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9096-UVOUT;n:type:ShaderForge.SFN_Panner,id:9096,x:31707,y:33076,varname:node_9096,prsc:2,spu:-0.2,spv:0.5|UVIN-2875-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2875,x:31458,y:33027,varname:node_2875,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:2050,x:33024,y:32699,varname:node_2050,prsc:2|A-4129-RGB,B-6127-OUT;n:type:ShaderForge.SFN_Tex2d,id:4129,x:32948,y:32550,ptovrint:False,ptlb:node_4129,ptin:_node_4129,varname:node_4129,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-461-OUT;n:type:ShaderForge.SFN_TexCoord,id:104,x:32096,y:32682,varname:node_104,prsc:2,uv:3;n:type:ShaderForge.SFN_Panner,id:3081,x:32343,y:32670,varname:node_3081,prsc:2,spu:0.3,spv:0.3|UVIN-104-UVOUT;n:type:ShaderForge.SFN_Add,id:461,x:32758,y:32550,varname:node_461,prsc:2|A-6386-R,B-3081-UVOUT;n:type:ShaderForge.SFN_Vector3,id:3150,x:32648,y:32934,varname:node_3150,prsc:2,v1:1,v2:0.4726167,v3:0.2352941;n:type:ShaderForge.SFN_Multiply,id:6127,x:32777,y:32771,varname:node_6127,prsc:2|A-7113-RGB,B-3150-OUT,C-9518-OUT,D-8729-R;n:type:ShaderForge.SFN_Vector1,id:9518,x:32514,y:33114,varname:node_9518,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:8729,x:32789,y:33016,ptovrint:False,ptlb:node_8729,ptin:_node_8729,varname:node_8729,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:6386-7113-8321-4129-8729;pass:END;sub:END;*/

Shader "Shader Forge/Player_ Worrior_niutou_nengliangzhao_Eeffecst" {
    Properties {
        _node_6386 ("node_6386", 2D) = "white" {}
        _node_7113 ("node_7113", 2D) = "white" {}
        _node_8321 ("node_8321", 2D) = "white" {}
        _node_4129 ("node_4129", 2D) = "white" {}
        _node_8729 ("node_8729", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_6386; uniform float4 _node_6386_ST;
            uniform sampler2D _node_7113; uniform float4 _node_7113_ST;
            uniform sampler2D _node_8321; uniform float4 _node_8321_ST;
            uniform sampler2D _node_4129; uniform float4 _node_4129_ST;
            uniform sampler2D _node_8729; uniform float4 _node_8729_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord3 : TEXCOORD3;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv3 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv3 = v.texcoord3;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_1176 = _Time + _TimeEditor;
                float2 node_4826 = (i.uv0+node_1176.g*float2(-0.5,1));
                float4 _node_6386_var = tex2D(_node_6386,TRANSFORM_TEX(node_4826, _node_6386));
                float2 node_461 = (_node_6386_var.r+(i.uv3+node_1176.g*float2(0.3,0.3)));
                float4 _node_4129_var = tex2D(_node_4129,TRANSFORM_TEX(node_461, _node_4129));
                float2 node_9096 = (i.uv0+node_1176.g*float2(-0.2,0.5));
                float4 _node_8321_var = tex2D(_node_8321,TRANSFORM_TEX(node_9096, _node_8321));
                float2 node_8004 = (i.uv0+_node_8321_var.r*float2(0.2,0.5));
                float4 _node_7113_var = tex2D(_node_7113,TRANSFORM_TEX(node_8004, _node_7113));
                float4 _node_8729_var = tex2D(_node_8729,TRANSFORM_TEX(i.uv0, _node_8729));
                float3 emissive = (_node_4129_var.rgb+(_node_7113_var.rgb*float3(1,0.4726167,0.2352941)*0.5*_node_8729_var.r));
                float3 finalColor = emissive;
                return fixed4(finalColor,_node_8729_var.r);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
