#version 330 core
layout (location = 0) in vec3 position;

uniform mat4 viewMatrix;
uniform float aspectRatio;

varying vec3 rayDirection;

void main()
{
    //vec4 worldPos = modelMatrix * vec4(position.x, position.y, position.z, 1.0);
    //world = worldPos.xyz;
    //gl_Position = projMatrix * viewMatrix * worldPos;
    rayDirection = (viewMatrix * vec4(position.x * aspectRatio, position.y, position.z, 0.0f)).xyz;
    gl_Position = vec4(position, 1.0f);
}