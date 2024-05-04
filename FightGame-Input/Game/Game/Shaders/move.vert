#version 440

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec2 vTexCoord;

uniform vec2 u_Position;

void main()
{
    gl_Position = vec4(aPosition.x + u_Position.x, aPosition.y + u_Position.y, aPosition.z, 1.0);
    vTexCoord = aTexCoord;
}
