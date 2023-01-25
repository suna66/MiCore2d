#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;

void main()
{
  outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
