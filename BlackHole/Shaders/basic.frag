#version 330 core
const float sqrt3 = 1.73205081;
const float pi = 3.14159265;
const int maxIter = 100;
const int intervals = 100;
const float eps = 1e-6;

uniform samplerCube cubeMap;
uniform float M;
uniform float dist;
uniform vec3 camPos;

layout (location = 0) out vec4 oColor;

varying vec3 rayDirection;

float g(float a, float w)
{
    return w * w * (a * w - 1) + 1;
}

float f(float a, float w)
{
    return 1 / sqrt(g(a, w));
}

float getW1(float a)
{
    // g(w) = aw^3 - w^2 + 1 = 0
    // a = 2M / b
    // first positive root will be between 0 and sqrt3, f(0) > 0, f(sqrt3) < 0
    float start = 0, end = sqrt3, mid;
    float fstart = g(a, start), fend = g(a, end), fmid;
    for (int i = 0; i < maxIter; i++)
    {
        mid = (start + end) / 2;
        fmid = g(a, mid);
        if (abs(fmid) < eps || (end - start) / 2 < eps)
            break;
        if (sign(fmid) == sign(fstart))
        {
            start = mid;
            fstart = fmid;
        }
        else
        {
            end = mid;
            fend = fmid;
        }
    }
    return mid; // we always gotta return something...
}

void main()
{
    vec3 toHole = vec3(0.0f, 0.0f, dist);
    vec3 crossCamRay = cross(camPos, rayDirection);
    float lenCrossCamRay = length(crossCamRay);
    float b = lenCrossCamRay / length(rayDirection);
    float a = 2 * M / b;
    if (a >= 2 / (3*sqrt3))
        discard;

    float w1 = getW1(a);
    float angle = f(a, 0) / 2;
    for (int k = 1; k < intervals; k++)
    {
        angle += f(a, k * w1 / intervals);
    }
    angle = 2 * angle * w1 / intervals - pi;
    float sinAngle = sin(-angle), cosAngle = cos(-angle);
    vec3 axis = crossCamRay / lenCrossCamRay;
    vec3 newRayDir = rayDirection * cosAngle + cross(axis, rayDirection) * sinAngle + axis * dot(axis, rayDirection) * (1.0f - cosAngle); // Rodrigues

    oColor = texture(cubeMap, newRayDir);
    //oColor = texture(cubeMap, rayDirection);
}