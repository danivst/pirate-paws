Shader "Unlit/DepthMask"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // Render the mask after regular geometry, but before masked geometry and transparent things.
        Tags { "Queue" = "Geometry+10" }

        // Don't draw in the RGBA channels; just the depth buffer
        ColorMask 0
        ZWrite On

        // Use stencil to mask specific layers
        Stencil
        {
            Ref 1          // Reference value for the stencil buffer
            Comp Always    // Always pass the stencil test when rendering the depth mask
            Pass Replace   // Write the reference value (1) into the stencil buffer
        }

        Pass {}
    }
}
