#version 330 core
const float sqrt3 = sqrt(3);
const int maxIter = 1000;
const float eps = 1e-6;

uniform samplerCube cubeMap;
uniform float M;
uniform float dist;

layout (location = 0) out vec4 oColor;

varying vec3 rayDirectionUnrot;
varying vec3 rayDirection;

float f(float a, float w)
{
    return w * w * (a * w - 1) + 1;
}

float getW1(float a)
{
    // f(w) = aw^3 - w^2 + 1 = 0
    // a = 2M / b
    // first positive root will be between 0 and sqrt3, f(0) > 0, f(sqrt3) < 0
    float start = 0, end = sqrt3, mid;
    float fstart = f(a, start), fend = f(a, end);
    for (int i = 0; i < maxIter; i++)
    {
        mid = (start + end) / 2;
        float fmid = f(a, mid);
        if (abs(fmid) < eps || (end - start) / 2 < eps) // TODO: better stop condition?
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
    float b = length(cross(toHole, rayDirectionUnrot)) / length(rayDirectionUnrot);
    float a = 2 * M / b;
    if (a >= 2 / (3*sqrt3))
        discard;
    oColor = texture(cubeMap, rayDirection);
}