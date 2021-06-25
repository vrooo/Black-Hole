#version 330 core
uniform vec4 color;
uniform samplerCube cubeMap;
layout (location = 0) out vec4 oColor;

void main()
{
    //oColor = color;
    oColor = texture(cubeMap, gl_FragCoord.xyz);
}