#version 440

in vec2 vTexCoord;

out vec4 fragColor;

uniform sampler2D u_Texture;

void main()
{
    fragColor = texture(u_Texture, vTexCoord);
}
