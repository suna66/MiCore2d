#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;

void main()
{
  vec4 color = texture(texture0, texCoord).rgba;
  float g = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
  color.r = g * 0.9;
  color.g = g * 0.7;
  color.b = g * 0.4;
  outputColor = mix(color, texture(texture0, texCoord), texAlpha);
}
