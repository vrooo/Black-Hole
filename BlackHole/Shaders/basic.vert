#version 330 core
layout (location = 0) in vec3 position;

uniform mat4 viewMatrix;
uniform float aspectRatio;

varying vec3 rayDirectionUnrot;
varying vec3 rayDirection;

void main()
{
    vec4 ray = vec4(position.x * aspectRatio, position.y, position.z, 0.0f);
    rayDirectionUnrot = ray.xyz;
    rayDirection = (viewMatrix * ray).xyz;
    gl_Position = vec4(position, 1.0f);
}