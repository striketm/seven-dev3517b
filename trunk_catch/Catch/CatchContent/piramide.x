xof 0303txt 0032

Frame pyramid10uRFrame
{
    FrameTransformMatrix
    {
        1.000000,0.000000,0.000000,0.000000,
        0.000000,1.000000,0.000000,0.000000,
        0.000000,0.000000,1.000000,0.000000,
        0.000000,0.000000,0.000000,1.000000;;
    }

    Mesh pyramid10uRMesh
    {
        12;
        10.000000;0.000000;-10.000000;,
        10.000000;0.000000;10.000000;,
        0.000000;20.000000;0.000000;,
        -10.000000;0.000000;-10.000000;,
        10.000000;0.000000;-10.000000;,
        0.000000;20.000000;0.000000;,
        -10.000000;0.000000;10.000000;,
        -10.000000;0.000000;-10.000000;,
        0.000000;20.000000;0.000000;,
        10.000000;0.000000;10.000000;,
        -10.000000;0.000000;10.000000;,
        0.000000;20.000000;0.000000;;
        4;
        3;0,2,1;,
        3;3,5,4;,
        3;6,8,7;,
        3;9,11,10;;

        MeshNormals
        {
            4;
            0.894427;0.447214;0.000000;,
            0.000000;0.447214;-0.894427;,
            -0.894427;0.447214;0.000000;,
            0.000000;0.447214;0.894427;;
            4;
            3;0,0,0;,
            3;1,1,1;,
            3;2,2,2;,
            3;3,3,3;;
        }

        MeshTextureCoords
        {
            12;
            -0.003837;1.007675;,
            1.000000;1.000000;,
            0.504967;0.007675;,
            0.000000;1.000000;,
            0.996163;1.007675;,
            0.504966;0.003837;,
            0.000000;1.000000;,
            1.000000;1.000000;,
            0.501129;0.003837;,
            0.000000;1.000000;,
            1.000000;1.000000;,
            0.504967;0.007675;;
        }

        MeshMaterialList
        {
            1;
            4;
            0,
            0,
            0,
            0;

            Material PyramidMat
            {
                0.800000;0.800000;0.800000;1.000000;;
                0.000000;
                0.000000;0.000000;0.000000;;
                0.000000;0.000000;0.000000;;

                TextureFileName
                {
                    "barreira_vermelha.png";
                }
            }
        }
    }
}
