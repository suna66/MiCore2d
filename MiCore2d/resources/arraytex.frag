#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2DArray texture0;
uniform float texAlpha;
uniform float texIndex;

void main()
{
  outputColor = mix(texture(texture0, vec3(texCoord, texIndex)), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
