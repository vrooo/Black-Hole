#version 330 core
layout (location = 0) in vec3 position;

uniform mat4 viewMatrix;
uniform mat4 invViewMatrix;
uniform float aspectRatio;

varying vec3 rayDirection;
varying vec3 camPos;

void main()
{
    rayDirection = (invViewMatrix * vec4(position.x * aspectRatio, position.y, position.z, 0.0f)).xyz;
    camPos = invViewMatrix[3].xyz;
    gl_Position = vec4(position, 1.0f);
}