#version 330 core
uniform vec4 color;
uniform samplerCube cubeMap;
layout (location = 0) out vec4 oColor;

varying vec3 rayDirection;

void main()
{
    oColor = texture(cubeMap, rayDirection);
}